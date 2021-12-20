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
            RectangleInt bounds = new RectangleInt(Vector2Int.Zero - Vector2Int.One * 3, new Vector2Int(inputLines[3].Length, inputLines.Length - 2) + Vector2Int.One * 3);

            for (int y = 2; y < inputLines.Length; y++)
            {
                for (int x = 0; x < inputLines[y].Length; x++)
                {
                    image.Add(new Vector2Int(x, y - 2), inputLines[y][x] == '#');
                }
            }

            for (int step = 0; step < 2; step++)
            {
                Dictionary<Vector2Int, bool> updatedPixels = new Dictionary<Vector2Int, bool>();
                bounds.Position -= Vector2Int.One;
                bounds.Size += Vector2Int.One * 2;

                for (int y = bounds.Position.Y; y <= bounds.Size.Y; y++)
                {
                    for (int x = bounds.Position.X; x <= bounds.Size.X; x++)
                    {
                        var position = new Vector2Int(x, y);
                        int pixelIndex = ReadPixel(image, position);

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

        private int ReadPixel(List<Pixel> image, Vector2Int position)
        {
            int output = 0;
            //string bits = "";
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    Pixel pixel = image.FirstOrDefault(pixel => pixel.Position == new Vector2Int(position.X + j, position.Y + i));

                    if (pixel == null)
                    {
                        output <<= 1;
                        //bits += '0';
                    }
                    else
                    {
                        output <<= 1;
                        output += pixel.IsOn ? 1 : 0;
                        //bits += pixel.IsOn ? '1' : '0';
                    }
                }
            }

            //var value = Convert.ToInt32(bits, 2);
            return output;
        }

        private int ReadPixel(Dictionary<Vector2Int, bool> image, Vector2Int position)
        {
            int output = 0;
            //string bits = "";
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    output <<= 1;
                    if (image.TryGetValue(new Vector2Int(position.X + j, position.Y + i), out bool isOn))
                    {
                        output += isOn ? 1 : 0;
                    }
                }
            }

            //var value = Convert.ToInt32(bits, 2);
            return output;
        }

        public override string Part2()
        {
            string[] inputLines = Input.Split("\r\n");

            string algorithm = inputLines[0];
            Dictionary<Vector2Int, bool> image = new Dictionary<Vector2Int, bool>();
            RectangleInt bounds = new RectangleInt(Vector2Int.Zero, new Vector2Int(inputLines[3].Length, inputLines.Length - 2));

            for (int y = 2; y < inputLines.Length; y++)
            {
                for (int x = 0; x < inputLines[x].Length; x++)
                {
                    image.Add(new Vector2Int(x, y - 2), inputLines[y][x] == '#');
                }
            }

            for (int step = 0; step < 50; step++)
            {
                Dictionary<Vector2Int, bool> updatedPixels = new Dictionary<Vector2Int, bool>();
                bounds.Position -= Vector2Int.One;
                bounds.Size += Vector2Int.One * 2;

                for (int y = bounds.Position.Y; y <= bounds.Size.Y; y++)
                {
                    for (int x = bounds.Position.X; x <= bounds.Size.X; x++)
                    {
                        var position = new Vector2Int(x, y);
                        int pixelIndex = ReadPixel(image, position);

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

    public class Pixel
    {
        public Vector2Int Position { get; set; }

        public bool IsOn { get; set; }
    }
}