using System;
using System.Text;
using AdventOfCode2025.Helper;

namespace AdventOfCode2025.DaySpecific
{
    public class Day2 : DayBase
	{
		/// <summary>
		/// Checks if a string is made of a sequence repeated at least twice.
		/// For example: "11" (pattern "1" repeated 2x), "565656" (pattern "56" repeated 3x).
		/// Returns true if the entire string can be composed of a pattern repeated at least twice.
		/// </summary>
		private bool IsRepeatedPattern(string s)
		{
			int len = s.Length;
			// Try all possible pattern lengths from 1 to len/2
			for (int patternLen = 1; patternLen <= len / 2; patternLen++)
			{
				// The pattern length must divide the total length evenly
				if (len % patternLen != 0)
					continue;

				string pattern = s.Substring(0, patternLen);
				int repetitions = len / patternLen;

				// Reconstruct the full string from the pattern
				string reconstructed = string.Concat(Enumerable.Repeat(pattern, repetitions));

				if (reconstructed == s)
					return true;
			}

			return false;
		}

		public override void Part1()
		{
			var input = ReadInputLines();
            var lines = input?.FirstOrDefault()?.Split(',') ?? [];
            int n = lines.Length;
            long res = 0;

            for (int i = 0; i < n; i++)
            {
                var ids = lines[i].Split("-");
                long start = 0;
                long end = 0;

                long.TryParse(ids[0], out start);
                long.TryParse(ids[1], out end);

                for (long j = start; j <= end; j++)
                {
                    string numStr = j.ToString();

                    if (numStr.Length % 2 != 0)
                        continue;

                    string left = numStr.Substring(0, numStr.Length / 2);
                    string right = numStr.Substring(numStr.Length / 2);

                    if (left == right)
                        res += j;
                }
            }

            Console.WriteLine($"Day2 Part1: {res}.");
		}

		public override void Part2()
		{
			var input = ReadInputLines();
            var lines = input?.FirstOrDefault()?.Split(',') ?? [];
            int n = lines.Length;
            long res = 0;

            for (int i = 0; i < n; i++)
            {
                var ids = lines[i].Split("-");
                long start = 0;
                long end = 0;

                long.TryParse(ids[0], out start);
                long.TryParse(ids[1], out end);

                for (long j = start; j <= end; j++)
                {
                    string numStr = j.ToString();

                    if (IsRepeatedPattern(numStr))
                        res += j;
                }
            }

            Console.WriteLine($"Day2 Part2: {res}.");
		}
	}
}
