namespace AdventOfCode2021.Days.Day06
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class Day06 : BaseDay
    {
        public override string Part1()
        {
            List<int> fishTimes = Input.Split(',').Select(int.Parse).ToList();

            for (int i = 0; i < 80; i++)
            {
                int newFish = 0;
                for (int j = 0; j < fishTimes.Count; j++)
                {
                    if (fishTimes[j] == 0)
                    {
                        fishTimes[j] = 7;
                        newFish++;
                    }

                    fishTimes[j] -= 1;
                }

                for (int j = 0; j < newFish; j++)
                {
                    fishTimes.Add(8);
                }
            }

            return fishTimes.Count.ToString();
        }

        public override string Part2()
        {
            List<int> fishTimes = Input.Split(',').Select(int.Parse).ToList();

            var fishDictionary = new Dictionary<int, long>
            {
                { 0, 0 },
                { 1, 0 },
                { 2, 0 },
                { 3, 0 },
                { 4, 0 },
                { 5, 0 },
                { 6, 0 },
                { 7, 0 },
                { 8, 0 }
            };

            for (int i = 0; i < fishTimes.Count; i++)
            {
                fishDictionary[fishTimes[i]] += 1;
            }

            for (int i = 0; i < 256; i++)
            {
                long newFish = fishDictionary[0];
                for (int j = 1; j <= 8; j++)
                {
                    fishDictionary[j - 1] = fishDictionary[j];
                }

                fishDictionary[6] += newFish;
                fishDictionary[8] = newFish;
            }

            return fishDictionary.Sum(kvp => kvp.Value).ToString();
        }
    }
}