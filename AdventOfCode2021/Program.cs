using System;

namespace AdventOfCode2021
{
    using System.Diagnostics;

    public class Program
    {
        public static void Main(string[] args)
        {
			int day;

            // auto select day in december 2021
            DateTime today = DateTime.Now;
            if (today.Year == 2021 && today.Month == 12 && today.Day <= 25)
            {
                day = today.Day;
            }
            else
            {
                Console.WriteLine("Select a day:");
                day = int.Parse(Console.ReadLine());
            }

            string dayName = $"Day{(day < 10 ? "0" + day : day.ToString())}";
            Type type = Type.GetType($"AdventOfCode2021.Days.{dayName}.{dayName}");
            BaseDay currentDay = (BaseDay)Activator.CreateInstance(type);

            // execute day
            currentDay.ReadInput().Wait();

            Stopwatch stopwatch = Stopwatch.StartNew();
            Console.WriteLine("Part 1");
            Console.WriteLine(currentDay.Part1());
            Console.WriteLine($"took {stopwatch.Elapsed.TotalMilliseconds} ms");

            Console.WriteLine();

            stopwatch = Stopwatch.StartNew();
            Console.WriteLine("Part 2");
            Console.WriteLine(currentDay.Part2());
            Console.WriteLine($"took {stopwatch.Elapsed.TotalMilliseconds} ms");

            Console.Write("\nPress any key to quit");
            Console.ReadKey();
		}
    }
}
