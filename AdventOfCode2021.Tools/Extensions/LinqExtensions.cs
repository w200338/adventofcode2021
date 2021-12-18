﻿namespace AdventOfCode2021.Tools.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    public static class LinqExtensions
    {
        // https://stackoverflow.com/a/33336576
        public static IEnumerable<IEnumerable<T>> DifferentCombinations<T>(this IEnumerable<T> elements, int k)
        {
            return k == 0 ? new[] { new T[0] } :
                elements.SelectMany((e, i) =>
                    elements.Skip(i + 1).DifferentCombinations(k - 1).Select(c => (new[] { e }).Concat(c)));
        }
    }
}