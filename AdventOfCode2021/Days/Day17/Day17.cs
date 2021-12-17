namespace AdventOfCode2021.Days.Day17
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Tools.Mathematics._2DShapes;
    using Tools.Mathematics.Vectors;

    public class Day17 : BaseDay
    {
        private Regex inputRegex = new Regex(@"x=(-?\d+)\.\.(-?\d+), y=(\-\d+)\.\.(-?\d+)");

        public override string Part1()
        {
            Match regexMatch = inputRegex.Match(Input);
            int xMin = int.Parse(regexMatch.Groups[1].Value);
            int xMax = int.Parse(regexMatch.Groups[2].Value);
            int yMin = int.Parse(regexMatch.Groups[3].Value);
            int yMax = int.Parse(regexMatch.Groups[4].Value);
            RectangleInt bounds = new RectangleInt(new Vector2Int(xMin, yMin), new Vector2Int(xMax - xMin, yMax - yMin));

            int bestHeight = int.MinValue;
            for (int x = -200; x < 200; x++)
            {
                for (int y = -200; y < 200; y++)
                {
                    int bestHeightInRun = int.MinValue;
                    Vector2Int probePosition = Vector2Int.Zero;
                    Vector2Int probeVelocity = new Vector2Int(x, y);

                    for (int i = 0; i < 250; i++)
                    {
                        probePosition += probeVelocity;

                        if (probePosition.Y > bestHeightInRun)
                        {
                            bestHeightInRun = probePosition.Y;
                        }

                        probeVelocity.Y -= 1;

                        if (probeVelocity.X > 0)
                        {
                            probeVelocity.X -= 1;
                        }
                        else if (probeVelocity.X < 0)
                        {
                            probeVelocity.X += 1;
                        }

                        if (bounds.IsInRectangle(probePosition))
                        {
                            if (bestHeightInRun > bestHeight)
                            {
                                bestHeight = bestHeightInRun;
                            }
                            break;
                        }

                        if (probePosition.Y < yMin)
                        {
                            break;
                        }
                    }
                }
            }

            return bestHeight.ToString();
        }

        public override string Part2()
        {
            Match regexMatch = inputRegex.Match(Input);
            int xMin = int.Parse(regexMatch.Groups[1].Value);
            int xMax = int.Parse(regexMatch.Groups[2].Value);
            int yMin = int.Parse(regexMatch.Groups[3].Value);
            int yMax = int.Parse(regexMatch.Groups[4].Value);
            RectangleInt bounds = new RectangleInt(new Vector2Int(xMin, yMin), new Vector2Int(xMax - xMin, yMax - yMin));

            int totalReached = 0;
            for (int x = -200; x < 200; x++)
            {
                for (int y = -200; y < 200; y++)
                {
                    Vector2Int probePosition = Vector2Int.Zero;
                    Vector2Int probeVelocity = new Vector2Int(x, y);

                    for (int i = 0; i < 250; i++)
                    {
                        probePosition += probeVelocity;

                        probeVelocity.Y -= 1;

                        if (probeVelocity.X > 0)
                        {
                            probeVelocity.X -= 1;
                        }
                        else if (probeVelocity.X < 0)
                        {
                            probeVelocity.X += 1;
                        }

                        if (bounds.IsInRectangle(probePosition))
                        {
                            totalReached++;
                            break;
                        }

                        if (probePosition.Y < yMin)
                        {
                            break;
                        }
                    }
                }
            }

            return totalReached.ToString();
        }
    }
}