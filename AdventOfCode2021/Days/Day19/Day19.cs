namespace AdventOfCode2021.Days.Day19
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Tools.Extensions;
    using Tools.Mathematics.Vectors;

    public class Day19 : BaseDay
    {
        // create 24 unique rotations
        // https://stackoverflow.com/a/50546727
        private static RotationData[] rotations = new RotationData[24]
        {
            new RotationData(0, 0, 0),

            new RotationData(1, 0, 0),
            new RotationData(0, 1, 0),
            new RotationData(0, 1, 0), // 4

            new RotationData(2, 0, 0),
            new RotationData(1, 1, 0),
            new RotationData(0, 1, 1),
            new RotationData(1, 1, 3),
            new RotationData(0, 2, 0),
            new RotationData(3, 0, 1), // 10
            new RotationData(0, 0, 2),

            new RotationData(3, 0, 0), // 12
            new RotationData(2, 1, 0),
            new RotationData(1, 1, 1),
            new RotationData(0, 2, 1),
            new RotationData(1, 2, 0),
            new RotationData(1, 0, 2),

            new RotationData(0, 1, 2, true), // 18
            new RotationData(0, 3, 0),
            new RotationData(0, 0, 3), // 20

            new RotationData(3, 1, 0),
            new RotationData(1, 2, 1),
            new RotationData(0, 3, 1),
            new RotationData(1, 3, 0) // 24
        };

        public override string Part1()
        {
            // Read input
            string[] inputLines = Input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            Dictionary<int, List<Vector3Int>> data = new Dictionary<int, List<Vector3Int>>();

            int currentScanner = 0;
            foreach (string inputLine in inputLines)
            {
                if (inputLine.StartsWith("---"))
                {
                    currentScanner = int.Parse(inputLine.Split(' ', StringSplitOptions.RemoveEmptyEntries)[2]);
                    data.Add(currentScanner, new List<Vector3Int>());
                }
                else
                {
                    int[] coordinates = inputLine.Split(',').Select(int.Parse).ToArray();
                    data[currentScanner].Add(new Vector3Int(coordinates[0], coordinates[1], coordinates[2]));
                }
            }

            List<Vector3Int> beaconLocations = new List<Vector3Int>(data[0]);
            Dictionary<Vector3Int, (Vector3Int, Vector3Int)> relativeDistances = new Dictionary<Vector3Int, (Vector3Int, Vector3Int)>();
            foreach (Vector3Int vector in data[0])
            {
                foreach (Vector3Int otherVector in data[0])
                {
                    if (vector == otherVector)
                    {
                        continue;
                    }

                    relativeDistances.Add(otherVector - vector, (vector, otherVector));
                    //relativeDistances.Add((otherVector, vector), vector - otherVector);
                }
            }

            List<Dictionary<Vector3Int, (Vector3Int, Vector3Int)>> allRelativeDistances = new List<Dictionary<Vector3Int, (Vector3Int, Vector3Int)>>();
            allRelativeDistances.Add(relativeDistances);

            // use scanner 0 as absolute truth
            List<int> scannersToGo = new List<int>(Enumerable.Range(1, data.Count - 1));

            while (scannersToGo.Count > 0)
            {
                for (int i = 0; i < scannersToGo.Count; i++)
                {
                    for (int j = 0; j < allRelativeDistances.Count; j++)
                    {
                        (RotationData, Vector3Int) rotation = FindOrientation(data[1], allRelativeDistances[j]);
                        if (rotation.Item1 != null)
                        {
                            // TODO: find out which beacons are new and where they are located

                            // find an already known combination
                            //Vector3Int offset = FindScannerOffset(data[i], rotation., allRelativeDistances[j]);

                            // create list of new beacons and new relative distances
                            List<Vector3Int> transformedPoints = data[i].Select(point => point + rotation.Item2).ToList();
                            beaconLocations.AddRange(transformedPoints);
                            Dictionary<Vector3Int, (Vector3Int, Vector3Int)> otherRelativeDistances = new Dictionary<Vector3Int, (Vector3Int, Vector3Int)>();
                            foreach (Vector3Int vector in transformedPoints)
                            {
                                foreach (Vector3Int otherVector in transformedPoints)
                                {
                                    if (vector == otherVector)
                                    {
                                        continue;
                                    }

                                    if (!otherRelativeDistances.ContainsKey(otherVector - vector))
                                    {
                                        otherRelativeDistances.Add(otherVector - vector, (vector, otherVector));
                                    }
                                }
                            }

                            allRelativeDistances.Add(otherRelativeDistances);
                            break;
                        }
                    }

                    scannersToGo.Remove(scannersToGo[i]);
                    i--;
                }
            }

            beaconLocations = beaconLocations.Distinct().OrderBy(location => location.X).ToList();
            return beaconLocations.Count.ToString();
        }

        private static Vector3Int FindScannerOffset(List<Vector3Int> data, RotationData rotation, Dictionary<Vector3Int, (Vector3Int, Vector3Int)> relativeDistances)
        {
            List<Vector3Int> offsets = new List<Vector3Int>();
            List<Vector3Int> transformedPositions = data.Select(rotation.Transform).ToList();
            foreach (Vector3Int transformedPosition in transformedPositions)
            {
                foreach (Vector3Int otherTransformedPosition in transformedPositions)
                {
                    if (transformedPosition != otherTransformedPosition)
                    {
                        if (relativeDistances.TryGetValue(otherTransformedPosition - transformedPosition, out (Vector3Int, Vector3Int) match))
                        {
                            offsets.Add(match.Item1 - transformedPosition);
                            //return match.Item1 - transformedPosition;
                        }
                    }
                }
            }

            throw new Exception();
        }

        private static (RotationData, Vector3Int) FindOrientation(List<Vector3Int> data, Dictionary<Vector3Int, (Vector3Int, Vector3Int)> relativeDistances)
        {
            Vector3Int offset = Vector3Int.Zero;
            for (int i = 0; i < rotations.Length; i++)
            {
                List<Vector3Int> transformedPositions = data.Select(rotations[i].Transform).ToList();

                int found = 0;
                foreach (Vector3Int transformedPosition in transformedPositions)
                {
                    foreach (Vector3Int otherTransformedPosition in transformedPositions)
                    {
                        if (transformedPosition != otherTransformedPosition)
                        {
                            if (relativeDistances.ContainsKey(otherTransformedPosition - transformedPosition))
                            {
                                offset = relativeDistances[otherTransformedPosition - transformedPosition].Item1 - transformedPosition;

                                found++;
                            }
                        }
                    }
                }

                if (found >= 12)
                {
                    return (rotations[i], offset);
                }
            }

            return (null, offset);
        }

        public override string Part2()
        {
            return string.Empty;
        }
    }

    public class RotationData
    {
        private readonly bool _zFirst;
        public int ScannerId { get; set; }

        public int XRot { get; set; }

        public int YRot { get; set; }

        public int ZRot { get; set; }

        public RotationData(int x, int y, int z, bool zFirst = false)
        {
            _zFirst = zFirst;
            XRot = x;
            YRot = y;
            ZRot = z;
        }

        public Vector3Int Transform(Vector3Int vector)
        {
            if (_zFirst)
            {
                if (ZRot > 0)
                {
                    double sin = Math.Sin(Math.PI / 2 * ZRot);
                    double cos = Math.Cos(Math.PI / 2 * ZRot);

                    vector = new Vector3Int(
                        x: (int)(cos * vector.X - sin * vector.Y),
                        y: (int)(sin * vector.X + cos * vector.Y),
                        z: vector.Z
                    );
                }
            }

            if (XRot > 0)
            {
                double sin = Math.Sin(Math.PI / 2 * XRot);
                double cos = Math.Cos(Math.PI / 2 * XRot);

                vector = new Vector3Int(
                    x: vector.X,
                    y: (int)(cos * vector.Y - sin * vector.Z),
                    z: (int)(sin * vector.Y + cos * vector.Z)
                );
            }

            if (YRot > 0)
            {
                double sin = Math.Sin(Math.PI / 2 * YRot);
                double cos = Math.Cos(Math.PI / 2 * YRot);

                vector = new Vector3Int(
                    x: (int)(cos * vector.X + sin * vector.Z),
                    y: vector.Y,
                    z: (int)(-sin * vector.X + cos * vector.Z)
                );
            }

            if (!_zFirst)
            {
                if (ZRot > 0)
                {
                    double sin = Math.Sin(Math.PI / 2 * ZRot);
                    double cos = Math.Cos(Math.PI / 2 * ZRot);

                    vector = new Vector3Int(
                        x: (int)(cos * vector.X - sin * vector.Y),
                        y: (int)(sin * vector.X + cos * vector.Y),
                        z: vector.Z
                    );
                }
            }
            
            return vector;
        }
    }
}