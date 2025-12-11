using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025.Helper
{
    public static class PermuteHelpers
    {
        public static List<List<T>> Permute<T>(IList<T> items)
        {
            var results = new List<List<T>>();
            PermuteHelper(items, 0, results);
            return results;
        }

        private static void PermuteHelper<T>(IList<T> items, int start, List<List<T>> results)
        {
            if (start == items.Count)
            {
                results.Add(new List<T>(items));
                return;
            }

            for (int i = start; i < items.Count; i++)
            {
                (items[start], items[i]) = (items[i], items[start]); // swap
                PermuteHelper(items, start + 1, results);
                (items[start], items[i]) = (items[i], items[start]); // swap back
            }
        }
    }
}