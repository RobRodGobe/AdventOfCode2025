using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2025.Helper;

namespace AdventOfCode2025.DaySpecific
{
	public class Day6 : DayBase
	{
        public override void Part1()
		{
			var lines = ReadInputLines();
            int n = lines.Length;
            List<List<string>> map = new List<List<string>>();

            for (int i = 0; i < n; i++)
            {
                StringBuilder sb = new StringBuilder();
                List<string> nums = new List<string>();
                int len = lines[i].Length;

                for (int j = 0; j < len; j++)
                {
                    if (char.IsWhiteSpace(lines[i][j]) && sb.Length == 0)
                        continue;

                    if (char.IsWhiteSpace(lines[i][j]))
                    {
                        nums.Add(sb.ToString());
                        sb.Clear();

                        continue;
                    }

                    sb.Append(lines[i][j]);
                }

                if (sb.Length > 0)
                    nums.Add(sb.ToString());

                map.Add(nums);
            }                

            var transposed = TransposeMap(map);
            int t = transposed.Count;
            long r = 0;
            
            for (int i = 0; i < t; i++)
            {
                int c = transposed[i].Count;
                long res = 0;

                if (transposed[i][c - 1] == "*")
                {
                    res = 1;

                    for (int j = 0; j < c - 1; j++)
                    {
                        int num = Int32.Parse(transposed[i][j]);
                        res *= num;
                    }
                }
                else
                {
                    for (int j = 0; j < c - 1; j++)
                    {
                        int num = Int32.Parse(transposed[i][j]);
                        res += num;
                    }
                }

                r += res;
            }

            Console.WriteLine($"Day6 Part1: {r}.");
		}

		public override void Part2()
		{
			var lines = ReadInputLines();
            int n = lines.Length;
            List<List<string>> map = new List<List<string>>();

            for (int i = 0; i < n; i++)
            {
                List<string> line = lines[i].ToCharArray().Select(c => c.ToString()).ToList();
                map.Add(line);
            }

            var transposed = TransposeMap(map);
            int t = transposed.Count;
            bool isSum = false;
            long res = 0;
            long r = 0;
            List<long> results = new List<long>();

            for (int i = 0; i < t; i++)
            {
                if (String.IsNullOrEmpty(String.Join("",transposed[i]).Trim()))
                {
                    r += res;
                    res = 0;
                    continue;
                }

                int c = transposed[i].Count;
                StringBuilder sb = new StringBuilder();

                if (transposed[i][c - 1] == "+")
                {
                    res = 0;
                    isSum = true;
                }
                else if (transposed[i][c - 1] == "*")
                {
                    res = 1;
                    isSum = false;
                }

                for (int j = 0; j < c - 1; j++)
                {
                    if (transposed[i][j] != "")
                        sb.Append(transposed[i][j]);
                }

                long num = long.Parse(sb.ToString());

                if (isSum)
                    res += num;
                else
                    res *= num;

                results.Add(res);
            }
            
            r += res;
			
			Console.WriteLine($"Day6 Part2: {r}.");
		}

        private List<List<string>> TransposeMap(List<List<string>> source)
        {
            if (source == null || source.Count == 0)
                return new List<List<string>>();

            int rows = source.Count;
            int cols = source.Max(r => r?.Count ?? 0);

            var result = new List<List<string>>(cols);
            for (int c = 0; c < cols; c++)
                result.Add(new List<string>(rows));

            for (int r = 0; r < rows; r++)
            {
                var row = source[r];
                int rowLen = row?.Count ?? 0;
                for (int c = 0; c < cols; c++)
                {
                    if (c < rowLen)
                        result[c].Add(row[c]);
                    else
                        result[c].Add(string.Empty);
                }
            }

            return result;
        }
	}
}
