namespace AdventOfCode2021.Days.Day09
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Tools.Mathematics._2DShapes;
    using Tools.Mathematics.Vectors;

    public class Day09 : BaseDay
    {
        public override string Part1()
        {
            List<List<int>> input = Input.Split("\r\n").Select(line => line.ToCharArray().Select(c => c.ToString()).Select(int.Parse).ToList()).ToList();

            int totalRisk = 0;
            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[0].Count; j++)
                {
                    bool isLowest = true;

                    if (i > 0)
                    {
                        if (input[i - 1][j] <= input[i][j])
                        {
                            isLowest = false;
                        }
                    }

                    if (i < input.Count - 1)
                    {
                        if (input[i + 1][j] <= input[i][j])
                        {
                            isLowest = false;
                        }
                    }

                    if (j > 0)
                    {
                        if (input[i][j - 1] <= input[i][j])
                        {
                            isLowest = false;
                        }
                    }

                    if (j < input[0].Count - 1)
                    {
                        if (input[i][j + 1] <= input[i][j])
                        {
                            isLowest = false;
                        }
                    }

                    if (isLowest)
                    {
                        totalRisk += 1 + input[i][j];
                    }
                }
            }

            return totalRisk.ToString();

        }

        public override string Part2()
        {
            List<List<int>> input = Input.Split("\r\n").Select(line => line.ToCharArray().Select(c => c.ToString()).Select(int.Parse).ToList()).ToList();

            RectangleInt gridRectangle = new RectangleInt(Vector2Int.Zero, new Vector2Int(input.Count - 1, input[0].Count - 1));
            List<Vector2Int> lowPoints = new List<Vector2Int>();
            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[0].Count; j++)
                {
                    bool isLowest = true;

                    if (i > 0)
                    {
                        if (input[i - 1][j] <= input[i][j])
                        {
                            isLowest = false;
                        }
                    }

                    if (i < input.Count - 1)
                    {
                        if (input[i + 1][j] <= input[i][j])
                        {
                            isLowest = false;
                        }
                    }

                    if (j > 0)
                    {
                        if (input[i][j - 1] <= input[i][j])
                        {
                            isLowest = false;
                        }
                    }

                    if (j < input[0].Count - 1)
                    {
                        if (input[i][j + 1] <= input[i][j])
                        {
                            isLowest = false;
                        }
                    }

                    if (isLowest)
                    {
                        lowPoints.Add(new Vector2Int(i, j));
                    }
                }
            }

            List<int> basinSizes = new List<int>();
            foreach (Vector2Int lowPoint in lowPoints)
            {
                Queue<Vector2Int> visitQueue = new Queue<Vector2Int>();
                visitQueue.Enqueue(lowPoint);
                HashSet<Vector2Int> visitedNeighbors = new HashSet<Vector2Int>();

                while (visitQueue.Count > 0)
                {
                    Vector2Int currentPoint = visitQueue.Dequeue();
                    foreach (Vector2Int neighbor in NeighborDirections
                        .Select(direction => currentPoint + direction)
                        .Where(neighbor => gridRectangle.IsInRectangle(neighbor)))
                    {
                        if (visitedNeighbors.Contains(neighbor) || input[neighbor.X][neighbor.Y] == 9)
                        {
                            continue;
                        }

                        visitedNeighbors.Add(neighbor);
                        visitQueue.Enqueue(neighbor);
                    }
                }

                basinSizes.Add(visitedNeighbors.Count);
            }

            int score = basinSizes.OrderByDescending(basinSize => basinSize).Take(3).Aggregate((acc, current) => acc * current);
            return score.ToString();
        }

        private static List<Vector2Int> NeighborDirections = new List<Vector2Int>
        {
            new Vector2Int(-1, 0),
            new Vector2Int(1, 0),
            new Vector2Int(0, -1),
            new Vector2Int(0, 1)
        };
    }
}