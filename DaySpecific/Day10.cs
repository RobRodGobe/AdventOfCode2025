using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2025.Helper;
using Microsoft.Z3;

namespace AdventOfCode2025.DaySpecific
{
	public class Day10 : DayBase
	{
        public override void Part1()
		{
            var lines = ReadInputLines();
            int n = lines.Length;
            int res = 0;
			List<Machine> machines = new List<Machine>();

			for (int i = 0; i < n; i++)
            {
                string[] elements = lines[i].Split(" ");
				int len = elements.Length;

				string pattern = elements[0][1..^1];
				var target = pattern.Select(c => c == '#').ToList();

				List<HashSet<int>> wirings = new ();
				int k = 1;

				while (k < len && elements[k].StartsWith('('))
                {
                    var buttons = elements[k][1..^1].Split(',').Select(e => Int32.Parse(e));
					wirings.Add(buttons.ToHashSet());
					k++;
                }

				List<int> joltages = new();
				
				if (k < len && elements[k].StartsWith('{'))
					joltages = elements[k][1..^1].Split(',').Select(e => Int32.Parse(e)).ToList();

				machines.Add(new Machine(target, wirings, joltages));
            }

			int m = machines.Count;

			for (int i = 0; i < m; i++)
            {
				int b = machines[i].Wirings.Count;
				int tLen = machines[i].Diagrams.Count;
				bool[] state = new bool[tLen];

                for (int j = 0; j < b; j++)
                {
                    if (CanSolve(machines[i], j, 0, 0, state))					
					{
						res += j;
						break;
					}
                }
            }

			Console.WriteLine($"{GetType().Name} {nameof(Part1)}: {res}.");
		}

		public override void Part2()
        {
            var lines = ReadInputLines();
            int n = lines.Length;
            long res = 0;
            List<Machine> machines = new List<Machine>();

            for (int i = 0; i < n; i++)
            {
                string[] elements = lines[i].Split(" ");
                int len = elements.Length;

                string pattern = elements[0][1..^1];
                var target = pattern.Select(c => c == '#').ToList();

                List<HashSet<int>> wirings = new();
                int k = 1;

                while (k < len && elements[k].StartsWith('('))
                {
                    var buttons = elements[k][1..^1].Split(',').Select(e => Int32.Parse(e));
                    wirings.Add(buttons.ToHashSet());
                    k++;
                }

                List<int> joltages = new();

                if (k < len && elements[k].StartsWith('{'))
                    joltages = elements[k][1..^1].Split(',').Select(e => Int32.Parse(e)).ToList();

                machines.Add(new Machine(target, wirings, joltages));
            }

            int m = machines.Count;

            for (int i = 0; i < m; i++)
            {
                res += CalculateJoltages(machines[i]);
            }

            Console.WriteLine($"{GetType().Name} {nameof(Part2)}: {res}.");
        }

        private static long CalculateJoltages(Machine machine)
        {
            using var ctx = new Context();
            using var opt = ctx.MkOptimize();

            var presses = Enumerable.Range(0, machine.Wirings.Count)
                .Select(i => ctx.MkIntConst($"p{i}"))
                .ToArray();

            foreach (var press in presses)
                opt.Add(ctx.MkGe(press, ctx.MkInt(0)));

            for (int i = 0; i < machine.Joltages.Count; i++)
            {
                var affecting = presses.Where((_, j) => machine.Wirings[j].Contains(i)).ToArray();
                if (affecting.Length > 0)
                {
                    ArithExpr sum = affecting.Length == 1 ? affecting[0] : ctx.MkAdd(affecting);
                    opt.Add(ctx.MkEq(sum, ctx.MkInt(machine.Joltages[i])));
                }
            }

            opt.MkMinimize(presses.Length == 1 ? presses[0] : ctx.MkAdd(presses));
            if (opt.Check() != Status.SATISFIABLE)
                throw new Exception("No solution found");

            var model = opt.Model;
            return presses.Sum(p => ((IntNum)model.Evaluate(p, true)).Int64);
        }

		private record Machine(List<bool> Diagrams, List<HashSet<int>> Wirings, List<int> Joltages);

		private bool CanSolve(Machine mac, int t, int start, int depth, bool[] macState)
        {
			int d = mac.Wirings.Count;

			if (t == depth)
				return macState.SequenceEqual(mac.Diagrams);

			for (int i = start; i <= d - (t - depth); i++)
            {
                Toggle(mac.Wirings[i], macState);

				if (CanSolve(mac, t, i + 1, depth + 1, macState))
					return true;

				Toggle(mac.Wirings[i], macState);
            }

			return false;
        }

		private void Toggle(HashSet<int> lights, bool[] macState)
        {
            foreach (int i in lights.Where(i => i < macState.Length))
				macState[i] = !macState[i];
        }
	}
}
