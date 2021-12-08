namespace AdventOfCode2021.Days.Day08
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class Day08 : BaseDay
    {
        public override string Part1()
        {
            List<string> inputLines = Input.Split("\r\n").ToList();

            int total = 0;
            foreach (string inputLine in inputLines)
            {
                total += inputLine.Split(" | ")[1].Split(' ').Where(segments => segments.Length == 2 || segments.Length == 3 || segments.Length == 4 || segments.Length == 7).Count();

            }

            return total.ToString();
        }

        public override string Part2()
        {
            List<string> inputLines = Input.Split("\r\n").ToList();

            int total = 0;
            foreach (string inputLine in inputLines)
            {
                string[] lineParts = inputLine.Split(" | ");

                string[] uniqueSignals = lineParts[0].Split(' ');

                string one = uniqueSignals.First(signal => signal.Length == 2);
                string four = uniqueSignals.First(signal => signal.Length == 4);
                string seven = uniqueSignals.First(signal => signal.Length == 3);
                string eight = uniqueSignals.First(signal => signal.Length == 7);

                string nine = uniqueSignals.First(signal => signal.Length == 6 && four.All(letter => signal.Contains(letter)) && seven.All(letter => signal.Contains(letter)));
                string zero = uniqueSignals.First(signal => signal.Length == 6 && signal != nine && nine.Count(letter => signal.Contains(letter)) == 5 && seven.All(letter => signal.Contains(letter)));
                string six = uniqueSignals.First(signal => signal.Length == 6 && signal != nine && signal != zero);

                string three = uniqueSignals.First(signal => signal.Length == 5 && nine.Count(letter => signal.Contains(letter)) == 5 && seven.All(letter => signal.Contains(letter)));
                string five = uniqueSignals.First(signal => signal.Length == 5 && six.Count(letter => signal.Contains(letter)) == 5);

                string two = uniqueSignals.First(signal => signal.Length == 5 && signal != five && three.Count(letter => signal.Contains(letter)) == 4);

                int output = 0;
                foreach (string outputSignal in lineParts[1].Split(' '))
                {
                    output *= 10;

                    if (outputSignal.Length == 2)
                    {
                        output += 1;
                    }
                    else if (outputSignal.Length == 4)
                    {
                        output += 4;
                    }
                    else if (outputSignal.Length == 3)
                    {
                        output += 7;
                    }
                    else if (outputSignal.Length == 7)
                    {
                        output += 8;
                    }
                    else if (outputSignal.Length == 5)
                    {
                        if (three.All(letter => outputSignal.Contains(letter)))
                        {
                            output += 3;
                        }
                        else if (five.All(letter => outputSignal.Contains(letter)))
                        {
                            output += 5;
                        }
                        else if (two.All(letter => outputSignal.Contains(letter)))
                        {
                            output += 2;
                        }
                    }
                    else if (outputSignal.Length == 6)

                    {
                        if (six.All(letter => outputSignal.Contains(letter)))
                        {
                            output += 6;
                        }
                        else if (nine.All(letter => outputSignal.Contains(letter)))
                        {
                            output += 9;
                        }
                        else if (zero.All(letter => outputSignal.Contains(letter)))
                        {
                            output += 0;
                        }
                    }
                }

                total += output;
            }

            return total.ToString();
        }
    }
}