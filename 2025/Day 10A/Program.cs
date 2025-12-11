using System.Collections;
using System.Text.RegularExpressions;

Console.WriteLine("Hello!");
string path = "input.txt";
List<Machine> points = File
	.ReadAllLines(path)
	.Select(s => new Machine(s))
	.ToList();

Console.WriteLine("Done!");

internal class Machine
{
	private readonly Regex machinePattern = new(@"\[(.+)](?: \(([^)]+)\))+ \{(.+)}", RegexOptions.Compiled);

	public BitArray TargetIndicatorLights { get; }
	public List<List<int>> ButtonWiringSchematics { get; }
	public List<int> JoltageRequirements { get; }

	public Machine(string input)
	{
		Match match = machinePattern.Match(input);

		if (!match.Success)
			throw new ArgumentException("Could not parse input!", input);

		TargetIndicatorLights = new(match.Groups[1].Value.Select(c => c == '#').ToArray());
		ButtonWiringSchematics = match.Groups[2].Captures.Select(c => c.Value.Split(',').Select(s => int.Parse(s)).ToList()).ToList();
		JoltageRequirements = match.Groups[3].Value.Split(',').Select(int.Parse).ToList();
	}
}