namespace AdventOfCode2021.Days.Day04
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class Day04 : BaseDay
    {
        /// <inheritdoc />
        public override string Part1()
        {
            Queue<string> inputLines = new Queue<string>(Input.Split("\r\n").ToList());
            List<int> numbers = inputLines.Dequeue().Split(',').Select(int.Parse).ToList();

            List<BingoCard> cards = new List<BingoCard>();

            while (inputLines.Count > 0)
            {
                BingoCard card = new BingoCard();

                inputLines.Dequeue(); // skip empty line

                for (int i = 0; i < 5; i++)
                {
                    card.Numbers[i] = inputLines.Dequeue().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                }

                cards.Add(card);
            }

            foreach (int number in numbers)
            {
                foreach (BingoCard bingoCard in cards)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            if (!bingoCard.Marked[i][j])
                            {
                                if (bingoCard.Numbers[i][j] == number)
                                {
                                    bingoCard.Marked[i][j] = true;

                                    if (bingoCard.HasWon())
                                    {
                                        int sumOfUnmarked = bingoCard.SumOfUnmarked();
                                        return (sumOfUnmarked * number).ToString();
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return string.Empty;
        }

        /// <inheritdoc />
        public override string Part2()
        {
            Queue<string> inputLines = new Queue<string>(Input.Split("\r\n").ToList());
            List<int> numbers = inputLines.Dequeue().Split(',').Select(int.Parse).ToList();

            List<BingoCard> cards = new List<BingoCard>();

            while (inputLines.Count > 0)
            {
                BingoCard card = new BingoCard();

                inputLines.Dequeue(); // skip empty line

                for (int i = 0; i < 5; i++)
                {
                    card.Numbers[i] = inputLines.Dequeue().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                }

                cards.Add(card);
            }

            List<int> winningScores = new List<int>(cards.Count);
            foreach (int number in numbers)
            {
                foreach (BingoCard bingoCard in cards)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            if (!bingoCard.Marked[i][j])
                            {
                                if (bingoCard.Numbers[i][j] == number)
                                {
                                    bingoCard.Marked[i][j] = true;

                                    if (bingoCard.HasWon())
                                    {
                                        int sumOfUnmarked = bingoCard.SumOfUnmarked();
                                        winningScores.Add(sumOfUnmarked * number);
                                    }
                                }
                            }
                        }
                    }
                }

                cards = cards.Where(card => !card.HasWon()).ToList();
            }

            return winningScores.Last().ToString();
        }

        public class BingoCard
        {
            public int[][] Numbers { get; set; } = new int[5][];

            public bool[][] Marked { get; set; }

            public BingoCard()
            {
                Marked = new bool[5][]
                {
                    new bool[5],
                    new bool[5],
                    new bool[5],
                    new bool[5],
                    new bool[5]
                };
            }

            public int SumOfUnmarked()
            {
                int sum = 0;
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (!Marked[i][j])
                        {
                            sum += Numbers[i][j];
                        }
                    }
                }

                return sum;
            }

            public bool HasWon()
            {
                for (int i = 0; i < 5; i++)
                {
                    bool hasWonVertical = true;
                    bool hasWonHorizontal = true;
                    for (int j = 0; j < 5; j++)
                    {
                        hasWonVertical &= Marked[i][j];
                        hasWonHorizontal &= Marked[j][i];
                    }

                    if (hasWonVertical || hasWonHorizontal)
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}