namespace AdventOfCode2021.Days.Day24
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Day24 : BaseDay
    {
        public override string Part1()
        {
            string[] inputLines = Input.Split("\r\n");
            List<CycleVariables> cycles = new List<CycleVariables>();

            for (int i = 0; i < 14; i++)
            {
                int divider = int.Parse(inputLines[(i * 18) + 4].Split(' ')[2]);
                int unknown1 = int.Parse(inputLines[(i * 18) + 5].Split(' ')[2]);
                int unknown2 = int.Parse(inputLines[(i * 18) + 15].Split(' ')[2]);

                var cycle = new CycleVariables(i, unknown1, unknown2, divider);
                cycles.Add(cycle);
            }

            // push 4x
            // pop 2x
            // push 2x
            // pop 2x
            // push 1x
            // pop 3x
            var cycleStack = new Stack<CycleVariables>();
            char[] output = new string('0', 14).ToCharArray();
            foreach (CycleVariables currentCycle in cycles)
            {
                if (currentCycle.Divider == 1)
                {
                    cycleStack.Push(currentCycle);
                }
                else
                {
                    CycleVariables mirrorCycle = cycleStack.Pop();
                    int offsets = currentCycle.Unknown1 + mirrorCycle.Unknown2;
                    int largestCurrent = 0;
                    int largestMirror = 0;

                    for (int i = 1; i < 10; i++)
                    {
                        int otherNumber = i + offsets;
                        if (otherNumber > 0 && otherNumber < 10)
                        {
                            if (otherNumber > largestCurrent)
                            {
                                largestCurrent = otherNumber;
                                largestMirror = i;
                            }
                        }
                    }

                    output[currentCycle.Index] = largestCurrent.ToString()[0];
                    output[mirrorCycle.Index] = largestMirror.ToString()[0];
                }
            }

            //return new string(output);

            /*
            var numbers = "13579246899999";

            for (int i = 1; i < 14; i++)
            {
                int z = 0;
                for (int j = 0; j < cycles.Count; j++)
                {
                    CycleVariables cycle = cycles[j];
                    z = ProgramRewrite(int.Parse(numbers[i].ToString()), z, cycle.Unknown1, cycle.Unknown2, cycle.Divider);
                }

                Console.WriteLine("Z = " + z);
                if (z == 0)
                {
                    Console.WriteLine("Z = " + z);
                }
            }
            */

            // check output the old fashioned way
            var numbers = new string(output);

            Dictionary<char, int> registers = ExecuteProgram(inputLines, numbers);

            if (registers['z'] == 0)
            {
                Console.WriteLine("Solution: " + numbers);
            }

            return numbers;
        }

        private static Dictionary<char, int> ExecuteProgram(string[] inputLines, string numbers)
        {
            int numberIndex = 0;
            Dictionary<char, int> registers = new Dictionary<char, int>
            {
                { 'w', 0 },
                { 'x', 0 },
                { 'y', 0 },
                { 'z', 0 },
            };

            for (var i = 0; i < inputLines.Length; i++)
            {
                string inputLine = inputLines[i];
                string[] instructionParts = inputLine.Split(' ');

                switch (instructionParts[0])
                {
                    case "inp":
                        registers[instructionParts[1][0]] = int.Parse(numbers[numberIndex++].ToString());
                        break;

                    case "add":
                        registers[instructionParts[1][0]] += char.IsDigit(instructionParts[2][0]) || instructionParts[2][0] == '-' ? int.Parse(instructionParts[2]) : registers[instructionParts[2][0]];
                        break;

                    case "mul":
                        registers[instructionParts[1][0]] *= char.IsDigit(instructionParts[2][0]) || instructionParts[2][0] == '-' ? int.Parse(instructionParts[2]) : registers[instructionParts[2][0]];
                        break;

                    case "div":
                        registers[instructionParts[1][0]] = (int)Math.Floor((double)registers[instructionParts[1][0]] / (char.IsDigit(instructionParts[2][0]) || instructionParts[2][0] == '-' ? int.Parse(instructionParts[2]) : registers[instructionParts[2][0]]));
                        break;

                    case "mod":
                        registers[instructionParts[1][0]] %= char.IsDigit(instructionParts[2][0]) || instructionParts[2][0] == '-' ? int.Parse(instructionParts[2]) : registers[instructionParts[2][0]];
                        break;

                    case "eql":
                        registers[instructionParts[1][0]] = registers[instructionParts[1][0]] == (char.IsDigit(instructionParts[2][0]) || instructionParts[2][0] == '-' ? int.Parse(instructionParts[2]) : registers[instructionParts[2][0]]) ? 1 : 0;
                        break;
                }

                if (i % 18 == 0)
                {
                    Console.WriteLine("Z = " + registers['z']);
                }
            }

            return registers;
        }

        private static int ProgramRewrite(int input, int z, int unknown1, int unknown2, int divider)
        {
            // X is only 1 on lines where unknown1 is greater than 10, which is where divider is always 1
            // looks like a number system using base 26
            // works like a stack, pushing (div z 1) and popping numbers (div z 26)
            // => pushed numbers must be popped by making 'x = z % 26 + unknown1 != input ? 1 : 0' equal to 0 where divider is 26
            // => oldInput + unknown2 = currentInput - unknown1
            z *= 26;
            z += input + unknown2;

            /* rewritten in function of Z
            int x = z % 26 + unknown1 != input ? 1 : 0;
            z /= divider;
            z *= 25 * x + 1;
            z += (input * unknown2) * x;
            */

            /* compacted
            int x = ((z % 26) + unknown1) != input ? 1 : 0;
            int y = 25 * x + 1;
            z = z / divider * y;
            y = (input + unknown2) * x;
            z = z + y;
            */

            // attempt to throw it all together
            //z = (z * (25 * (((z % 26) + unknown1) != input ? 1 : 0) + 1)) / divider + (input + unknown2) * (((z % 26) + unknown1) != input ? 1 : 0);

            /* single instruction set
            int x = 0, y = 0, w = 0;
            x *= 0;
            x += z;
            x %= 26;
            z /= divider;
            x += unknown1;
            x = x == w ? 1 : 0;
            x = x == 0 ? 1 : 0;
            y *= 0;
            y += 25;
            y *= x;
            y += 1;
            z *= y;
            y *= 0;
            y += input;
            y += unknown2;
            y *= x;
            z += y;
            */

            return z;
        }

        public override string Part2()
        {
            string[] inputLines = Input.Split("\r\n");
            List<CycleVariables> cycles = new List<CycleVariables>();

            for (int i = 0; i < 14; i++)
            {
                int divider = int.Parse(inputLines[(i * 18) + 4].Split(' ')[2]);
                int unknown1 = int.Parse(inputLines[(i * 18) + 5].Split(' ')[2]);
                int unknown2 = int.Parse(inputLines[(i * 18) + 15].Split(' ')[2]);

                var cycle = new CycleVariables(i, unknown1, unknown2, divider);
                cycles.Add(cycle);
            }

            // push 4x
            // pop 2x
            // push 2x
            // pop 2x
            // push 1x
            // pop 3x
            var cycleStack = new Stack<CycleVariables>();
            char[] output = new string('0', 14).ToCharArray();
            foreach (CycleVariables currentCycle in cycles)
            {
                if (currentCycle.Divider == 1)
                {
                    cycleStack.Push(currentCycle);
                }
                else
                {
                    CycleVariables mirrorCycle = cycleStack.Pop();
                    int offsets = currentCycle.Unknown1 + mirrorCycle.Unknown2;
                    int largestCurrent = 9;
                    int largestMirror = 9;

                    for (int i = 10; i >= 1; i--)
                    {
                        int otherNumber = i + offsets;
                        if (otherNumber > 0 && otherNumber < 10)
                        {
                            if (otherNumber < largestCurrent)
                            {
                                largestCurrent = otherNumber;
                                largestMirror = i;
                            }
                        }
                    }

                    output[currentCycle.Index] = largestCurrent.ToString()[0];
                    output[mirrorCycle.Index] = largestMirror.ToString()[0];
                }
            }

            return new string(output);
        }
    }

    public readonly struct CycleVariables
    {
        public int Index { get; }
        public int Unknown1 { get; }

        public int Unknown2 { get; }

        public int Divider { get; }

        public CycleVariables(int index, int unknown1, int unknown2, int divider)
        {
            Index = index;
            Unknown1 = unknown1;
            Unknown2 = unknown2;
            Divider = divider;
        }
    }

    public readonly struct State
    {
        public int Cycle { get; }

        public int ZRegister { get; }

        public long InputsSoFar { get; }

        public State(int cycle, int zRegister, long inputsSoFar)
        {
            Cycle = cycle;
            ZRegister = zRegister;
            InputsSoFar = inputsSoFar;
        }
    }
}