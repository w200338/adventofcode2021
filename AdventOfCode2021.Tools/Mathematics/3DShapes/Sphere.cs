namespace AdventOfCode2021.Tools.Mathematics._3DShapes
{
	using Vectors;

	public class Sphere
	{
		public Vector3 Position { get; set; }
		public double Radius { get; set; }

		/// <summary>
		/// Create sphere from a position and radius
		/// </summary>
		/// <param name="position"></param>
		/// <param name="radius"></param>
		public Sphere(Vector3 position, double radius)
		{
			Position = position;
			Radius = radius;
		}

		/// <summary>
		/// Is point in sphere
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		public bool IsInSphere(Vector3 point)
		{
			return Vector3.Distance(Position, point) < Radius;
		}
    }
}