namespace AdventOfCode2021.Tools.PathFinding.Dijkstra
{
    using System;
    using System.Collections.Generic;

    public static class DijkstraSolver
    {
        public static List<DijkstraNode> Solve(List<DijkstraNode> nodes, DijkstraNode start, DijkstraNode end)
        {
            foreach (DijkstraNode node in nodes)
            {
                node.DistanceToStart = double.PositiveInfinity;
            }

            start.DistanceToStart = 0;

            var openSet = new SortedSet<DijkstraNode>();
            openSet.Add(start);

            while (openSet.Count > 0)
            {
                DijkstraNode currentNode = openSet.Min;
                openSet.Remove(currentNode);

                // end
                if (currentNode == end)
                {
                    return CreatePathFromEndNode(currentNode);
                }

                // neighbours
                foreach (DijkstraConnection connection in currentNode.Connections)
                {
                    double totalDistance = currentNode.DistanceToStart + connection.Distance;
                    if (totalDistance < connection.Node.DistanceToStart)
                    {
                        connection.Node.DistanceToStart = totalDistance;
                        connection.Node.PreviousNode = currentNode;

                        openSet.Add(connection.Node);
                    }
                }
            }

            throw new Exception("No path found between given nodes");
        }

        private static List<DijkstraNode> CreatePathFromEndNode(DijkstraNode node)
        {
            var nodes = new List<DijkstraNode>();

            while (node != null)
            {
                nodes.Add(node);
                node = node.PreviousNode;
            }

            nodes.Reverse();
            return nodes;
        }
    }
}