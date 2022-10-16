namespace AdventOfCode2021.Days.Day23
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Tools.Mathematics.Vectors;

    public class Day23 : BaseDay
    {
        private static List<Vector2Int> hallwaySpots = new List<Vector2Int>
        {
            new Vector2Int(1, 1),
            new Vector2Int(2, 1),
            new Vector2Int(4, 1),
            new Vector2Int(6, 1),
            new Vector2Int(8, 1),
            new Vector2Int(10, 1),
            new Vector2Int(11, 1),
            new Vector2Int(12, 1)
        };

        private static List<Vector2Int> allSpots = new List<Vector2Int>(hallwaySpots)
        {
            new Vector2Int(3, 1),
            new Vector2Int(3, 2),
            new Vector2Int(3, 3),
            new Vector2Int(5, 1),
            new Vector2Int(5, 2),
            new Vector2Int(5, 3),
            new Vector2Int(7, 1),
            new Vector2Int(7, 2),
            new Vector2Int(7, 3),
            new Vector2Int(9, 1),
            new Vector2Int(9, 2),
            new Vector2Int(9, 3)
        };

        private static List<Vector2Int> forbiddenSpots = new List<Vector2Int>
        {
            new Vector2Int(3, 1),
            new Vector2Int(5, 1),
            new Vector2Int(7, 1),
            new Vector2Int(9, 1)
        };

        public override string Part1()
        {
            string[] inputLines = Input.Split("\r\n");

            Dictionary<Vector2Int, char> amphipodsLocations = new Dictionary<Vector2Int, char>();

            for (var i = 0; i < inputLines.Length; i++)
            {
                string inputLine = inputLines[i];

                for (int j = 0; j < inputLine.Length; j++)
                {
                    if (char.IsUpper(inputLine[j]))
                    {
                        amphipodsLocations.Add(new Vector2Int(j, i), inputLine[j]);
                    }
                }
            }

            State initialState = new State
            {
                Positions = amphipodsLocations,
                EnergyUsed = 0
            };

            SortedSet<State> statesToDo = new SortedSet<State>();
            statesToDo.Add(initialState);

            while (statesToDo.Count > 0)
            {
                State lowestEnergyState = statesToDo.Min;
                statesToDo.Remove(lowestEnergyState);

                if (lowestEnergyState.EnergyUsed > 20_000)
                {
                    return "Failed to find solution";
                }
                
                foreach (KeyValuePair<Vector2Int, char> kvp in lowestEnergyState.Positions)
                {
                    if (kvp.Value == 'A' && kvp.Key == new Vector2Int(3, 3))
                    {
                        continue;
                    }

                    if (kvp.Value == 'B' && kvp.Key == new Vector2Int(5, 3))
                    {
                        continue;
                    }

                    if (kvp.Value == 'C' && kvp.Key == new Vector2Int(7, 3))
                    {
                        continue;
                    }

                    if (kvp.Value == 'D' && kvp.Key == new Vector2Int(9, 3))
                    {
                        continue;
                    }

                    foreach (Vector2Int move in GetMoves(lowestEnergyState.Positions, kvp.Key))
                    {
                        if (move.Y == 3)
                        {
                            switch (move.X)
                            {
                                case 3:
                                    if (kvp.Value != 'A') continue;
                                    break;

                                case 5:
                                    if (kvp.Value != 'B') continue;
                                    break;

                                case 7:
                                    if (kvp.Value != 'C') continue;
                                    break;

                                case 9:
                                    if (kvp.Value != 'D') continue;
                                    break;
                            }
                        }

                        Dictionary<Vector2Int, char> newPositions = lowestEnergyState.Positions.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                        newPositions.Remove(kvp.Key);
                        newPositions.Add(move, kvp.Value);

                        int distanceTraveled = Vector2Int.DistanceManhattan(kvp.Key, move);
                        int energyMultiplier = kvp.Value switch
                        {
                            'A' => 1,
                            'B' => 10,
                            'C' => 100,
                            'D' => 1000
                        };

                        var newState = new State
                        {
                            Positions = newPositions,
                            EnergyUsed = lowestEnergyState.EnergyUsed + distanceTraveled * energyMultiplier
                        };

                        if (newState.IsSolved())
                        {
                            return newState.EnergyUsed.ToString();
                        }

                        statesToDo.Add(newState);
                    }
                }
            }

            return string.Empty;
        }

        public override string Part2()
        {
            return string.Empty;
        }

        private List<Vector2Int> GetMoves(Dictionary<Vector2Int, char> amphipodLocations, Vector2Int startLocation)
        {
            // top
            List<Vector2Int> output = new List<Vector2Int>();
            if (startLocation.Y == 1)
            {
                for (int i = 3; i <= 9; i += 2)
                {
                    Vector2Int bottomPos = new Vector2Int(i, 3);
                    if (!amphipodLocations.ContainsKey(bottomPos))
                    {
                        output.Add(bottomPos);
                    }
                    else
                    {
                        Vector2Int topPos = new Vector2Int(i, 2);
                        if (!amphipodLocations.ContainsKey(topPos))
                        {
                            output.Add(topPos);
                        }
                    }
                }
            }
            // in slots
            else
            {

            }

            return output;

            /*
            HashSet<Vector2Int> output = new HashSet<Vector2Int>();

            Queue<Vector2Int> spotsToSearch = new Queue<Vector2Int>();
            spotsToSearch.Enqueue(startLocation);

            while (spotsToSearch.Count > 0)
            {
                Vector2Int spot = spotsToSearch.Dequeue();
                List<Vector2Int> adjacentSpots = allSpots
                    .Except(output)
                    .Where(s => s.DistanceManhattan(spot) == 1)
                    .Where(s => !amphipodLocations.ContainsKey(s))
                    .ToList();

                foreach (Vector2Int adjacentSpot in adjacentSpots)
                {
                    spotsToSearch.Enqueue(adjacentSpot);
                    output.Add(adjacentSpot);
                }
            }

            return output.Except(forbiddenSpots).ToList();
            */
            
            /*
            var output = new List<Vector2Int>();
            switch (startLocation.Y)
            {
                case 3: // Bottom -> top
                    // blocked by other above
                    if (amphipodLocations.Any(loc => loc.Key.Y == 2 && loc.Key.X == startLocation.X))
                    {
                        return output;
                    }

                    //output.Add(new Vector2Int(startLocation.X, 2));


                    goto case 2;
                    break;

                case 2: // Middle -> out
                    if (!amphipodLocations.Any(loc => loc.Key.Y == 1 && loc.Key.X == startLocation.X - 1))
                    {
                        output.Add(new Vector2Int(startLocation.X - 1, 1));
                    }

                    if (!amphipodLocations.Any(loc => loc.Key.Y == 1 && loc.Key.X == startLocation.X + 1))
                    {
                        output.Add(new Vector2Int(startLocation.X + 1, 1));
                    }

                    //if (startLocation.Y != 3 && !amphipodLocations.Any(loc => loc.Key.Y == 3 && loc.Key.X == startLocation.X))
                    //{
                    //    output.Add(new Vector2Int(startLocation.X, 3));
                    //}

                    for (int i = startLocation.X; i <= 11; i++)
                    {
                        if (hallwaySpots.Any(loc => loc.X == i) && !amphipodLocations.Any(loc => loc.Key.Y == 1 && loc.Key.X == i))
                        {
                            output.Add(new Vector2Int(i, 1));
                        }
                    }

                    for (int i = startLocation.X; i >= 1; i--)
                    {
                        if (hallwaySpots.Any(loc => loc.X == i) && !amphipodLocations.Any(loc => loc.Key.Y == 1 && loc.Key.X == i))
                        {
                            output.Add(new Vector2Int(i, 1));
                        }
                    }

                    break;
                case 1: // Hallway -> room
                    for (int i = startLocation.X; i <= 11; i++)
                    {
                        if (i > 2 && i < 10 && i % 2 == 1 && !amphipodLocations.Any(loc => loc.Key.Y == 2 && loc.Key.X == i))
                        {
                            if (!amphipodLocations.Any(loc => loc.Key.Y == 3 && loc.Key.X == i))
                            {
                                output.Add(new Vector2Int(i, 3));
                            }
                            else
                            {
                                output.Add(new Vector2Int(i, 2));
                            }
                        }
                    }

                    for (int i = startLocation.X; i >= 1; i--)
                    {
                        if (i < 10 && i > 2 && i % 2 == 1 && !amphipodLocations.Any(loc => loc.Key.Y == 2 && loc.Key.X == i))
                        {
                            if (!amphipodLocations.Any(loc => loc.Key.Y == 3 && loc.Key.X == i))
                            {
                                output.Add(new Vector2Int(i, 3));
                            }
                            else
                            {
                                output.Add(new Vector2Int(i, 2));
                            }
                        }
                    }

                    break;
            }
            */

            return output;
        }
    }

    public class State : IComparable<State>
    {
        public int EnergyUsed { get; set; }

        public Dictionary<Vector2Int, char> Positions { get; set; }

        public bool IsSolved()
        {
            Positions.TryGetValue(new Vector2Int(3, 2), out char topA);
            if (topA != 'A') return false;

            Positions.TryGetValue(new Vector2Int(3, 3), out char bottomA);
            if (bottomA != 'A') return false;

            Positions.TryGetValue(new Vector2Int(5, 2), out char topB);
            if (topB != 'B') return false;

            Positions.TryGetValue(new Vector2Int(5, 3), out char bottomB);
            if (bottomB != 'B') return false;

            Positions.TryGetValue(new Vector2Int(7, 2), out char topC);
            if (topC != 'C') return false;

            Positions.TryGetValue(new Vector2Int(7, 3), out char bottomC);
            if (bottomC != 'C') return false;

            Positions.TryGetValue(new Vector2Int(9, 2), out char topD);
            if (topD != 'D') return false;

            Positions.TryGetValue(new Vector2Int(9, 3), out char bottomD);
            if (bottomD != 'D') return false;

            return true;
            /*
            return (Positions[new Vector2Int(3, 2)] == 'A' && Positions[new Vector2Int(3, 3)] == 'A') &&
                   (Positions[new Vector2Int(5, 2)] == 'B' && Positions[new Vector2Int(5, 3)] == 'B') &&
                   (Positions[new Vector2Int(7, 2)] == 'C' && Positions[new Vector2Int(7, 3)] == 'C') &&
                   (Positions[new Vector2Int(9, 2)] == 'D' && Positions[new Vector2Int(9, 3)] == 'D');
            */
        }

        protected bool Equals(State other)
        {
            return EnergyUsed == other.EnergyUsed && Equals(Positions, other.Positions);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((State)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return HashCode.Combine(EnergyUsed, Positions);
        }

        /// <inheritdoc />
        public int CompareTo(State other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return EnergyUsed.CompareTo(other.EnergyUsed);
        }
    }
}