using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using AdventOfCode2025.Interfaces;

// Expect a single argument: the day number (e.g., 1 for Day1)
if (args.Length == 0)
{
	return;
}

if (!int.TryParse(args[0], out var dayNumber))
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

var targetName = $"Day{dayNumber}";
var targetType = dayTypes.FirstOrDefault(t => string.Equals(t.Name, targetName, StringComparison.OrdinalIgnoreCase));

if (targetType == null)
{
	return; // no matching DayN class
}

var instance = provider.GetService(targetType) as IDay;

if (instance == null)
{
	return;
}

instance.Run();
