namespace AdventOfCode2021.Days.Day15
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Tools.PathFinding.Dijkstra;

    public class Day15 : BaseDay
    {
        public override string Part1()
        {
            List<List<int>> input = Input.Split("\r\n").Select(line => line.ToCharArray().Select(c => c.ToString()).Select(int.Parse).ToList()).ToList();

            List<List<Node>> nodes = new List<List<Node>>();
            for (int i = 0; i < input.Count; i++)
            {
                nodes.Add(new List<Node>());
                for (int j = 0; j < input[0].Count; j++)
                {
                    nodes[i].Add(new Node
                    {
                        Risk = input[i][j],
                    });
                }
            }

            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[0].Count; j++)
                {
                    if (i > 0)
                    {
                        nodes[i - 1][j].Connections.Add(new DijkstraConnection
                        {
                            Distance = nodes[i][j].Risk,
                            Node = nodes[i][j]
                        });
                    }

                    if (j > 0)
                    {
                        nodes[i][j - 1].Connections.Add(new DijkstraConnection
                        {
                            Distance = nodes[i][j].Risk,
                            Node = nodes[i][j]
                        });
                    }

                    if (i < input.Count - 1)
                    {
                        nodes[i + 1][j].Connections.Add(new DijkstraConnection
                        {
                            Distance = nodes[i][j].Risk,
                            Node = nodes[i][j]
                        });
                    }

                    if (j < input[0].Count - 1)
                    {
                        nodes[i][j + 1].Connections.Add(new DijkstraConnection
                        {
                            Distance = nodes[i][j].Risk,
                            Node = nodes[i][j]
                        });
                    }
                }
            }

            List<Node> path = DijkstraSolver.Solve(nodes.SelectMany(nodeList => nodeList).Cast<DijkstraNode>().ToList(), nodes[0][0], nodes.Last().Last()).Cast<Node>().ToList();

            return path.Skip(1).Sum(node => node.Risk).ToString();
        }

        public override string Part2()
        {
            List<List<int>> input = Input.Split("\r\n").Select(line => line.ToCharArray().Select(c => c.ToString()).Select(int.Parse).ToList()).ToList();

            Node[][] nodes = new Node[input.Count * 5][];
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = new Node[input[0].Count * 5];
            }

            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[0].Count; j++)
                {
                    for (int expandY = 0; expandY < 5; expandY++) 
                    {
                        for (int expandX = 0; expandX < 5; expandX++)
                        {
                            int risk = (input[i][j] + expandX + expandY);
                            if (risk > 9)
                            {
                                risk -= 9;
                            }
                            nodes[expandY * input.Count + i][expandX * input[0].Count + j] = new Node
                            {
                                Risk = risk,
                            };
                        }
                    }
                }
            }

            for (int i = 0; i < nodes.Length; i++)
            {
                for (int j = 0; j < nodes[0].Length; j++)
                {
                    if (i > 0)
                    {
                        nodes[i - 1][j].Connections.Add(new DijkstraConnection
                        {
                            Distance = nodes[i][j].Risk,
                            Node = nodes[i][j]
                        });
                    }

                    if (j > 0)
                    {
                        nodes[i][j - 1].Connections.Add(new DijkstraConnection
                        {
                            Distance = nodes[i][j].Risk,
                            Node = nodes[i][j]
                        });
                    }

                    if (i < nodes.Length - 1)
                    {
                        nodes[i + 1][j].Connections.Add(new DijkstraConnection
                        {
                            Distance = nodes[i][j].Risk,
                            Node = nodes[i][j]
                        });
                    }

                    if (j < nodes[0].Length - 1)
                    {
                        nodes[i][j + 1].Connections.Add(new DijkstraConnection
                        {
                            Distance = nodes[i][j].Risk,
                            Node = nodes[i][j]
                        });
                    }
                }
            }

            List<Node> path = DijkstraSolver.Solve(nodes.SelectMany(nodeList => nodeList).Cast<DijkstraNode>().ToList(), nodes[0][0], nodes.Last().Last()).Cast<Node>().ToList();

            return path.Skip(1).Sum(node => node.Risk).ToString();
        }
    }

    public class Node : DijkstraNode
    {
        public int Risk { get; set; }
    }
}