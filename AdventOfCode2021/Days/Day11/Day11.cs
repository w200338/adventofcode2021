namespace AdventOfCode2021.Days.Day11
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Tools.Mathematics._2DShapes;
    using Tools.Mathematics.Vectors;

    public class Day11 : BaseDay
    {
        public override string Part1()
        {
            int[][] inputGrid = Input.Split("\r\n").Select(line => line.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();

            bool[][] flashed = new bool[inputGrid.Length][];
            for (int i = 0; i < inputGrid.Length; i++)
            {
                flashed[i] = new bool[inputGrid[0].Length];
            }

            RectangleInt bounds = new RectangleInt(Vector2Int.Zero, new Vector2Int(inputGrid.Length - 1, inputGrid[0].Length - 1));

            int totalFlashes = 0;
            for (int i = 0; i < 100; i++)
            {
                for (int x = 0; x < inputGrid.Length; x++)
                {
                    for (int y = 0; y < inputGrid[x].Length; y++)
                    {
                        inputGrid[x][y] += 1;
                    }
                }

                int lastFlashCount = int.MinValue;
                int currentFlashCount = 0;

                while (currentFlashCount > lastFlashCount)
                {
                    for (int x = 0; x < inputGrid.Length; x++)
                    {
                        for (int y = 0; y < inputGrid[x].Length; y++)
                        {
                            if (inputGrid[x][y] > 9 && !flashed[x][y])
                            {
                                flashed[x][y] = true;

                                IncreaseValue(inputGrid, bounds, new Vector2Int(x - 1, y - 1));
                                IncreaseValue(inputGrid, bounds, new Vector2Int(x - 1, y));
                                IncreaseValue(inputGrid, bounds, new Vector2Int(x - 1, y + 1));

                                IncreaseValue(inputGrid, bounds, new Vector2Int(x, y - 1));
                                IncreaseValue(inputGrid, bounds, new Vector2Int(x, y + 1));

                                IncreaseValue(inputGrid, bounds, new Vector2Int(x + 1, y - 1));
                                IncreaseValue(inputGrid, bounds, new Vector2Int(x + 1, y));
                                IncreaseValue(inputGrid, bounds, new Vector2Int(x + 1, y + 1));

                                /*
                                inputGrid[x - 1][y - 1] += 1;
                                inputGrid[x - 1][y] += 1;
                                inputGrid[x - 1][y + 1] += 1;

                                inputGrid[x][y - 1] += 1;
                                inputGrid[x][y + 1] += 1;

                                inputGrid[x + 1][y - 1] += 1;
                                inputGrid[x + 1][y] += 1;
                                inputGrid[x + 1][y + 1] += 1;
                                */
                            }
                        }
                    }

                    lastFlashCount = currentFlashCount;
                    currentFlashCount = flashed.Sum(line => line.Sum(pos => pos ? 1 : 0));
                }
                

                for (int x = 0; x < inputGrid.Length; x++)
                {
                    for (int y = 0; y < inputGrid[x].Length; y++)
                    {
                        if (flashed[x][y])
                        {
                            totalFlashes++;
                            flashed[x][y] = false;
                            inputGrid[x][y] = 0;
                        }
                    }
                }
            }

            return totalFlashes.ToString();
        }

        private void IncreaseValue(int[][] inputGrid, RectangleInt bounds, Vector2Int position)
        {
            if (bounds.IsInRectangle(position))
            {
                inputGrid[position.X][position.Y] += 1;
            }
        }

        public override string Part2()
        {
            int[][] inputGrid = Input.Split("\r\n").Select(line => line.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();

            bool[][] flashed = new bool[inputGrid.Length][];
            for (int i = 0; i < inputGrid.Length; i++)
            {
                flashed[i] = new bool[inputGrid[0].Length];
            }

            RectangleInt bounds = new RectangleInt(Vector2Int.Zero, new Vector2Int(inputGrid.Length - 1, inputGrid[0].Length - 1));

            int steps = 0;
            while (inputGrid.Sum(line => line.Sum()) > 0)
            {
                for (int x = 0; x < inputGrid.Length; x++)
                {
                    for (int y = 0; y < inputGrid[x].Length; y++)
                    {
                        inputGrid[x][y] += 1;
                    }
                }

                int lastFlashCount = int.MinValue;
                int currentFlashCount = 0;

                while (currentFlashCount > lastFlashCount)
                {
                    for (int x = 0; x < inputGrid.Length; x++)
                    {
                        for (int y = 0; y < inputGrid[x].Length; y++)
                        {
                            if (inputGrid[x][y] > 9 && !flashed[x][y])
                            {
                                flashed[x][y] = true;

                                IncreaseValue(inputGrid, bounds, new Vector2Int(x - 1, y - 1));
                                IncreaseValue(inputGrid, bounds, new Vector2Int(x - 1, y));
                                IncreaseValue(inputGrid, bounds, new Vector2Int(x - 1, y + 1));

                                IncreaseValue(inputGrid, bounds, new Vector2Int(x, y - 1));
                                IncreaseValue(inputGrid, bounds, new Vector2Int(x, y + 1));

                                IncreaseValue(inputGrid, bounds, new Vector2Int(x + 1, y - 1));
                                IncreaseValue(inputGrid, bounds, new Vector2Int(x + 1, y));
                                IncreaseValue(inputGrid, bounds, new Vector2Int(x + 1, y + 1));

                                /*
                                inputGrid[x - 1][y - 1] += 1;
                                inputGrid[x - 1][y] += 1;
                                inputGrid[x - 1][y + 1] += 1;

                                inputGrid[x][y - 1] += 1;
                                inputGrid[x][y + 1] += 1;

                                inputGrid[x + 1][y - 1] += 1;
                                inputGrid[x + 1][y] += 1;
                                inputGrid[x + 1][y + 1] += 1;
                                */
                            }
                        }
                    }

                    lastFlashCount = currentFlashCount;
                    currentFlashCount = flashed.Sum(line => line.Sum(pos => pos ? 1 : 0));
                }


                for (int x = 0; x < inputGrid.Length; x++)
                {
                    for (int y = 0; y < inputGrid[x].Length; y++)
                    {
                        if (flashed[x][y])
                        {
                            flashed[x][y] = false;
                            inputGrid[x][y] = 0;
                        }
                    }
                }

                steps++;
            }

            return steps.ToString();
        }
    }
}