namespace AdventOfCode2021.Tools.PathFinding
{
	using System.Collections.Generic;
	using System.Linq;
	using Grids;
	using Mathematics.Vectors;

	public class AStarSolver
	{
		public Vector2Int StartPos { get; set; }
		public Vector2Int EndPos { get; set; }

		public bool WalkDiagonal { get; set; }

		private Grid2D<AStarGridCell> grid;

		public AStarSolver(int width, int height)
		{
			grid = new Grid2D<AStarGridCell>(width, height);
		}

		/// <summary>
		/// Set which tiles are not walkable
		/// </summary>
		/// <param name="positions"></param>
		public void SetWalls(IEnumerable<Vector2Int> positions)
		{
			foreach (Vector2Int position in positions)
			{
				grid.Get(position).Walkable = false;
			}
		}

		/// <summary>
		/// Set which tiles are walkable
		/// </summary>
		/// <param name="positions"></param>
		public void SetWalkable(IEnumerable<Vector2Int> positions)
		{
			foreach (Vector2Int position in positions)
			{
				grid.Get(position).Walkable = true;
			}
		}

		public List<GridCell> Solve()
		{
			AStarGridCell starGridCell = grid.Get(StartPos);
			starGridCell.GScore = 0;
			starGridCell.FScore = CalcHScore(StartPos);

			SortedSet<AStarGridCell> openSet = new SortedSet<AStarGridCell>();
			openSet.Add(starGridCell);

			while (openSet.Count > 0)
			{
				AStarGridCell current = openSet.Min();
				openSet.Remove(current);

				// reached end
				if (current.Position == EndPos)
				{
					return CreatePathFrom(current);
				}

				foreach (AStarGridCell neighbor in GetNeighbors(current))
				{
					if (!current.CameFrom.Equals(neighbor))
					{
						// calculate new scores
						double newGScore = current.GScore + neighbor.Position.Distance(current.Position);
						double newFScore = newGScore + CalcHScore(neighbor.Position);

						// check if this path is better
						if (newFScore < neighbor.FScore)
						{
							neighbor.GScore = newGScore;
							neighbor.FScore = newFScore;
							neighbor.CameFrom = current;
							openSet.Add(neighbor);
						}

						/* dijkstra
						double newDistanceFromStart = neighbor.Position.Distance(current.Position) + current.DistanceFromStart;
						if (newDistanceFromStart < neighbor.DistanceFromStart)
						{
							neighbor.DistanceFromStart = newDistanceFromStart;
							neighbor.CameFrom = current;
							openSet.Add(neighbor);
						}
						*/
					}
				}
			}

			throw new PathNotFoundException();
		}

		private List<GridCell> CreatePathFrom(GridCell end)
		{
			List<GridCell> gridCells = new List<GridCell>();
			GridCell current = end;

			while (current != null)
			{
				gridCells.Add(current);
				current = current.CameFrom;
			}

			return gridCells;
		}

		private List<AStarGridCell> GetNeighbors(AStarGridCell cell)
		{
			List<Vector2Int> positions = new List<Vector2Int>();
			if (WalkDiagonal)
			{
				foreach (Vector2Int offset in DiagonalDirectionOffsets)
				{
					positions.Add(cell.Position + offset);
				}
			}

			foreach (Vector2Int offset in DirectionOffsets)
			{
				positions.Add(cell.Position + offset);
			}

			return positions.Where(pos => pos.X > -1 && pos.Y > -1 && pos.X < grid.Width && pos.Y < grid.Height)
				.Select(pos => grid.Get(pos))
				.Where(gridCell => gridCell.Walkable)
				.ToList();
		}

		private static readonly List<Vector2Int> DirectionOffsets = new List<Vector2Int>()
		{
			new Vector2Int(-1, 0),
			new Vector2Int(1, 0),
			new Vector2Int(0, -1),
			new Vector2Int(0, 1),
		};

		private static readonly List<Vector2Int> DiagonalDirectionOffsets = new List<Vector2Int>()
		{
			new Vector2Int(-1, -1),
			new Vector2Int(-1, 1),
			new Vector2Int(1, -1),
			new Vector2Int(1, 1),
		};

		private double CalcHScore(Vector2Int position)
		{
			return Vector2Int.Distance(EndPos, position);
		}
	}
}