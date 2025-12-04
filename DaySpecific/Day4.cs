using System;
using System.Text;
using AdventOfCode2025.Helper;

namespace AdventOfCode2025.DaySpecific
{
	public class Day4 : DayBase
	{
		private static readonly int[,] directions = new int[,] { { 0, -1 }, { 0, 1 }, { -1, 0 }, { 1, 0 }, { -1, -1 }, { -1, 1 }, { 1, -1 }, { 1, 1 } };
		public override void Part1()
		{
			var lines = ReadInputLines();
            int rolls = 0;
            int n = lines.Length;
            int m = lines[0].Length;
            int d = directions.GetLength(0);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    char roll = lines[i][j];
                    int adj = 0;

                    if (roll != '@')
                        continue;

                    for (int k = 0; k < d; k++)
                    {
                        int x = i + directions[k, 0];
                        int y = j + directions[k, 1];

                        if (x < 0 || x >= n || y < 0 || y >= m)
                            continue;
                        
                        if (lines[x][y] == '@')
                            adj++;
                    }

                    if (adj < 4)
                        rolls++;
                }
            }
            
            Console.WriteLine($"Day4 Part1: {rolls}.");
		}

		public override void Part2()
		{
			var lines = ReadInputLines();
            int rolls = 0;
            int n = lines.Length;
            int m = lines[0].Length;
            int d = directions.GetLength(0);
            char[][] map = new char[n][];
            char[][] clone = new char[n][];
            bool hasAccessible = true;

            for (int i = 0; i < n; i++)
            {
                map[i] = lines[i].ToCharArray();                
                clone[i] = lines[i].ToCharArray();
            }

            while (hasAccessible)
            {
                hasAccessible = false;

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        char roll = map[i][j];
                        int adj = 0;

                        if (roll != '@')
                            continue;

                        for (int k = 0; k < d; k++)
                        {
                            int x = i + directions[k, 0];
                            int y = j + directions[k, 1];

                            if (x < 0 || x >= n || y < 0 || y >= m)
                                continue;
                            
                            if (map[x][y] == '@')
                                adj++;
                        }

                        if (adj < 4)
                        {
                            rolls++;
                            map[i][j] = 'x';
                            hasAccessible = true;
                        }
                    }
                }

                if (hasAccessible)
                    continue;

                for (int i = 0; i < n; i++)
                {
                    if (clone[i] != map[i])
                    {
                        clone = map;
                        hasAccessible = true;
                        break;
                    }
                }
            }

			Console.WriteLine($"Day4 Part2: {rolls}.");
		}
	}
}
