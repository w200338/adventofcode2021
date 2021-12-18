namespace AdventOfCode2021.Days.Day18
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Tools.Extensions;

    public class Day18 : BaseDay
    {
        public override string Part1()
        {
            string[] inputLines = Input.Split("\r\n");

            // Read input
            List<DoubleValue> snailFishNumbers = inputLines.Select(ParseSnailNumber).ToList();

            // Start adding
            DoubleValue total = snailFishNumbers[0];
            //VerifyTree(total);
            //Console.WriteLine(total.ToString());

            for (int i = 1; i < snailFishNumbers.Count; i++)
            {
                total = Add(total, snailFishNumbers[i]);
                //Console.Write("After addition: ");
                //Console.WriteLine(total.ToString());

                bool change = true;
                while (change)
                {
                    change = TryExplode(total);
                    if (change)
                    {
                        //Console.Write("after explode:  ");
                        //Console.WriteLine(total.ToString());
                        //VerifyTree(total);
                        continue;
                    }

                    change = TrySplit(total);
                    //if (change)
                    //{
                    //    Console.Write("after split:    ");
                    //    Console.WriteLine(total.ToString());
                    //    VerifyTree(total);
                    //}
                }

                //Console.Write("After reduction:");
                //Console.WriteLine(total.ToString());
            }

            Console.WriteLine("Result:");
            Console.WriteLine(total.ToString());

            long magnitude = CalculateMagnitude(total);
            return magnitude.ToString();
        }

        public override string Part2()
        {
            var inputLines = Input.Split("\r\n").ToList();

            long max = inputLines.DifferentCombinations(2).Max(numbers =>
            {
                string[] numberArray = numbers.ToArray();
                
                // recreate inputs again and again because data is mutable
                DoubleValue value1 = ParseSnailNumber(numberArray[0]);
                DoubleValue value2 = ParseSnailNumber(numberArray[1]);

                var total = Add(value1, value2);

                while (TryExplode(total) || TrySplit(total))
                {
                }

                return CalculateMagnitude(total);
            });

            inputLines.Reverse();
            long inverseMax = inputLines.DifferentCombinations(2).Max(numbers =>
            {
                string[] numberArray = numbers.ToArray();

                // recreate inputs again and again because data is mutable
                DoubleValue value1 = ParseSnailNumber(numberArray[0]);
                DoubleValue value2 = ParseSnailNumber(numberArray[1]);

                var total = Add(value1, value2);

                while (TryExplode(total) || TrySplit(total))
                {
                }

                return CalculateMagnitude(total);
            });

            return (max > inverseMax ? max : inverseMax).ToString();
        }

        private long CalculateMagnitude(DoubleValue doubleValue)
        {
            long total = 0;

            if (doubleValue.Left is SingleValue left)
            {
                total += 3 * left.Value;
            }
            else
            {
                total += 3 * CalculateMagnitude(doubleValue.Left as DoubleValue);
            }

            if (doubleValue.Right is SingleValue right)
            {
                total += 2 * right.Value;
            }
            else
            {
                total += 2 * CalculateMagnitude(doubleValue.Right as DoubleValue);
            }

            return total;
        }

        private DoubleValue Add(ISnailFishNumber a, ISnailFishNumber b)
        {
            var doubleValue = new DoubleValue(null)
            {
                Left = a,
                Right = b
            };

            a.Parent = doubleValue;
            b.Parent = doubleValue;

            return doubleValue;
        }

        private void Explode(DoubleValue value)
        {
            var rootNode = value.Parent;
            while (rootNode.Parent != null)
            {
                rootNode = rootNode.Parent;
            }

            List<ISnailFishNumber> singleValues = Flatten(rootNode);
            int index = singleValues.IndexOf(value);
            if (index > 0)
            {
                for (int i = index - 1; i >= 0; i--)
                {
                    if (singleValues[i] is SingleValue singleValue)
                    {
                        singleValue.Value += (value.Left as SingleValue)?.Value ?? 0;
                        break;
                    }
                }

                for (int i = index + 3; i < singleValues.Count; i++)
                {
                    if (singleValues[i] is SingleValue singleValue)
                    {
                        singleValue.Value += (value.Right as SingleValue)?.Value ?? 0;
                        break;
                    }
                }
            }

            var replacement = new SingleValue(0, value.Parent);
            if (value == value.Parent.Left)
            {
                value.Parent.Left = replacement;
            }
            else
            {
                value.Parent.Right = replacement;
            }
        }

        private List<ISnailFishNumber> Flatten(DoubleValue root)
        {
            List<ISnailFishNumber> numbers = new List<ISnailFishNumber>();
            numbers.Add(root);

            if (root.Left is SingleValue singleValueLeft)
            {
                numbers.Add(singleValueLeft);
            }
            else
            {
                numbers.AddRange(Flatten(root.Left as DoubleValue));
            }

            if (root.Right is SingleValue singleValueRight)
            {
                numbers.Add(singleValueRight);
            }
            else
            {
                numbers.AddRange(Flatten(root.Right as DoubleValue));
            }

            return numbers;
        }

        private void Split(SingleValue value, bool isLeft)
        {
            var splitValue = new DoubleValue(value.Parent);

            splitValue.Left = new SingleValue(value.Value / 2, splitValue);
            splitValue.Right = new SingleValue(value.Value / 2 + value.Value % 2, splitValue);

            if (isLeft)
            {
                value.Parent.Left = splitValue;
            }
            else
            {
                value.Parent.Right = splitValue;
            }
        }

        private bool TryExplode(DoubleValue doubleValue, int depth = 0)
        {
            if (depth >= 4 && doubleValue.Left is SingleValue && doubleValue.Right is SingleValue)
            {
                Explode(doubleValue);
                return true;
            }

            if (doubleValue.Left is DoubleValue left)
            {
                if (TryExplode(left, depth + 1))
                {
                    return true;
                }
            }

            if (doubleValue.Right is DoubleValue right)
            {
                if (TryExplode(right, depth + 1))
                {
                    return true;
                }
            }

            return false;
        }

        private bool TrySplit(DoubleValue doubleValue)
        {
            if (doubleValue.Left is SingleValue leftSingle && leftSingle.Value > 9)
            {
                Split(leftSingle, true);
                return true;
            }

            if (doubleValue.Left is DoubleValue left)
            {
                if (TrySplit(left))
                {
                    return true;
                }
            }

            if (doubleValue.Right is SingleValue rightSingle && rightSingle.Value > 9)
            {
                Split(rightSingle, false);
                return true;
            }

            if (doubleValue.Right is DoubleValue right)
            {
                if (TrySplit(right))
                {
                    return true;
                }
            }

            return false;
        }

        private static DoubleValue ParseSnailNumber(string inputLine)
        {
            var root = new DoubleValue(null);

            Stack<DoubleValue> inputStack = new Stack<DoubleValue>();
            Stack<bool> leftSideStack = new Stack<bool>();
            inputStack.Push(root);
            leftSideStack.Push(true);

            for (int i = 1; i < inputLine.Length; i++)
            {
                if (inputLine[i] == '[')
                {
                    var doubleValue = new DoubleValue(inputStack.Peek());

                    if (leftSideStack.Peek())
                    {
                        inputStack.Peek().Left = doubleValue;
                    }
                    else
                    {
                        inputStack.Peek().Right = doubleValue;
                    }

                    leftSideStack.Push(true);
                    inputStack.Push(doubleValue);
                }
                else if (char.IsDigit(inputLine[i]))
                {
                    if (leftSideStack.Peek())
                    {
                        inputStack.Peek().Left = new SingleValue(int.Parse(inputLine[i].ToString()), inputStack.Peek());
                    }
                    else
                    {
                        inputStack.Peek().Right = new SingleValue(int.Parse(inputLine[i].ToString()), inputStack.Peek());
                    }
                }
                else if (inputLine[i] == ',')
                {
                    leftSideStack.Pop();
                    leftSideStack.Push(false);
                }
                else if (inputLine[i] == ']')
                {
                    inputStack.Pop();
                    leftSideStack.Pop();
                }
            }

            return root;
        }

        //private void VerifyTree(DoubleValue root)
        //{
        //    if (root.Parent != null)
        //    {
        //        if (root.Parent.Left != root && root.Parent.Right != root)
        //        {
        //            throw new Exception();
        //        }
        //    }

        //    if (root.Left.Parent != root)
        //    {
        //        throw new Exception();
        //    }

        //    if (root.Right.Parent != root)
        //    {
        //        throw new Exception();
        //    }

        //    if (root.Left is DoubleValue left)
        //    {
        //        VerifyTree(left);
        //    }

        //    if (root.Right is DoubleValue right)
        //    {
        //        VerifyTree(right);
        //    }
        //}
    }

    public interface ISnailFishNumber
    {
        DoubleValue Parent { get; set; }
    }

    public class SingleValue : ISnailFishNumber
    {
        public SingleValue(int value, DoubleValue parent)
        {
            Value = value;
            Parent = parent;
        }

        public int Value { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }

        /// <inheritdoc />
        public DoubleValue Parent { get; set; }
    }

    public class DoubleValue : ISnailFishNumber
    {
        public DoubleValue(DoubleValue parent)
        {
            Parent = parent;
        }

        public ISnailFishNumber Left { get; set; }

        public ISnailFishNumber Right { get; set; }

        public override string ToString()
        {
            return $"[{Left},{Right}]";
        }

        /// <inheritdoc />
        public DoubleValue Parent { get; set; }
    }

}