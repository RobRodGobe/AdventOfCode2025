using System;
using AdventOfCode2025.Helper;

namespace AdventOfCode2025.DaySpecific
{
	public class Day1 : DayBase
	{
		public override void Part1()
		{
			var lines = ReadInputLines();
            int start = 50;
            int n = lines.Length;
            int count = 0;
			
			for (int i = 0; i < n; i++)
			{
				string line = lines[i];
                string dir = line.Substring(0, 1);
                int pos = 0;
                string posStr = line.Substring(1);
                
                Int32.TryParse(posStr, out pos);
                
                if (dir == "L")
                    pos *= -1;

                start += pos;

                while (start < 0)
                    start += 100;

                while (start >= 100)
                    start -= 100;

                if (start % 100 == 0)
                    count++;
			}

            Console.WriteLine($"{GetType().Name} {nameof(Part1)}: Final position {start}, crossed 0 a total of {count} times.");
		}

		public override void Part2()
		{
			var lines = ReadInputLines();
            int start = 50;
            int n = lines.Length;
            int count = 0;
			
			for (int i = 0; i < n; i++)
			{
				string line = lines[i];
                string dir = line.Substring(0, 1);
                int pos = 0;
                string posStr = line.Substring(1);
                
                Int32.TryParse(posStr, out pos);
                
                if (dir == "L")
                    pos *= -1;

                if (pos > 0)
                {
                    count += (int)(Math.Floor((double)(start + pos) / 100) - Math.Floor(start / 100d));
                }
                else
                {
                    count += (int)(Math.Floor((double)(start - 1) / 100) - Math.Floor((start - 1 + pos) / 100d));
                }

                start = (start + pos + 100 % 100);
            }            

            Console.WriteLine($"{GetType().Name} {nameof(Part2)}: Final position {start}, crossed 0 a total of {count} times.");
		}
	}
}
