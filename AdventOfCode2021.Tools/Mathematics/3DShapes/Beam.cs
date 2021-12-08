namespace AdventOfCode2021.Tools.Mathematics._3DShapes
{
	using Vectors;

	public class Beam
	{
		/// <summary>
		/// Position of Rectangle
		/// </summary>
		public Vector3 Position { get; set; }

		/// <summary>
		/// Size of rectangle
		/// </summary>
		public Vector3 Size { get; set; }

		/// <summary>
		/// Create a rectangle
		/// </summary>
		/// <param name="position"></param>
		/// <param name="size"></param>
		public Beam(Vector3 position, Vector3 size)
		{
			Position = position;
			Size = size;
		}

		/// <summary>
		/// Check if point is in rectangle
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		public bool IsInRectangle(Vector3 point)
		{
			return point.X >= Position.X &&
			       point.X <= Position.X + Size.X &&
			       point.Y >= Position.Y &&
			       point.Y <= Position.Y + Size.Y &&
			       point.Z >= Position.Z &&
			       point.Z <= Position.Z + Size.Z;
		}
	}
}