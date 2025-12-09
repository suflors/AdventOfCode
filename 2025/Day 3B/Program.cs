Console.WriteLine("Hello!");
string path = "input.txt";
List<string> banks = File.ReadAllLines(path).ToList();
List<ulong> joltages = [];

foreach (string bank in banks)
{
	List<int> bankDigits = bank.Select(ch => (int)char.GetNumericValue(ch)).ToList();
	int startIndex = 0;
	string joltageString = string.Empty;
	for (int i = 12; i > 0; i--)
	{
		List<int> bankDigitsSubrange = bankDigits.GetRange(startIndex, bankDigits.Count - startIndex - (i - 1));
		int max = bankDigitsSubrange.Max();
		int index = bankDigitsSubrange.IndexOf(max);
		joltageString += max;
		startIndex += index + 1;
	}
	joltages.Add(ulong.Parse(joltageString));
}

Console.WriteLine("Done!");
ulong sum = 0;
foreach (ulong joltage in joltages)
{
	sum += joltage;
}
Console.WriteLine($"Sum of joltages: {sum}");