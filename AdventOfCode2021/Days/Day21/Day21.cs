namespace AdventOfCode2021.Days.Day21
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Collections.Generic;

    public class Day21 : BaseDay
    {
        public override string Part1()
        {
            int player1Score = 0;
            int player1Space = 8;
            int player2Score = 0;
            int player2Space = 6;
            
            int dieRolls = 0;

            int dieCounter = 0;
            while (player1Score < 1000 && player2Score < 1000)
            {
                for (int i = 0; i < 3; i++)
                {
                    dieCounter++;
                    if (dieCounter > 100)
                    {
                        dieCounter = 1;
                    }
                    
                    player1Space += dieCounter;
                    while (player1Space > 10)
                    {
                        player1Space -= 10;
                    }

                    dieRolls++;
                }

                player1Score += player1Space;
                //Console.WriteLine($"player 1 on position {player1Space} score {player1Score}");

                if (player1Score >= 1000)
                {
                    break;
                }

                for (int i = 0; i < 3; i++)
                {
                    dieCounter++;
                    if (dieCounter > 100)
                    {
                        dieCounter = 1;
                    }

                    player2Space += dieCounter;
                    while (player2Space > 10)
                    {
                        player2Space -= 10;
                    }

                    dieRolls++;
                }

                player2Score += player2Space;
                //Console.WriteLine($"player 2 on position {player2Space} score {player2Score}");

                if (player2Score >= 1000)
                {
                    break;
                }
            }

            return (dieRolls * (player1Score < 1000 ? player1Score : player2Score)).ToString();
        }

        public override string Part2()
        {
            List<int> uniqueDieCombination = new List<int>();

            for (int i = 1; i < 4; i++)
            {
                for (int j = 1; j < 4; j++)
                {
                    for (int k = 1; k < 4; k++)
                    {
                        uniqueDieCombination.Add(i + j + k);
                    }
                }
            }

            Dictionary<int, int> dieCombinations = uniqueDieCombination.GroupBy(combo => combo).ToDictionary(group => group.Key, group => group.Count());

            Dictionary<Universe, long> seenUniverses = new Dictionary<Universe, long>
            {
                { new Universe(0, 8, 0, 6), 1 }
            };

            long player1Wins = 0;
            long player2Wins = 0;

            while (seenUniverses.Count > 0)
            {
                Dictionary<Universe, long> newUniverses = new Dictionary<Universe, long>();
                foreach (KeyValuePair<Universe, long> universeKvp in seenUniverses)
                {
                    Universe universe = universeKvp.Key;
                    foreach (var dieCombination in dieCombinations)
                    {
                        int player1Pos = (universe.Player1Position + dieCombination.Key - 1) % 10 + 1;
                        int player1Score = universe.Player1Score + player1Pos;

                        if (player1Score >= 21)
                        {
                            player1Wins += universeKvp.Value * dieCombination.Value;
                            continue;
                        }

                        foreach (var player2DieCombo in dieCombinations)
                        {
                            int player2Pos = (universe.Player2Position + player2DieCombo.Key - 1) % 10 + 1;
                            int player2Score = universe.Player2Score + player2Pos;

                            if (player2Score >= 21)
                            {
                                player2Wins += universeKvp.Value * dieCombination.Value * player2DieCombo.Value;
                                continue;
                            }

                            Universe newUniverse = new Universe((byte)player1Score, (byte)player1Pos, (byte)player2Score, (byte)player2Pos);

                            if (newUniverses.ContainsKey(newUniverse))
                            {
                                newUniverses[newUniverse] += universeKvp.Value * dieCombination.Value * player2DieCombo.Value;
                            }
                            else
                            {
                                newUniverses[newUniverse] = universeKvp.Value * dieCombination.Value * player2DieCombo.Value;
                            }
                        }
                    }
                }

                seenUniverses = newUniverses;
            }

            return (player1Wins > player2Wins ? player1Wins : player2Wins).ToString();
        }

        public readonly struct Universe
        {
            public byte Player1Score { get; }

            public byte Player1Position { get; }

            public byte Player2Score { get; }

            public byte Player2Position { get; }

            public Universe(byte player1Score, byte player1Position, byte player2Score, byte player2Position)
            {
                Player1Score = player1Score;
                Player1Position = player1Position;
                Player2Score = player2Score;
                Player2Position = player2Position;
            }

            public bool Equals(Universe other)
            {
                return Player1Score == other.Player1Score && Player1Position == other.Player1Position && Player2Score == other.Player2Score && Player2Position == other.Player2Position;
            }

            /// <inheritdoc />
            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                return obj.GetType() == GetType() && Equals((Universe)obj);
            }

            /// <inheritdoc />
            public override int GetHashCode()
            {
                return HashCode.Combine(Player1Score, Player1Position, Player2Score, Player2Position);
            }
        }
    }
}