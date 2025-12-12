using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2025.Helper;

namespace AdventOfCode2025.DaySpecific
{
	public class Day12 : DayBase
	{
        public override void Part1()
		{
            var lines = ReadInputLines();
            int n = lines.Length;
            int res = 0;

			(List<int> presents, List<Region> regions) = GetRegionsAndAreas(lines);

			foreach (var region in regions)
			{
				if (region.DoPresentsFit(presents))
					res++;
			}

			Console.WriteLine($"{GetType().Name} {nameof(Part1)}: {res}.");
		}

		public override void Part2()
		{
			var lines = ReadInputLines();
            int n = lines.Length;
            string res = "Merry Christmas!";

			Console.WriteLine($"{GetType().Name} {nameof(Part2)}: {res}.");
		}

		private (List<int>, List<Region>) GetRegionsAndAreas(string[] lines)
		{
			int n = lines.Length;
			List<int> presents = new List<int>();
			List<Region> regions = new List<Region>();
			int area = 0;
			int idx = 0;

			for (int i = 0; i < n; i++)
			{
				if (String.IsNullOrEmpty(lines[i].Trim()))
				{
					presents[idx] = area;
					area = 0;
					continue;
				}

				if (lines[i].Contains(":"))
				{
					string[] sections = lines[i].Split(":");
					
					if (sections[0].Contains("x"))
					{
						string[] dims = sections[0].Split("x");
						int x = Int32.Parse(dims[0]);
						int y = Int32.Parse(dims[1]);
						List<int> presentList = sections[1].Split(" ").Where(s => Int32.TryParse(s, out int num)).Select(s => Int32.Parse(s)).ToList();

						regions.Add(new Region((x, y), presentList));
					}
					else
					{
						int shapeNum = Int32.Parse(sections[0]);
						idx = shapeNum;
						presents.Add(0);
					}
				}
				else
				{
					char[] line = lines[i].ToCharArray();
					area += line.Count(l => l == '#');
				}
			}

			return (presents, regions);
		}

		private record Region((int x, int y) Dimensions, List<int> Presents)
		{
			public bool DoPresentsFit(List<int> presentAreas)
			{
				int n = presentAreas.Count;
				int total = 0;

				for (int i = 0; i < n; i++)
				{
					total += Presents[i] * presentAreas[i];
				}

				return (Dimensions.x * Dimensions.y) > total;
			}
		}
	}
}
