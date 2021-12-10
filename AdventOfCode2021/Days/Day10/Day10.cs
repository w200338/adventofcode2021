namespace AdventOfCode2021.Days.Day10
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Day10 : BaseDay
    {
        public override string Part1()
        {
            Dictionary<char, int> bracketScoreDictionary = new Dictionary<char, int>
            {
                { ')', 3 },
                { ']', 57 },
                { '}', 1197 },
                { '>', 25137 },
                { '(', 3 },
                { '[', 57 },
                { '{', 1197 },
                { '<', 25137 },
            };

            List<string> inputLines = Input.Split("\r\n").ToList();

            int score = 0;
            foreach (string inputLine in inputLines)
            {
                Stack<char> openBracketStack = new Stack<char>();
                foreach (char c in inputLine)
                {
                    if (c == '(' || c == '[' || c == '{' || c == '<')
                    {
                        openBracketStack.Push(c);
                    }
                    else
                    {
                        char lastOpenBracket = openBracketStack.Peek();
                        if (lastOpenBracket == '(' && c == ')' ||
                            lastOpenBracket == '[' && c == ']' ||
                            lastOpenBracket == '{' && c == '}' ||
                            lastOpenBracket == '<' && c == '>')
                        {
                            openBracketStack.Pop();
                        }
                        else
                        {
                            score += bracketScoreDictionary[c];
                            break;
                        }
                    }
                }
            }

            return score.ToString();
        }

        public override string Part2()
        {
            Dictionary<char, int> bracketScoreDictionary = new Dictionary<char, int>
            {
                { ')', 1 },
                { ']', 2 },
                { '}', 3 },
                { '>', 4 },
                { '(', 1 },
                { '[', 2 },
                { '{', 3 },
                { '<', 4 },
            };

            List<string> inputLines = Input.Split("\r\n").ToList();

            List<long> scores = new List<long>();
            foreach (string inputLine in inputLines)
            {
                Stack<char> openBracketStack = new Stack<char>();
                bool isCorrupt = false;
                foreach (char c in inputLine)
                {
                    if (c == '(' || c == '[' || c == '{' || c == '<')
                    {
                        openBracketStack.Push(c);
                    }
                    else
                    {
                        char lastOpenBracket = openBracketStack.Peek();
                        if (lastOpenBracket == '(' && c == ')' ||
                            lastOpenBracket == '[' && c == ']' ||
                            lastOpenBracket == '{' && c == '}' ||
                            lastOpenBracket == '<' && c == '>')
                        {
                            openBracketStack.Pop();
                        }
                        else
                        {
                            isCorrupt = true;
                            break;
                        }
                    }
                }

                if (!isCorrupt)
                {
                    long score = 0;
                    while (openBracketStack.Count > 0)
                    {
                        score *= 5;
                        score += bracketScoreDictionary[openBracketStack.Pop()];
                    }

                    scores.Add(score);
                }
            }

            scores = scores.OrderBy(s => s).ToList();
            return scores[(scores.Count - 1) / 2].ToString();
        }
    }
}