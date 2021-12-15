namespace AdventOfCode2021.Tools.PathFinding.Dijkstra
{
    using System;
    using System.Collections.Generic;

    public class DijkstraNode : IComparable<DijkstraNode>
    {
        private static int IdCounter = 1;

        public DijkstraNode()
        {
            Id = IdCounter++;
        }

        public int Id { get; set; }

        public List<DijkstraConnection> Connections { get; } = new List<DijkstraConnection>();

        public DijkstraNode PreviousNode { get; set; }

        public double DistanceToStart { get; set; }

        /// <inheritdoc />
        public int CompareTo(DijkstraNode other)
        {
            if (Math.Abs(DistanceToStart - other.DistanceToStart) > 0.001)
            {
                return DistanceToStart.CompareTo(other.DistanceToStart);
            }

            return Id.CompareTo(other.Id);
        }
    }
}