namespace AdventOfCode2021.Days.Day02
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Tools.Enums;
    using Tools.Mathematics.Vectors;

    public class Day02 : BaseDay
    {
        /// <inheritdoc />
        public override string Part1()
        {
            List<Instruction> instructions = new List<Instruction>();

            foreach (string inputLine in Input.Split('\n'))
            {
                string[] lineParts = inputLine.Split(' ');
                int amount = int.Parse(lineParts[1]);
                Vector2Int direction = (lineParts[0]) switch

                {
                    "forward" => new Vector2Int(amount, 0),
                    "down" => new Vector2Int(0, amount),
                    "up" => new Vector2Int(0, -amount)
                };

                instructions.Add(new Instruction
                {
                    Direction = direction,
                    Amount = amount
                });
            }

            Vector2Int total = Vector2Int.Zero;
            foreach (Instruction instruction in instructions)
            {
                total += instruction.Direction;
            }

            return (total.X * total.Y).ToString();
        }

        /// <inheritdoc />
        public override string Part2()
        {
            List<Instruction> instructions = new List<Instruction>();

            foreach (string inputLine in Input.Split('\n'))
            {
                string[] lineParts = inputLine.Split(' ');
                int amount = int.Parse(lineParts[1]);
                Vector2Int direction = (lineParts[0]) switch

                {
                    "forward" => new Vector2Int(amount, 0),
                    "down" => new Vector2Int(0, amount),
                    "up" => new Vector2Int(0, -amount)
                };

                instructions.Add(new Instruction
                {
                    Direction = direction,
                    Amount = amount
                });
            }

            Vector2Int total = Vector2Int.Zero;
            int aim = 0;
            foreach (Instruction instruction in instructions)
            {
                aim += instruction.Direction.Y;
                if (instruction.Direction.X != 0)
                {
                    total += instruction.Direction;
                    total.Y += aim * instruction.Amount;
                }
            }

            return (total.X * total.Y).ToString();
        }

        public class Instruction
        {
            public Vector2Int Direction { get; set; }

            public int Amount { get; set; }
        }
    }
}