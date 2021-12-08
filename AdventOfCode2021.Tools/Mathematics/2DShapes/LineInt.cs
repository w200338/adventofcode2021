namespace AdventOfCode2021.Tools.Mathematics._2DShapes
{
	using System;
	using Vectors;

	public class LineInt
	{
		public Vector2Int A { get; set; }
		public Vector2Int B { get; set; }

		/// <summary>
		/// Length of line calculated with manhattan distance
		/// </summary>
		/// <returns></returns>
		public int LengthManhattan => A.DistanceManhattan(B);

		/// <summary>
		/// Total length of the line
		/// </summary>
		/// <returns></returns>
		public double Length => A.Distance(B);

		/// <summary>
		/// Check if two lines intersect
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public Vector2Int Intersect(LineInt other)
		{
			double deltaACy = A.Y - other.A.Y;
			double deltaDCx = other.B.X - other.A.X;
			double deltaACx = A.X - other.A.X;
			double deltaDCy = other.B.Y - other.A.Y;
			double deltaBAx = B.X - A.X;
			double deltaBAy = B.Y - A.Y;

			double denominator = deltaBAx * deltaDCy - deltaBAy * deltaDCx;
			double numerator = deltaACy * deltaDCx - deltaACx * deltaDCy;

			if (denominator == 0)
			{
				if (numerator == 0)
				{
					// collinear. Potentially infinite intersection points.
					// Check and return one of them.
					if (A.X >= other.A.X && A.X <= other.B.X)
					{
						return A;
					}
					else if (other.A.X >= A.X && other.A.X <= B.X)
					{
						return other.A;
					}
					else
					{
						return null;
					}
				}
				else
				{ // parallel
					return null;
				}
			}

			double r = numerator / denominator;
			if (r < 0 || r > 1)
			{
				return null;
			}

			double s = (deltaACy * deltaBAx - deltaACx * deltaBAy) / denominator;
			if (s < 0 || s > 1)
			{
				return null;
			}

			return new Vector2Int((int)(A.X + r * deltaBAx), (int)(A.Y + r * deltaBAy));
        }

		/// <summary>
		/// Check if point is on line
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		public bool IsOnLine(Vector2Int point)
		{
			/*
             return true anytime C lies perfectly on the line between A en B
             A-C------B
            
            */
			return Math.Abs(Vector2Int.Distance(A, point) + Vector2Int.Distance(point, B) - Length) < 0.000_001;
		}

		public bool IsOnLineStraight(Vector2Int point)
		{
			/*
             return true anytime C lies perfectly on the line between A en B
             A-C------B
            
            */

			if (point.X == A.X && point.X == B.X)
			{
				if (point.Y >= A.Y && point.Y <= B.Y || point.Y >= B.Y && point.Y <= A.Y)
				{
					return true;
				}
			}

			if (point.Y == A.Y && point.Y == B.Y)
			{
				if (point.X >= A.X && point.X <= B.X || point.X >= B.X && point.X <= A.X)
				{
					return true;
				}
			}
			

			return false;
			return Math.Abs(Vector2Int.Distance(A, point) + Vector2Int.Distance(point, B) - Length) < 0.000_001;
		}
	}
}