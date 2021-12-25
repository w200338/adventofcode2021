namespace AdventOfCode2021.Days.Day25
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class Day25 : BaseDay
    {
        public override string Part1()
        {
            char[][] inputLines = Input.Split("\r\n").Select(i => i.ToCharArray()).ToArray();
            bool allSame = false;
            int steps = 0;

            while (!allSame)
            {
                steps++;
                char[][] start = inputLines.Select(i => i.ToArray()).ToArray();
                char[][] current = inputLines.Select(i => i.ToArray()).ToArray();

                // left -> right
                for (int i = 0; i < inputLines.Length; i++)
                {
                    for (int j = 0; j < inputLines[0].Length - 1; j++)
                    {
                        if (inputLines[i][j] == '>' && inputLines[i][j + 1] == '.')
                        {
                            current[i][j] = '.';
                            current[i][j + 1] = '>';
                        }
                    }

                    // wrap
                    if (inputLines[i][0] == '.' && inputLines[i][inputLines[0].Length - 1] == '>')
                    {
                        current[i][inputLines[0].Length - 1] = '.';
                        current[i][0] = '>';
                    }
                }

                inputLines = current.Select(i => i.ToArray()).ToArray();

                // top -> bottom
                for (int i = 0; i < inputLines[0].Length; i++)
                {
                    for (int j = 0; j < inputLines.Length - 1; j++)
                    {
                        if (inputLines[j][i] == 'v' && inputLines[j + 1][i] == '.')
                        {
                            current[j][i] = '.';
                            current[j + 1][i] = 'v';
                        }
                    }

                    if (inputLines[0][i] == '.' && inputLines[inputLines.Length - 1][i] == 'v')
                    {
                        current[inputLines.Length - 1][i] = '.';
                        current[0][i] = 'v';
                    }
                }

                allSame = true;
                Console.WriteLine(steps);
                for (int i = 0; i < current.Length; i++)
                {
                    for (int j = 0; j < current[0].Length; j++)
                    {
                        if (start[i][j] != current[i][j])
                        {
                            allSame = false;
                            break;
                        }
                    }

                    //Console.WriteLine(new string(current[i]));
                }

                inputLines = current;

                /*
                // left to right
                for (var i = 0; i < inputLines.Length; i++)
                {
                    var inputLine = inputLines[i].ToCharArray();
                    for (int j = 0; j < inputLine.Length - 1; j++)
                    {
                        if (inputLine[j] == '>' && inputLine[j + 1] == '.')
                        {
                            inputLine[j] = '.';
                            inputLine[j + 1] = '>';
                        }
                    }

                    // wrap screen
                    if (inputLine[inputLine.Length - 1] == '>' && inputLine[0] == '.')
                    {
                        inputLine[inputLine.Length - 1] = '.';
                        inputLine[0] = '>';
                    }

                    inputLines[i] = new string(inputLine);
                }

                // top to bottom
                for (int i = 0; i < inputLines[0].Length; i++)
                {
                    for (int j = 0; j < inputLines.Length - 1; j++)
                    {
                        if (inputLines[j][i] == 'v' && inputLines[j + 1][i] == '.')
                        {
                            var nextLine = inputLines[j + 1].ToCharArray();
                            nextLine[i] = 'v';
                            inputLines[j + 1] = new string(nextLine);

                            var prevLine = inputLines[j].ToCharArray();
                            prevLine[i] = '.';
                            inputLines[j] = new string(nextLine);
                        }
                    }

                    if (inputLines[inputLines.Length - 1][i] == 'v' && inputLines[0][i] == '.')
                    {
                        var nextLine = inputLines[0].ToCharArray();
                        nextLine[i] = 'v';
                        inputLines[0] = new string(nextLine);

                        var prevLine = inputLines[inputLines.Length - 1].ToCharArray();
                        prevLine[i] = '.';
                        inputLines[inputLines.Length - 1] = new string(nextLine);
                    }
                }

                allSame = true;
                for (int i = 0; i < inputLines.Length; i++)
                {
                    if (inputLines[i] != currentInputs[i])
                    {
                        allSame = false;
                    }

                    Console.WriteLine(inputLines[i]);
                }
                */
            }
            
            return steps.ToString();
        }

        public override string Part2()
        {
            return string.Empty;
        }
    }
}