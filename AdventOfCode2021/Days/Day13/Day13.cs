namespace AdventOfCode2021.Days.Day13
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Tools.Mathematics.Vectors;

    public class Day13 : BaseDay
    {
        public override string Part1()
        {
            List<string> inputLines = Input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries).ToList();

            List<Vector2Int> inputPoints = new List<Vector2Int>();
            //List<int> foldAlongX = new List<int>();
            //List<int> foldAlongY = new List<int>();
            List<Fold> folds = new List<Fold>();
            foreach (string inputLine in inputLines)
            {
                if (inputLine.Contains(','))
                {
                    string[] lineParts = inputLine.Split(',');
                    inputPoints.Add(new Vector2Int(int.Parse(lineParts[0]), int.Parse(lineParts[1])));
                }
                else
                {
                    string[] lineParts = inputLine.Replace("fold along ", "").Split('=');
                    folds.Add(new Fold
                    {
                        XDirection = lineParts[0][0] == 'x',
                        Coordinate = int.Parse(lineParts[1])
                    });
                }
            }

            Fold fold = folds[0];
            foreach (Vector2Int point in inputPoints)
            {
                if (fold.XDirection)
                {
                    if (point.X > fold.Coordinate)
                    {
                        point.X -= 2 * (point.X - fold.Coordinate);
                    }
                }
                else
                {
                    if (point.Y > fold.Coordinate)
                    {
                        point.Y -= 2 * (point.Y - fold.Coordinate);
                    }
                }
            }

            /*
            foreach (Fold fold in folds)
            {
                foreach (Vector2Int point in inputPoints)
                {
                    if (fold.XDirection)
                    {
                        if (point.X > fold.Coordinate)
                        {
                            point.X -= 2 * (point.X - fold.Coordinate);
                        }
                    }
                    else
                    {
                        if (point.Y > fold.Coordinate)
                        {
                            point.Y -= 2 * (point.Y - fold.Coordinate);
                        }
                    }
                }
                
            }
            */
            /*
            for (int i = 0; i <= inputPoints.Max(point => point.X); i++)
            {
                for (int j = 0; j <= inputPoints.Max(point => point.Y); j++)
                {
                    if (inputPoints.Any(point => point == new Vector2Int(i, j)))
                    {
                        Console.Write('*');
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }
                Console.WriteLine();
            }
            */

            return inputPoints.Distinct().Count().ToString();
        }

        public override string Part2()
        {
            List<string> inputLines = Input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries).ToList();

            List<Vector2Int> inputPoints = new List<Vector2Int>();
            //List<int> foldAlongX = new List<int>();
            //List<int> foldAlongY = new List<int>();
            List<Fold> folds = new List<Fold>();
            foreach (string inputLine in inputLines)
            {
                if (inputLine.Contains(','))
                {
                    string[] lineParts = inputLine.Split(',');
                    inputPoints.Add(new Vector2Int(int.Parse(lineParts[0]), int.Parse(lineParts[1])));
                }
                else
                {
                    string[] lineParts = inputLine.Replace("fold along ", "").Split('=');
                    folds.Add(new Fold
                    {
                        XDirection = lineParts[0][0] == 'x',
                        Coordinate = int.Parse(lineParts[1])
                    });
                }
            }

            foreach (Fold fold in folds)
            {
                foreach (Vector2Int point in inputPoints)
                {
                    if (fold.XDirection)
                    {
                        if (point.X > fold.Coordinate)
                        {
                            point.X -= 2 * (point.X - fold.Coordinate);
                        }
                    }
                    else
                    {
                        if (point.Y > fold.Coordinate)
                        {
                            point.Y -= 2 * (point.Y - fold.Coordinate);
                        }
                    }
                }
                
            }

            for (int i = inputPoints.Max(point => point.X); i >= 0; i--)
            {
                for (int j = 0; j <= inputPoints.Max(point => point.Y); j++)
                {
                    if (inputPoints.Any(point => point == new Vector2Int(i, j)))
                    {
                        Console.Write('*');
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }
                Console.WriteLine();
            }

            return inputPoints.Distinct().Count().ToString();
        }
    }

    public class Fold
    {
        public bool XDirection { get; set; }

        public int Coordinate { get; set; }
    }
}