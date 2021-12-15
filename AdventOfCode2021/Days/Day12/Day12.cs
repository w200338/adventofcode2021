namespace AdventOfCode2021.Days.Day12
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Day12 : BaseDay
    {
        public override string Part1()
        {
            List<string> inputLines = Input.Split("\r\n").ToList();

            List<Node> nodes = new List<Node>();
            foreach (string inputLine in inputLines)
            {
                var parts = inputLine.Split('-');
                
                if (nodes.All(node => node.Name != parts[0]))
                {
                    nodes.Add(new Node
                    {
                        Name = parts[0],
                        IsBig = char.IsUpper(parts[0][0])
                    });
                }

                if (nodes.All(node => node.Name != parts[1]))
                {
                    nodes.Add(new Node
                    {
                        Name = parts[1],
                        IsBig = char.IsUpper(parts[1][0])
                    });
                }

                nodes.First(node => node.Name == parts[0]).Connections.Add(nodes.First(node => node.Name == parts[1]));
                nodes.First(node => node.Name == parts[1]).Connections.Add(nodes.First(node => node.Name == parts[0]));
            }

            List<List<Node>> paths = new List<List<Node>>();

            paths.Add(new List<Node>
            {
                nodes.First(node => node.Name == "start")
            });

            while (paths.Any(path => path.Last().Name != "end"))
            {
                foreach (List<Node> path in paths.Where(path => path.Last().Name != "end").ToList())
                {
                    paths.Remove(path);
                    foreach (Node node in path.Last().Connections.Where(connectionNode => connectionNode.IsBig || path.All(otherNodes => otherNodes.Name != connectionNode.Name)))
                    {
                        paths.Add(new List<Node>(path) { node });
                    }
                }
            }

            return paths.Count.ToString();
        }

        public override string Part2()
        {
            List<string> inputLines = Input.Split("\r\n").ToList();

            List<Node> nodes = new List<Node>();
            foreach (string inputLine in inputLines)
            {
                var parts = inputLine.Split('-');

                for (int i = 0; i < parts.Length; i++)
                {
                    if (nodes.All(node => node.Name != parts[i]))
                    {
                        nodes.Add(new Node
                        {
                            Name = parts[i],
                            IsBig = char.IsUpper(parts[i][0])
                        });

                        if (parts[i] == "start")
                        {
                            nodes.Last().Id = 0;
                        }
                        else if (parts[i] == "end")
                        {
                            nodes.Last().Id = int.MaxValue;
                        }
                    }
                }
                
                nodes.First(node => node.Name == parts[0]).Connections.Add(nodes.First(node => node.Name == parts[1]));
                nodes.First(node => node.Name == parts[1]).Connections.Add(nodes.First(node => node.Name == parts[0]));
            }

            List<List<Node>> paths = new List<List<Node>>();

            paths.Add(new List<Node>
            {
                nodes.First(node => node.Name == "start")
            });

            while (paths.Any(path => path.Last().Id != int.MaxValue))
            {
                foreach (List<Node> path in paths.Where(path => path.Last().Id != int.MaxValue).ToList())
                {
                    paths.Remove(path);
                    foreach (Node node in path.Last().Connections.Where(connectionNode => connectionNode.Id != 0 &&
                                                                        (connectionNode.IsBig || 
                                                                      (path.Where(node => !node.IsBig).GroupBy(node => node.Id).All(grouping => grouping.Count() != 2)) || 
                                                                      path.All(otherNodes => otherNodes.Id != connectionNode.Id))))
                    {
                        var newPath = new List<Node>(path) { node };
                        paths.Add(newPath);
                    }
                }
            }

            return paths.Count.ToString();
        }
    }

    public class Node
    {
        private static int idIndex = 1;

        public Node()
        {
            Id = idIndex;
            idIndex++;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsBig { get; set; }

        public List<Node> Connections { get; set; } = new List<Node>();
    }
}