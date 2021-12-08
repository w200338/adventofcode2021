namespace AdventOfCode2021.Tools.Mathematics._2DShapes
{
	using Vectors;

	public class CircleInt
	{
		/// <summary>
		/// Position of the circle
		/// </summary>
		public Vector2Int Position { get; set; }

		/// <summary>
		/// Radius of the circle
		/// </summary>
		public int Radius { get; set; }

		/// <summary>
		/// Create a circle from a position and radius
		/// </summary>
		/// <param name="position"></param>
		/// <param name="radius"></param>
		public CircleInt(Vector2Int position, int radius)
		{
			Position = position;
			Radius = radius;
		}

		/// <summary>
		/// Check if point is in circle
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		public bool IsInCircle(Vector2Int point)
		{
			return Vector2Int.Distance(point, Position) <= Radius;
		}
	}
}