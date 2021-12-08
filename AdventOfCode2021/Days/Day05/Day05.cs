namespace AdventOfCode2021.Days.Day05
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Tools.Mathematics._2DShapes;
    using Tools.Mathematics.Vectors;

    public class Day05 : BaseDay
    {
        public override string Part1()
        {
            List<string> inputLines = Input.Split("\r\n").ToList();

            List<LineInt> lines = new List<LineInt>();
            foreach (string inputLine in inputLines)
            {
                int[] numbers = inputLine.Split(new string[] { ",", " -> " }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                lines.Add(new LineInt()
                {
                    A = new Vector2Int(numbers[0], numbers[1]),
                    B = new Vector2Int(numbers[2], numbers[3]),
                });
            }

            int maxX = lines.SelectMany(line => new List<Vector2Int> { line.A, line.B }).Max(point => point.X);
            int maxY = lines.SelectMany(line => new List<Vector2Int> { line.A, line.B }).Max(point => point.Y);

            int totalTwoOverlap = 0;
            lines = lines.Where(line => line.A.X == line.B.X || line.A.Y == line.B.Y).ToList();

            for (int i = 0; i <= maxX; i++)
            {
                for (int j = 0; j <= maxY; j++)
                {
                    Vector2Int point = new Vector2Int(i, j);

                    int amountOnPoint = lines.Count(line => line.IsOnLineStraight(point));
                    if (amountOnPoint >= 2)
                    {
                        totalTwoOverlap++;
                    }
                }
            }

            return totalTwoOverlap.ToString();
        }

        public override string Part2()
        {
            List<string> inputLines = Input.Split("\r\n").ToList();

            List<LineInt> lines = new List<LineInt>();
            foreach (string inputLine in inputLines)
            {
                int[] numbers = inputLine.Split(new string[] { ",", " -> " }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                lines.Add(new LineInt
                {
                    A = new Vector2Int(numbers[0], numbers[1]),
                    B = new Vector2Int(numbers[2], numbers[3]),
                });
            }

            int maxX = lines.SelectMany(line => new List<Vector2Int> { line.A, line.B }).Max(point => point.X);
            int maxY = lines.SelectMany(line => new List<Vector2Int> { line.A, line.B }).Max(point => point.Y);

            int totalTwoOverlap = 0;

            for (int i = 0; i <= maxX; i++)
            {
                for (int j = 0; j <= maxY; j++)
                {
                    Vector2Int point = new Vector2Int(i, j);

                    int amountOnPoint = lines.Count(line => line.IsOnLine(point));
                    if (amountOnPoint >= 2)
                    {
                        totalTwoOverlap++;
                    }
                }
            }

            return totalTwoOverlap.ToString();
        }
    }
}