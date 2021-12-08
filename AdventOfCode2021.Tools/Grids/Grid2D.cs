namespace AdventOfCode2021.Tools.Grids
{
	using System.Collections.Generic;
	using Mathematics.Vectors;

	public class Grid2D<T>
	{
		private List<T> data;

		public int Width { get; private set; }
		public int Height { get; private set; }

		public Grid2D(int width, int height)
		{
			Width = width;
			Height = height;

			data = new List<T>(Width * Height);
			for (int i = 0; i < Width * Height; i++)
			{
				data.Add(default);
			}
		}

		public T Get(int x, int y)
		{
			return data[x * Height + y];
		}

		public T Get(Vector2Int pos)
		{
			return Get(pos.X, pos.Y);
		}

		public void Set(int x, int y, T value)
		{
			data[x * Height + y] = value;
		}

		public void Set(Vector2Int pos, T value)
		{
			Set(pos.X, pos.Y, value);
		}
	}
}