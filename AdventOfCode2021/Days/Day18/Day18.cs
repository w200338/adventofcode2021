namespace AdventOfCode2021.Days.Day18
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Day18 : BaseDay
    {
        public override string Part1()
        {
            string[] inputLines = Input.Split("\r\n");

            // Read input
            List<DoubleValue> snailFishNumbers = new List<DoubleValue>();
            foreach (string inputLine in inputLines)
            {
                var outerMostValue = new DoubleValue(null);
                snailFishNumbers.Add(outerMostValue);

                Stack<DoubleValue> inputStack = new Stack<DoubleValue>();
                Stack<bool> leftSideStack = new Stack<bool>();
                inputStack.Push(outerMostValue);
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
            }

            // Start adding
            DoubleValue total = snailFishNumbers[0];
            Console.WriteLine(total.ToString());
            for (int i = 1; i < snailFishNumbers.Count; i++)
            {
                total = Add(total, snailFishNumbers[i]);
                Console.Write("After addition:  ");
                Console.WriteLine(total.ToString());

                string startString = total.ToString();
                Reduce(total);
                Console.Write("After reduction: ");
                Console.WriteLine(total.ToString());

                while (startString != total.ToString())
                {
                    startString = total.ToString();
                    Reduce(total);
                    Console.Write("After reduction: ");
                    Console.WriteLine(total.ToString());
                }
            }

            Console.WriteLine(total.ToString());

            long magnitude = CalculateMagnitude(total);
            return magnitude.ToString();
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
            bool leftNull = false;
            bool rightNull = false;

            DoubleValue leftCurrentValue = value.Parent;
            while (leftCurrentValue != null && !(leftCurrentValue.Left is SingleValue))
            {
                leftCurrentValue = leftCurrentValue.Parent;
            }

            if (leftCurrentValue != null && leftCurrentValue.Left is SingleValue left)
            {
                left.Value += (value.Left as SingleValue)?.Value ?? 0;
            }
            else
            {
                leftNull = true;
            }

            DoubleValue rightCurrentValue = value.Parent;
            while (rightCurrentValue != null && !(rightCurrentValue.Right is SingleValue))
            {
                rightCurrentValue = rightCurrentValue.Parent;
            }

            if (rightCurrentValue != null && rightCurrentValue.Right is SingleValue right)
            {
                right.Value += (value.Right as SingleValue)?.Value ?? 0;
            }
            else
            {
                rightNull = true;
            }

            if (value == value.Parent.Left)
            {
                value.Parent.Left = new SingleValue(0, value.Parent);
            }
            else
            {
                value.Parent.Right = new SingleValue(0, value.Parent);
            }

            /*
            if (leftNull)
            {
                //value.Parent.Left = new SingleValue((value.Left as SingleValue).Value);
                value.Parent.Left = new SingleValue(0)
                {
                    Parent = value.Parent
                };
            }
            
            if (rightNull)
            {
                //value.Parent.Right = new SingleValue((value.Right as SingleValue).Value);
                value.Parent.Right = new SingleValue(0)
                {
                    Parent = value.Parent
                };
            }
            */
        }

        private void Split(SingleValue value, bool isLeft)
        {
            var splitValue = new DoubleValue(value.Parent)
            {
                Left = new SingleValue(value.Value / 2, value.Parent),
                Right = new SingleValue(value.Value / 2 + value.Value % 2, value.Parent)
            };

            if (isLeft)
            {
                value.Parent.Left = splitValue;
            }
            else
            {
                value.Parent.Right = splitValue;
            }
        }

        private void Reduce(DoubleValue doubleValue, int depth = 0)
        {
            if (depth >= 4 && doubleValue.Left is SingleValue && doubleValue.Right is SingleValue)
            {
                Explode(doubleValue);
                return;
            }

            if (doubleValue.Left is DoubleValue left)
            {
                Reduce(left, depth + 1);
            }
            else if (doubleValue.Left is SingleValue leftSingle && leftSingle.Value > 9)
            {
                Split(leftSingle, true);
            }

            if (doubleValue.Right is DoubleValue right)
            {
                Reduce(right, depth + 1);
            }
            else if (doubleValue.Right is SingleValue rightSingle && rightSingle.Value > 9)
            {
                Split(rightSingle, false);
            }
        }

        public override string Part2()
        {
            return string.Empty;
        }
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
            return $"[{Left}, {Right}]";
        }

        /// <inheritdoc />
        public DoubleValue Parent { get; set; }
    }

}