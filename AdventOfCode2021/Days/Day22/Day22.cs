﻿namespace AdventOfCode2021.Days.Day22
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Tools.Mathematics._3DShapes;
    using Tools.Mathematics.Vectors;

    public class Day22 : BaseDay
    {
        private static readonly Regex CoordinateRegex = new Regex(@"(-?\d+)\.\.(-?\d+).*?(-?\d+)\.\.(-?\d+).*?(-?\d+)\.\.(-?\d+)$", RegexOptions.Compiled);

        public override string Part1()
        {
            string[] inputLines = Input.Split("\r\n");

            List<BeamInt> beams = new List<BeamInt>();
            Dictionary<BeamInt, bool> regionState = new Dictionary<BeamInt, bool>();
            foreach (string inputLine in inputLines)
            {
                Match match = CoordinateRegex.Match(inputLine);

                int xMin = int.Parse(match.Groups[1].Value);
                int yMin = int.Parse(match.Groups[3].Value);
                int zMin = int.Parse(match.Groups[5].Value);

                int xMax = int.Parse(match.Groups[2].Value);
                int yMax = int.Parse(match.Groups[4].Value);
                int zMax = int.Parse(match.Groups[6].Value);

                var beam = new BeamInt(new Vector3Int(xMin, yMin, zMin), new Vector3Int(xMax - xMin, yMax - yMin, zMax - zMin));
                beams.Add(beam);
                regionState.Add(beam, inputLine.StartsWith("on"));
            }

            Dictionary<Vector3Int, bool> onOffDictionary = new Dictionary<Vector3Int, bool>();

            foreach (BeamInt beam in beams)
            {
                if (beam.Position.X < -50 || beam.Position.X > 50)
                {
                    continue;
                }

                bool state = regionState[beam];
                for (int x = beam.Position.X; x <= beam.Position.X + beam.Size.X; x++)
                {
                    for (int y = beam.Position.Y; y <= beam.Position.Y + beam.Size.Y; y++)
                    {
                        for (int z = beam.Position.Z; z <= beam.Position.Z + beam.Size.Z; z++)
                        {
                            onOffDictionary[new Vector3Int(x, y, z)] = state;
                        }
                    }
                }
            }

            int count = onOffDictionary.Count(kvp => kvp.Value);

            return count.ToString();
        }

        public override string Part2()
        {
            string[] inputLines = Input.Split("\r\n");

            List<(BeamInt, bool)> slices = new List<(BeamInt, bool)>();
            foreach (string inputLine in inputLines)
            {
                Match match = CoordinateRegex.Match(inputLine);

                int xMin = int.Parse(match.Groups[1].Value);
                int yMin = int.Parse(match.Groups[3].Value);
                int zMin = int.Parse(match.Groups[5].Value);

                int xMax = int.Parse(match.Groups[2].Value) + 1;
                int yMax = int.Parse(match.Groups[4].Value) + 1;
                int zMax = int.Parse(match.Groups[6].Value) + 1;

                var currentBeam = new BeamInt(new Vector3Int(xMin, yMin, zMin), new Vector3Int(xMax, yMax, zMax));
                bool state = inputLine.StartsWith("on");

                // calculate overlap with other beams
                List<(BeamInt, bool)> createdSlices = new List<(BeamInt, bool)>();
                foreach ((BeamInt otherBeam, bool otherState) in slices)
                {
                    if ((xMax > otherBeam.Position.X && xMin < otherBeam.Size.X) &&
                        (yMax > otherBeam.Position.Y && yMin < otherBeam.Size.Y) &&
                        (zMax > otherBeam.Position.Z && zMin < otherBeam.Size.Z))
                    {
                        // slice it into pieces if it overlaps
                        if (otherBeam.Position.X < xMin)
                        {
                            var newBeam = new BeamInt(
                                new Vector3Int(
                                    otherBeam.Position.X,
                                    otherBeam.Position.Y,
                                    otherBeam.Position.Z),
                                new Vector3Int(
                                    xMin,
                                    otherBeam.Size.Y,
                                    otherBeam.Size.Z));

                            createdSlices.Add((newBeam, otherState));

                            otherBeam.Position.X = xMin;
                        }

                        if (otherBeam.Size.X > xMax)
                        {
                            var newBeam = new BeamInt(
                                new Vector3Int(
                                    xMax,
                                    otherBeam.Position.Y,
                                    otherBeam.Position.Z),
                                new Vector3Int(
                                    otherBeam.Size.X,
                                    otherBeam.Size.Y,
                                    otherBeam.Size.Z));

                            createdSlices.Add((newBeam, otherState));

                            otherBeam.Size.X = xMax;
                        }

                        if (otherBeam.Position.Y < currentBeam.Position.Y)
                        {
                            var newBeam = new BeamInt(
                                new Vector3Int(
                                    otherBeam.Position.X,
                                    otherBeam.Position.Y,
                                    otherBeam.Position.Z),
                                new Vector3Int(
                                    otherBeam.Size.X,
                                    yMin,
                                    otherBeam.Size.Z));

                            createdSlices.Add((newBeam, otherState));

                            otherBeam.Position.Y = currentBeam.Position.Y;
                        }

                        if (otherBeam.Size.Y > yMax)
                        {
                            var newBeam = new BeamInt(
                                new Vector3Int(
                                    otherBeam.Position.X,
                                    yMax,
                                    otherBeam.Position.Z),
                                new Vector3Int(
                                    otherBeam.Size.X,
                                    otherBeam.Size.Y,
                                    otherBeam.Size.Z));

                            createdSlices.Add((newBeam, otherState));

                            otherBeam.Size.Y = yMax;
                        }

                        if (otherBeam.Position.Z < currentBeam.Position.Z)
                        {
                            var newBeam = new BeamInt(
                                new Vector3Int(
                                    otherBeam.Position.X,
                                    otherBeam.Position.Y,
                                    otherBeam.Position.Z),
                                new Vector3Int(
                                    otherBeam.Size.X,
                                    otherBeam.Size.Y,
                                    zMin));

                            createdSlices.Add((newBeam, otherState));

                            otherBeam.Position.Z = currentBeam.Position.Z;
                        }

                        if (otherBeam.Size.Z > zMax)
                        {
                            var newBeam = new BeamInt(
                                new Vector3Int(
                                    otherBeam.Position.X,
                                    otherBeam.Position.Y,
                                    zMax),
                                new Vector3Int(
                                    otherBeam.Size.X,
                                    otherBeam.Size.Y,
                                    otherBeam.Size.Z));

                            createdSlices.Add((newBeam, otherState));
                        }
                    }
                    else
                    {
                        createdSlices.Add((otherBeam, otherState));
                    }
                }

                createdSlices.Add((currentBeam, state));
                slices = createdSlices;
            }

            long total = 0;
            foreach ((BeamInt, bool) slice in slices.Where(slice => slice.Item2))
            {
                Vector3Int min = slice.Item1.Position;
                Vector3Int max = slice.Item1.Size;

                total += (long)(max.X - min.X) * (max.Y - min.Y) * (max.Z - min.Z);
            }

            return total.ToString();
        }
    }
}