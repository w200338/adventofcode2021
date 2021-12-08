namespace AdventOfCode2021.Days.Day03
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    public class Day03 : BaseDay
    {
        /// <inheritdoc />
        public override string Part1()
        {
            List<string> lines = Input.Split("\r\n").ToList();

            int gamma = 0;
            int epsilon = 0;
            for (int i = 0; i < lines[0].Length; i++)
            {
                int amountOne = 0;
                int amountZero = 0;

                foreach (string line in lines)
                {
                    if (line[i] == '0')
                    {
                        amountZero++;
                    }
                    else
                    {
                        amountOne++;
                    }
                }

                gamma <<= 1;
                epsilon <<= 1;

                if (amountOne > amountZero)
                {
                    gamma++;
                }
                else
                {
                    epsilon++;
                }
            }

            return (gamma * epsilon).ToString();
        }

        /// <inheritdoc />
        public override string Part2()
        {
            List<string> lines = Input.Split("\r\n").ToList();
            int oxygenGeneratorRating = 0;
            for (int i = 0; i < lines[0].Length; i++)
            {
                int amountOne = 0;
                int amountZero = 0;

                foreach (string line in lines)
                {
                    if (line[i] == '0')
                    {
                        amountZero++;
                    }
                    else
                    {
                        amountOne++;
                    }
                }

                if (amountOne >= amountZero)
                {
                    lines = lines.Where(line => line[i] == '1').ToList();
                }
                else
                {
                    lines = lines.Where(line => line[i] == '0').ToList();
                }

                if (lines.Count == 1)
                {
                    oxygenGeneratorRating = Convert.ToInt32(lines[0], 2);
                    break;
                }
            }

            lines = Input.Split("\r\n").ToList();
            int co2Rating = 0;
            for (int i = 0; i < lines[0].Length; i++)
            {
                int amountOne = 0;
                int amountZero = 0;

                foreach (string line in lines)
                {
                    if (line[i] == '0')
                    {
                        amountZero++;
                    }
                    else
                    {
                        amountOne++;
                    }
                }

                if (amountOne < amountZero)
                {
                    lines = lines.Where(line => line[i] == '1').ToList();
                }
                else
                {
                    lines = lines.Where(line => line[i] == '0').ToList();
                }

                if (lines.Count == 1)
                {
                    co2Rating = Convert.ToInt32(lines[0], 2);
                    break;
                }
            }

            return (oxygenGeneratorRating * co2Rating).ToString();
        }
    }
}