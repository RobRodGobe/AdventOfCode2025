using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2025.Helper;

namespace AdventOfCode2025.DaySpecific
{
	public class Day11 : DayBase
	{
		Dictionary<(string, string), long> memo = new Dictionary<(string, string), long>();

        public override void Part1()
		{
            var lines = ReadInputLines();
            long res = 0;
			Dictionary<string, HashSet<string>> servers = new Dictionary<string, HashSet<string>>();
			const string start = "you";
			const string end = "out";

			memo.Clear();

			servers = GetServerInfo(lines);
			res = GetPaths(start, end, servers);

			Console.WriteLine($"{GetType().Name} {nameof(Part1)}: {res}.");
		}

		public override void Part2()
		{
			var lines = ReadInputLines();
            long res = 0;
			Dictionary<string, HashSet<string>> servers = new Dictionary<string, HashSet<string>>();
			const string start = "svr";
			const string end = "out";
			string[] constraints = new string[] { "dac", "fft" };
			
			memo.Clear();

			servers = GetServerInfo(lines);

			foreach (var perm in PermuteHelpers.Permute(constraints))
			{
				long count = GetPaths(start, perm[0], servers);

				for (int i = 0; i < perm.Count - 1; i++)
					count *= GetPaths(perm[i], perm[i+1], servers);

				count *= GetPaths(perm.Last(), end, servers);

				res += count;
			}

			Console.WriteLine($"{GetType().Name} {nameof(Part2)}: {res}.");
		}

		private Dictionary<string, HashSet<string>> GetServerInfo(string[] lines)
        {			
			Dictionary<string, HashSet<string>> servers = new Dictionary<string, HashSet<string>>();
            int n = lines.Length;

			for (int i = 0; i < n; i++)
            {
                var info = lines[i].Split(":");
				string server = info[0].Replace(" ", "");
				var output = info[1].Split(" ").Select(i => i.Replace(" ", "")).Where(i => !String.IsNullOrEmpty(i)).ToHashSet();
				servers.Add(server, output);
            }

            return servers;
        }

		private long GetPaths(string start, string end, Dictionary<string, HashSet<string>> servers)
        {
			if (start == end)
				return 1;

			if (!servers.ContainsKey(start))
				return 0;

			if (memo.TryGetValue((start, end), out var cached))
				return cached;

            long count = 0;
			HashSet<string> paths = servers[start];

			foreach (string p in paths)
            {
                if (p == end)
				{
					count++;
					continue;
				}

				count += GetPaths(p, end, servers);
            }

			memo[(start, end)] = count;

			return count;
        }
	}
}
