namespace AdventOfCode2021.Days.Day07
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class Day07 : BaseDay
    {
        public override string Part1()
        {
            List<int> horizontalPositions = Input.Split(',').Select(int.Parse).ToList();

            int minPos = horizontalPositions.Min();
            int maxPos = horizontalPositions.Max();

            int lowestTotal = int.MaxValue;
            for (int i = minPos; i <= maxPos; i++)
            {
                int total = horizontalPositions.Select(position => Math.Abs(i - position)).Sum();

                if (total < lowestTotal)
                {
                    lowestTotal = total;
                }
            }

            return lowestTotal.ToString();
        }

        public override string Part2()
        {
            List<int> horizontalPositions = Input.Split(',').Select(int.Parse).ToList();

            int minPos = horizontalPositions.Min();
            int maxPos = horizontalPositions.Max();

            int lowestTotal = int.MaxValue;
            for (int i = minPos; i <= maxPos; i++)
            {
                int total = horizontalPositions.Select(position => Math.Abs(i - position)).Select(n => n * (n + 1) / 2).Sum();

                if (total < lowestTotal)
                {
                    lowestTotal = total;
                }
            }

            return lowestTotal.ToString();
        }
    }
}