using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2025.Helper;
using System.Numerics;

namespace AdventOfCode2025.DaySpecific
{
	public class Day9 : DayBase
	{
        public override void Part1()
		{
            var lines = ReadInputLines();
            int n = lines.Length;
            long res = 0;
			List<(long x, long y)> squares = new();

			for (int i = 0; i < n; i++)
            {
                string[] pos = lines[i].Split(",");
				long a = long.Parse(pos[0]);
				long b = long.Parse(pos[1]);

				squares.Add((a, b));
            }

			int c = squares.Count;

			for (int i = 0; i < c - 1; i++)
            {
                for (int j = i + 1; j < c; j++)
                {
					if (squares[i].x == squares[j].x || squares[i].y == squares[j].y)
						continue;

					res = Math.Max(GetArea(squares[i], squares[j]), res);					
                }
            }

            Console.WriteLine($"{GetType().Name} {nameof(Part1)}: {res}.");
		}

		public override void Part2()
		{
			var lines = ReadInputLines();
            int n = lines.Length;
            long res = 0;

			List<VectorXY> points = new();

			for (int i = 0; i < n; i++)
            {
                string[] pos = lines[i].Split(",");
				VectorXY point = new VectorXY(Int32.Parse(pos[0]), Int32.Parse(pos[1]));
				points.Add(point);
            }
			
			HashSet<VectorXY> edges = new();
			HashSet<VectorXY> lPoints = new();
        	HashSet<VectorXY> rPoints = new();

			for (int x = 0; x < points.Count; x++)
			{
				var pointA = points[x];
				var pointB = points[(x + 1) % points.Count];
				var diff = pointB - pointA;
				var diffLen = diff.ManhattanMetric();
				var diffDir = new VectorXY(Math.Sign(diff.x), Math.Sign(diff.y));
				
				for (int i = 0; i <= diffLen; i++)
				{
					VectorXY pos = pointA + diffDir.Scale(i);

					edges.Add(pos);
					lPoints.Add(pos + diffDir.RotatedLeft());
					rPoints.Add(pos + diffDir.RotatedRight());
				}
			}

			var leftMost = points.MinBy(p => p.y);
			var leftProbe = leftMost.NextLeft();
			HashSet<VectorXY> inside;
			HashSet<VectorXY> outside;

			if (lPoints.Contains(leftProbe))
			{
				outside = new(lPoints.Except(edges));
				inside = new(rPoints.Except(edges));
			}
			else if (rPoints.Contains(leftProbe))
			{
				outside = new(rPoints.Except(edges));
				inside = new(lPoints.Except(edges));
			}
            else // this should never happen
            {
                outside = new(rPoints.Except(edges));
				inside = new(lPoints.Except(edges));
            }

			int a = 0;
        	int b = 0;
			
			for (a = 0; a < points.Count; a++)
			{
				for (b = a + 1; b < points.Count; b++)
				{                
					var cornerA = points[a];
					var cornerB = points[b];
					var top = Math.Min(cornerA.x, cornerB.x);
					var bottom = Math.Max(cornerA.x, cornerB.x);
					var left = Math.Min(cornerA.y, cornerB.y);
					var right = Math.Max(cornerA.y, cornerB.y);
					bool inPolygon = true;

					for (int row = top; row <= bottom && inPolygon; row++)
					{
						inPolygon = !outside.Contains(new(row, left)) && !outside.Contains(new(row, right));
					}
					for (int col = left; col <= right && inPolygon; col++)
					{
						inPolygon = !outside.Contains(new(top, col)) && !outside.Contains(new(bottom, col));
					}

					if (inPolygon)
					{
						int height = Math.Abs(points[b].x - points[a].x) + 1;
						int width = Math.Abs(points[b].y - points[a].y) + 1;
						long area = Math.BigMul(height, width);
						if (area > res)
						{
							res = area;
						}
					}
				}
			}

			Console.WriteLine($"{GetType().Name} {nameof(Part2)}: {res}.");
		}

		private long GetArea((long x, long y) point1, (long x, long y) point2)
        {
            return Math.Abs(point1.x - point2.x + 1) * Math.Abs(point1.y - point2.y + 1);
        }
	}
}
