namespace AdventOfCode2021.Days.Day14
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Day14 : BaseDay
    {
        public override string Part1()
        {
            string[] inputLines = Input.Split("\r\n");

            string currentStep = inputLines[0];

            List<Rule> rules = new List<Rule>();
            foreach (string inputLine in inputLines.Skip(2))
            {
                string[] parts = inputLine.Split(" -> ");

                rules.Add(new Rule
                {
                    Pattern1 = parts[0][0],
                    Pattern2 = parts[0][1],
                    Insertion = parts[1]
                });
            }

            string nextStep = string.Empty;
            nextStep += currentStep[0];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 1; j < currentStep.Length; j++)
                {
                    Rule applicableRule = rules.FirstOrDefault(rule => rule.Pattern1 == currentStep[j - 1] && rule.Pattern2 == currentStep[j]);
                    if (applicableRule != null)
                    {
                        nextStep += applicableRule.Insertion;
                    }

                    nextStep += currentStep[j];
                }

                currentStep = nextStep;
                nextStep = string.Empty;
                nextStep += currentStep[0];
            }

            var groups = currentStep.GroupBy(c => c).ToList();
            return (groups.Max(c => c.Count()) - groups.Min(c => c.Count())).ToString();
        }

        public override string Part2()
        {
            string[] inputLines = Input.Split("\r\n");

            string startLine = inputLines[0];

            List<Rule> rules = new List<Rule>();
            foreach (string inputLine in inputLines.Skip(2))
            {
                string[] parts = inputLine.Split(" -> ");

                rules.Add(new Rule
                {
                    Pattern1 = parts[0][0],
                    Pattern2 = parts[0][1],
                    Insertion = parts[1]
                });
            }
            
            Dictionary<string, long> patternDictionary = new Dictionary<string, long>();
            for (int i = 0; i < startLine.Length - 1; i++)
            {
                string key = startLine.Substring(i, 2);
                if (patternDictionary.ContainsKey(key))
                {
                    patternDictionary[key] += 1;
                }
                else
                {
                    patternDictionary[key] = 1;
                }
            }

            for (int i = 0; i < 40; i++)
            {
                patternDictionary = Step(patternDictionary, rules);
            }

            Dictionary<string, long> outputDict = new Dictionary<string, long>
            {
                { startLine[0].ToString(), 1 },
                { startLine[startLine.Length - 1].ToString(), 1 }
            };

            foreach (KeyValuePair<string, long> pattern in patternDictionary)
            {
                UpdateDictionary(outputDict, pattern.Key[0].ToString(), pattern.Value);
                UpdateDictionary(outputDict, pattern.Key[1].ToString(), pattern.Value);
            }

            return ((outputDict.Max(kvp => kvp.Value) - outputDict.Min(kvp => kvp.Value)) / 2).ToString();
        }

        private void UpdateDictionary(Dictionary<string, long> dict, string key, long amount)
        {
            if (dict.ContainsKey(key))
            {
                dict[key] += amount;
            }
            else
            {
                dict[key] = amount;
            }
        }

        private Dictionary<string, long> Step(Dictionary<string, long> patternDictionary, List<Rule> rules)
        {
            Dictionary<string, long> output = new Dictionary<string, long>();

            foreach (KeyValuePair<string, long> pattern in patternDictionary)
            {
                Rule applicableRule = rules.FirstOrDefault(rule => rule.Pattern1 == pattern.Key[0] && rule.Pattern2 == pattern.Key[1]);
                if (applicableRule != null)
                {
                    UpdateDictionary(output, $"{pattern.Key[0]}{applicableRule.Insertion}", pattern.Value);
                    UpdateDictionary(output, $"{applicableRule.Insertion}{pattern.Key[1]}", pattern.Value);
                }
            }

            return output;
        }
    }

    public class Rule
    {
        public char Pattern1 { get; set; }
        public char Pattern2 { get; set; }

        public string Insertion { get; set; }
    }
}