using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2025.Helper;

namespace AdventOfCode2025.DaySpecific
{
	public class Day7 : DayBase
	{
        public override void Part1()
		{
			var lines = ReadInputLines();
			int n = lines.Length;
			int m = lines[0].Length;
			long count = 0;

			int startRow = -1;
			int startCol = -1;

			for (int i = 0; i < n; i++)
			{
				int col = lines[i].IndexOf('S');
				
				if (col >= 0)
				{
					startRow = i;
					startCol = col;
					break;
				}
			}

			if (startRow == -1)
			{
				Console.WriteLine($"Day7 Part1: {count}.");
				return;
			}

			Dictionary<int, long> beams = new Dictionary<int, long>();

			beams[startCol] = 1;

			for (int r = startRow + 1; r < n; r++)
			{
				Dictionary<int, long> nextBeams = new Dictionary<int, long>();

				foreach (var kv in beams)
				{
					int c = kv.Key;
					long cnt = kv.Value;

					if (c < 0 || c >= m || cnt == 0)
						continue;

					char cell = lines[r][c];

					if (cell == '^')
					{
						count += 1;

						int left = c - 1;
						int right = c + 1;
						
						if (left >= 0)
							nextBeams[left] = nextBeams.GetValueOrDefault(left, 0) + cnt;
						
						if (right < m)
							nextBeams[right] = nextBeams.GetValueOrDefault(right, 0) + cnt;
					}
					else
						nextBeams[c] = nextBeams.GetValueOrDefault(c, 0) + cnt;
				}

				beams = nextBeams;
				
				if (beams.Count == 0)
					break;
			}

			Console.WriteLine($"Day7 Part1: {count}.");
		}

		public override void Part2()
		{
			var lines = ReadInputLines();
			int n = lines.Length;
			int m = lines[0].Length;

			int startRow = -1;
			int startCol = -1;

			for (int i = 0; i < n; i++)
			{
				int col = lines[i].IndexOf('S');

				if (col >= 0)
				{
					startRow = i;
					startCol = col;
					break;
				}
			}

			if (startRow == -1)
			{
				Console.WriteLine($"Day7 Part2: 0");
				return;
			}

			long[,] memo = new long[n + 1, m];

			for (int r = 0; r <= n; r++)
				for (int c = 0; c < m; c++)
					memo[r, c] = -1;

			long CountPaths(int r, int c)
			{
				if (r >= n)
					return 1;

				if (c < 0 || c >= m)
					return 1;

				if (memo[r, c] != -1)
					return memo[r, c];

				char cell = lines[r][c];
				long res = 0;

				if (cell == '^')
					res = CountPaths(r + 1, c - 1) + CountPaths(r + 1, c + 1);
				else
					res = CountPaths(r + 1, c);

				memo[r, c] = res;

				return res;
			}

			long total = CountPaths(startRow + 1, startCol);

			Console.WriteLine($"Day7 Part2: {total}.");
		}
	}
}
