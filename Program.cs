using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using AdventOfCode2025.Interfaces;

// Expect a single argument: the day number (e.g., 1 for Day1) or "all" to run all days
if (args.Length == 0)
{
    return;
}

bool runAll = args[0] == "all";
int dayNumber = -1;

if (!runAll && !int.TryParse(args[0], out dayNumber))
{
    return;
}

var services = new ServiceCollection();

// Register all IDay implementations found in this assembly
var asm = Assembly.GetExecutingAssembly();
var dayTypes = asm.GetTypes()
    .Where(t => typeof(IDay).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
    .ToArray();

foreach (var t in dayTypes)
{
    services.AddTransient(t);
}

var provider = services.BuildServiceProvider();

// Benchmark entire run
var totalSw = Stopwatch.StartNew();

if (runAll)
{
    // select types named Day{number} and sort by number ascending
    var numbered = dayTypes
        .Select(t => new { Type = t, Name = t.Name })
        .Where(x => x.Name.StartsWith("Day", StringComparison.OrdinalIgnoreCase))
        .Select(x =>
        {
            var suffix = x.Name.Substring(3);
            return (x.Type, Number: int.TryParse(suffix, out var v) ? (int?)v : null);
        })
        .Where(x => x.Number.HasValue)
        .OrderBy(x => x.Number)
        .ToList();

    foreach (var item in numbered)
    {
        var inst = provider.GetService(item.Type) as IDay;
        if (inst == null)
            continue;

        var sw = Stopwatch.StartNew();
        inst.Run();
        sw.Stop();

        Console.WriteLine($"{item.Type.Name} completed in {sw.ElapsedMilliseconds} ms");
    }

    totalSw.Stop();
    Console.WriteLine($"All days completed in {totalSw.ElapsedMilliseconds} ms");
    return;
}

var targetName = $"Day{dayNumber}";
var targetType = dayTypes.FirstOrDefault(t => 
    string.Equals(t.Name, targetName, StringComparison.OrdinalIgnoreCase));

if (targetType == null)
{
    return; // no matching DayN class
}

var instance = provider.GetService(targetType) as IDay;

if (instance == null)
{
    return;
}

var singleSw = Stopwatch.StartNew();
instance.Run();
singleSw.Stop();

Console.WriteLine($"{targetName} completed in {singleSw.ElapsedMilliseconds} ms");

totalSw.Stop();
Console.WriteLine($"Total execution time: {totalSw.ElapsedMilliseconds} ms");
