using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2025.Helper;

namespace AdventOfCode2025.DaySpecific
{
	public class Day5 : DayBase
	{
        public override void Part1()
		{
			var lines = ReadInputLines();
            int n = lines.Length;
			HashSet<(long, long)> ranges = new HashSet<(long, long)>();
			int fresh = 0;

			int idx = 0;

			while (idx < n && !String.IsNullOrEmpty(lines[idx]))
            {
                string[] range = lines[idx].Split("-");
				long a = long.Parse(range[0]);
				long b = long.Parse(range[1]);
				
				ranges.Add((a, b));

				idx++;
            }

			for (int i = idx + 1; i < n; i++)
            {				
				var merged = ConsolidateRanges(ranges);

                foreach (var range in merged)
                {
					long item = long.Parse(lines[i]);
                    
					if (item >= range.Item1 && item <= range.Item2)
                    {
						fresh++;
                        break;
                    }
                }
            }

			Console.WriteLine($"Day5 Part1: {fresh}.");
		}

		public override void Part2()
		{
			var lines = ReadInputLines();
            int n = lines.Length;
			HashSet<(long, long)> ranges = new HashSet<(long, long)>();
			long fresh = 0;

			int idx = 0;

			while (idx < n && !String.IsNullOrEmpty(lines[idx]))
            {
                string[] range = lines[idx].Split("-");
				long a = long.Parse(range[0]);
				long b = long.Parse(range[1]);
				
				ranges.Add((a, b));

				idx++;
            }

			var merged = ConsolidateRanges(ranges);

			foreach (var r in merged)
			{
				fresh += r.Item2 - r.Item1 + 1;
			}

			Console.WriteLine($"Day5 Part2: {fresh}.");
		}
		
		private static List<(long, long)> ConsolidateRanges(IEnumerable<(long, long)> ranges)
		{
			var list = ranges.OrderBy(r => r.Item1).ThenBy(r => r.Item2).ToList();
			var result = new List<(long, long)>();
			if (list.Count == 0)
				return result;

			long curStart = list[0].Item1;
			long curEnd = list[0].Item2;

			foreach (var r in list.Skip(1))
			{
				if (r.Item1 <= curEnd + 1) 
				{
					curEnd = Math.Max(curEnd, r.Item2);
				}
				else
				{
					result.Add((curStart, curEnd));
					curStart = r.Item1;
					curEnd = r.Item2;
				}
			}

			result.Add((curStart, curEnd));

			return result;
		}
	}
}
