namespace AdventOfCode2021.Tools.Mathematics._3DShapes
{
	using Vectors;

	public class SphereInt
	{
		public Vector3Int Position { get; set; }
		public int Radius { get; set; }

		/// <summary>
		/// Create sphere from a position and radius
		/// </summary>
		/// <param name="position"></param>
		/// <param name="radius"></param>
		public SphereInt(Vector3Int position, int radius)
		{
			Position = position;
			Radius = radius;
		}

		/// <summary>
		/// Create sphere from position and radius
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <param name="radius"></param>
		public SphereInt(int x, int y, int z, int radius)
		{
			Position = new Vector3Int(x, y, z);
			Radius = radius;
		}

		/// <summary>
		/// Is point in sphere
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		public bool IsInSphere(Vector3Int point)
		{
			return Vector3Int.Distance(Position, point) < Radius;
		}
	}
}