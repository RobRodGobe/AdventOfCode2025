using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2025.Helper;

namespace AdventOfCode2025.DaySpecific
{
	public class Day8 : DayBase
	{
        public override void Part1()
		{
            var lines = ReadInputLines();
            int n = lines.Length;

            List<(long x, long y, long z)> points = new List<(long x, long y, long z)>();

            for (int i = 0; i < n; i++)
            {
                var parts = lines[i].Split(',');
                points.Add((long.Parse(parts[0]), long.Parse(parts[1]), long.Parse(parts[2])));
            }
            
            var edges = new List<(long dist, (long x, long y, long z) a, (long x, long y, long z) b)>();
            
            for (var i = 0; i < n - 1; i++)
            {
                var boxA = points[i];    
            
                for (var j = i + 1; j < n; j++)
                {
                    var boxB = points[j];
                    edges.Add((Distance(boxA, boxB), boxA, boxB));
                }
            }
            
            edges = [.. edges.OrderBy(d => d.dist)];
            
            var circuits = new List<HashSet<(long x, long y, long z)>>();
            
            foreach (var p in points)
            {
                circuits.Add([p]);
            }
            
            int iter = 0;
            int limit = n < 40  ? 10 : 1000; // add condition to test example and real input
            long res = 0;
            
            while (iter < limit)
            {
                iter++;
            
                var (d, a, b) = edges[iter - 1];
                var circuitA = circuits.First(c => c.Contains(a));
                var circuitB = circuits.First(c => c.Contains(b));
            
                if (circuitA != circuitB)
                {
                    circuitA.UnionWith(circuitB);
                    circuits.Remove(circuitB);
                    circuits = [.. circuits.OrderByDescending(c => c.Count)];
                }    
            
                res = circuits[0].Count * circuits[1].Count * circuits[2].Count;
            }

            Console.WriteLine($"{GetType().Name} {nameof(Part1)}: {res}.");
		}

		public override void Part2()
		{
			var lines = ReadInputLines();
            int n = lines.Length;

            List<(long x, long y, long z)> points = new List<(long x, long y, long z)>();

            for (int i = 0; i < n; i++)
            {
                var parts = lines[i].Split(',');
                points.Add((long.Parse(parts[0]), long.Parse(parts[1]), long.Parse(parts[2])));
            }
            
            var edges = new List<(long dist, (long x, long y, long z) a, (long x, long y, long z) b)>();
            
            for (var i = 0; i < n - 1; i++)
            {
                var boxA = points[i];    
            
                for (var j = i + 1; j < n; j++)
                {
                    var boxB = points[j];
                    edges.Add((Distance(boxA, boxB), boxA, boxB));
                }
            }
            
            edges = [.. edges.OrderBy(d => d.dist)];
            
            var circuits = new List<HashSet<(long x, long y, long z)>>();
            
            foreach (var p in points)
            {
                circuits.Add([p]);
            }
            
            int iter = 0;
            long res = 0;
            
            while (true)
            {
                iter++;
            
                var (d, a, b) = edges[iter - 1];
                var circuitA = circuits.First(c => c.Contains(a));
                var circuitB = circuits.First(c => c.Contains(b));
            
                if (circuitA != circuitB)
                {
                    circuitA.UnionWith(circuitB);
                    circuits.Remove(circuitB);
                    circuits = [.. circuits.OrderByDescending(c => c.Count)];
                }
            
                if (circuits.Count == 1 || circuits[0].Count == n)
                {
                    res = a.x * b.x;
                    break;
                }
            }

			Console.WriteLine($"{GetType().Name} {nameof(Part2)}: {res}.");
		}

        private long Distance((long x, long y, long z) point1, (long x, long y, long z) point2)
        {
            long dx = point1.x - point2.x;
            long dy = point1.y - point2.y;
            long dz = point1.z - point2.z;

            return dx * dx + dy * dy + dz * dz;
        }
	}
}
