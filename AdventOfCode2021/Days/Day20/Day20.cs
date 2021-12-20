namespace AdventOfCode2021.Days.Day20
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Tools.Mathematics._2DShapes;
    using Tools.Mathematics.Vectors;

    public class Day20 : BaseDay
    {
        public override string Part1()
        {
            string[] inputLines = Input.Split("\r\n");

            string algorithm = inputLines[0];
            Dictionary<Vector2Int, bool> image = new Dictionary<Vector2Int, bool>();

            for (int y = 2; y < inputLines.Length; y++)
            {
                for (int x = 0; x < inputLines[y].Length; x++)
                {
                    image.Add(new Vector2Int(x, y - 2), inputLines[y][x] == '#');
                }
            }

            RectangleInt bounds = new RectangleInt(
                new Vector2Int(image.Min(kvp => kvp.Key.X), image.Min(kvp => kvp.Key.Y)),
                new Vector2Int(image.Max(kvp => kvp.Key.X), image.Max(kvp => kvp.Key.Y)));

            bool background = true;
            for (int step = 0; step < 2; step++)
            {
                Dictionary<Vector2Int, bool> updatedPixels = new Dictionary<Vector2Int, bool>();
                bounds.Position -= Vector2Int.One;
                bounds.Size += Vector2Int.One * 2;
                
                if (bounds.Size.X > 90)
                {
                    background = !background;
                }

                for (int y = bounds.Position.Y; y <= bounds.Size.Y; y++)
                {
                    for (int x = bounds.Position.X; x <= bounds.Size.X; x++)
                    {
                        var position = new Vector2Int(x, y);
                        int pixelIndex = ReadPixel(image, position, background);

                        updatedPixels.Add(position, algorithm[pixelIndex] == '#');

                        Console.Write(algorithm[pixelIndex]);
                    }

                    Console.WriteLine();
                }

                Console.WriteLine();
                image = updatedPixels;
            }

            return image.Count(pixel => pixel.Value).ToString();
        }

        private int ReadPixel(Dictionary<Vector2Int, bool> image, Vector2Int position, bool background = false)
        {
            int output = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    output <<= 1;
                    if (image.TryGetValue(new Vector2Int(position.X + j, position.Y + i), out bool isOn))
                    {
                        output += isOn ? 1 : 0;
                    }
                    else
                    {
                        output += background ? 1 : 0;
                    }
                }
            }

            return output;
        }

        public override string Part2()
        {
            string[] inputLines = Input.Split("\r\n");

            string algorithm = inputLines[0];
            Dictionary<Vector2Int, bool> image = new Dictionary<Vector2Int, bool>();

            for (int y = 2; y < inputLines.Length; y++)
            {
                for (int x = 0; x < inputLines[y].Length; x++)
                {
                    image.Add(new Vector2Int(x, y - 2), inputLines[y][x] == '#');
                }
            }

            RectangleInt bounds = new RectangleInt(
                new Vector2Int(image.Min(kvp => kvp.Key.X), image.Min(kvp => kvp.Key.Y)),
                new Vector2Int(image.Max(kvp => kvp.Key.X), image.Max(kvp => kvp.Key.Y)));

            bool background = true;
            for (int step = 0; step < 50; step++)
            {
                Dictionary<Vector2Int, bool> updatedPixels = new Dictionary<Vector2Int, bool>();
                bounds.Position -= Vector2Int.One;
                bounds.Size += Vector2Int.One * 2;

                if (bounds.Size.X > 90)
                {
                    background = !background;
                }

                for (int y = bounds.Position.Y; y <= bounds.Size.Y; y++)
                {
                    for (int x = bounds.Position.X; x <= bounds.Size.X; x++)
                    {
                        var position = new Vector2Int(x, y);
                        int pixelIndex = ReadPixel(image, position, background);

                        updatedPixels.Add(position, algorithm[pixelIndex] == '#');

                        //Console.Write(algorithm[pixelIndex]);
                    }

                    //Console.WriteLine();
                }

                //Console.WriteLine();
                image = updatedPixels;
            }

            return image.Count(pixel => pixel.Value).ToString();
        }
    }
}