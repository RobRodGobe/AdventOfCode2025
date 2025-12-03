using System;
using System.IO;
using AdventOfCode2025.Interfaces;

namespace AdventOfCode2025.Helper
{
    public abstract class DayBase : IDay
    {
        public void Run()
        {
            Part1();
            Part2();
        }

        public abstract void Part1();

        public abstract void Part2();

        protected string[] ReadInputLines()
        {
            var typeName = GetType().Name; // e.g., Day1
            var cwd = Directory.GetCurrentDirectory();
            var path = Path.Combine(cwd, "InputFiles", $"{typeName}.txt");
            if (!File.Exists(path))
            {
                return Array.Empty<string>();
            }

            return File.ReadAllLines(path);
        }
    }
}
