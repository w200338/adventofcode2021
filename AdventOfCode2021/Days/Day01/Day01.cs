namespace AdventOfCode2021.Days.Day01
{
    using System.Collections.Generic;
    using System.Linq;

    public class Day01 : BaseDay
    {
        /// <inheritdoc />
        public override string Part1()
        {
            List<int> ints = Input.Split('\n').Select(int.Parse).ToList();

            int largerThanPrevious = 0;
            for (int i = 1; i < ints.Count; i++)
            {
                if (ints[i] > ints[i - 1])
                {
                    largerThanPrevious++;
                }
            }

            return largerThanPrevious.ToString();
        }

        /// <inheritdoc />
        public override string Part2()
        {
            List<int> ints = Input.Split('\n').Select(int.Parse).ToList();

            int largerThanPrevious = 0;
            for (int i = 3; i < ints.Count; i++)
            {
                if (ints[i] + ints[i - 1] + ints[i - 2] > ints[i - 1] + ints[i - 2] + ints[i - 3])
                {
                    largerThanPrevious++;
                }
            }

            return largerThanPrevious.ToString();
        }
    }
}