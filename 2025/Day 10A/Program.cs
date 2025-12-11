using System.Collections;
using System.Text.RegularExpressions;

Console.WriteLine("Hello!");
string path = "input.txt";
List<Machine> machines = File
	.ReadAllLines(path)
	.Select(s => new Machine(s))
	.ToList();

List<int> buttonsPressed = machines.Select(m => m.PowerOn()).ToList();
int totalButtonsPressed = buttonsPressed.Sum();

Console.WriteLine("Done!");
Console.WriteLine($"Total buttons pressed: {totalButtonsPressed}");

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

	public int PowerOn()
	{
		int buttonsToPress = 0;

		while (true)
		{
			buttonsToPress++;
			if (TryPresses(new BitArray(TargetIndicatorLights.Count), buttonsToPress))
				return buttonsToPress;
		}
	}

	private bool TryPresses(BitArray currentIndicatorLights, int buttonsToPress)
	{
		// did this combination work?
		if (buttonsToPress == 0)
			return ((BitArray)currentIndicatorLights.Clone())
				.Xor(TargetIndicatorLights)
				.OfType<bool>()
				.All(e => !e);

		// press every button until we're done
		foreach (List<int> button in ButtonWiringSchematics)
		{
			BitArray newIndicatorLights = (BitArray)currentIndicatorLights.Clone();
			foreach (int wire in button)
			{
				newIndicatorLights[wire] = !newIndicatorLights[wire];
			}
			if (TryPresses(newIndicatorLights, buttonsToPress - 1))
				return true; // if it worked, indicate higher iteration, else, next combination
		}

		return false; // no combination worked
	}
}