namespace AdventOfCode2021.Tools.PathFinding
{
	using System;
	using Mathematics.Vectors;

	public class GridCell : IComparable<GridCell>
	{
		public Vector2Int Position { get; set; }
		public bool Walkable { get; set; }
		public double DistanceFromStart { get; set; } = double.PositiveInfinity;

		/// <summary>
		/// Cell where this one came from
		/// </summary>
		public GridCell CameFrom { get; set; }

		protected bool Equals(GridCell other)
		{
			return Equals(Position, other.Position);
		}

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((GridCell) obj);
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			return (Position != null ? Position.GetHashCode() : 0);
		}


		/// <inheritdoc />
		public int CompareTo(GridCell other)
		{
			if (ReferenceEquals(this, other)) return 0;
			if (ReferenceEquals(null, other)) return 1;
			return DistanceFromStart.CompareTo(other.DistanceFromStart);
		}
	}
}