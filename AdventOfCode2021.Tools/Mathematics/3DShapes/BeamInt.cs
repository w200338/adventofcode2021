namespace AdventOfCode2021.Tools.Mathematics._3DShapes
{
	using Vectors;

	public class BeamInt
	{
		/// <summary>
		/// Position of Rectangle
		/// </summary>
		public Vector3Int Position { get; set; }

		/// <summary>
		/// Size of rectangle
		/// </summary>
		public Vector3Int Size { get; set; }

		/// <summary>
		/// Create a rectangle
		/// </summary>
		/// <param name="position"></param>
		/// <param name="size"></param>
		public BeamInt(Vector3Int position, Vector3Int size)
		{
			Position = position;
			Size = size;
		}

		/// <summary>
		/// Check if point is in rectangle
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		public bool IsInRectangle(Vector3Int point)
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
