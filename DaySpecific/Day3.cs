using System;
using System.Text;
using AdventOfCode2025.Helper;

namespace AdventOfCode2025.DaySpecific
{
    public class Day3 : DayBase
	{
		public override void Part1()
		{
			var banks = ReadInputLines();
            int n = banks.Length;
			int m = banks[0].Length;
            long totalJolts = 0;

			for (int i = 0; i < n; i++)
            {
				Dictionary<char, List<int>> joltValues = new ();
				char a = banks[i][0];
				char b = banks[i][1];
				
				for (int j = 2; j < m; j++)
                {
					char c = banks[i][j];
										
					if (b > a)
                    {
                        (a, b) = (b, a);
						b = c;
                    }
					else if (c > b)
						b = c;
                }				

				totalJolts += Int32.Parse($"{a}{b}");
            }

            Console.WriteLine($"Day3 Part1: {totalJolts}.");
		}

		public override void Part2()
		{
			var banks = ReadInputLines();
			int n = banks.Length;
			long totalJolts = 0;

			for (int i = 0; i < n; i++)
			{
				string row = banks[i];
				string largest12 = GetLargest12Digits(row);
				
				if (largest12.Length == 12 && long.TryParse(largest12, out long num))
				{
					totalJolts += num;
				}
			}

			Console.WriteLine($"Day3 Part2: {totalJolts}.");
		}
		
		private string GetLargest12Digits(string s)
		{
			int targetLength = 12;
			if (s.Length < targetLength)
				return string.Empty;

			StringBuilder result = new StringBuilder();
			int startIdx = 0;

			for (int selected = 0; selected < targetLength; selected++)
			{
				int needed = targetLength - selected;
				int remaining = s.Length - startIdx;
				int maxLookAhead = remaining - needed;

				char maxChar = s[startIdx];
				int maxIdx = startIdx;

				for (int j = startIdx; j <= startIdx + maxLookAhead; j++)
				{
					if (s[j] > maxChar)
					{
						maxChar = s[j];
						maxIdx = j;
					}
				}

				result.Append(maxChar);
				startIdx = maxIdx + 1;
			}

			return result.ToString();
		}
	}
}
