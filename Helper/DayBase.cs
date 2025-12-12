using System;
using System.IO;
using System.Diagnostics;
using AdventOfCode2025.Interfaces;

namespace AdventOfCode2025.Helper
{
    public abstract class DayBase : IDay
    {
        public void Run()
        {   
            RunTimed("Part1", Part1);
            RunTimed("Part2", Part2);
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

        private void RunTimed(string label, Action action)
        {
            var typeName = GetType().Name;
            var sw = Stopwatch.StartNew();
            action();
            sw.Stop();
            Console.WriteLine($"{typeName} {label} completed in {sw.ElapsedMilliseconds} ms");
        }
    }
}
