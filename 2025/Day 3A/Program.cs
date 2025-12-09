Console.WriteLine("Hello!");
string path = "input.txt";
List<string> banks = File.ReadAllLines(path).ToList();
List<int> joltages = [];

foreach (string bank in banks)
{
	string firstBatteryRange = bank.Substring(0, bank.Length - 1);
	int firstBatteryIndex = -1, secondBatteryIndex = -1;
	char firstBattery = ' ', secondBattery = ' ';
	for (int i = 9; i >= 0; i--)
	{
		firstBatteryIndex = firstBatteryRange.IndexOf(i.ToString());
		if (firstBatteryIndex != -1)
		{
			firstBattery = firstBatteryRange[firstBatteryIndex];
			break;
		}
	}
	if (firstBatteryIndex == -1)
	{
		throw new Exception($"Couldn't find a first battery in {bank}");
	}

	string secondBatteryRange = bank.Substring(firstBatteryIndex + 1);
	for (int i = 9; i >= 0; i--)
	{
		secondBatteryIndex = secondBatteryRange.IndexOf(i.ToString());
		if (secondBatteryIndex != -1)
		{
			secondBattery = secondBatteryRange[secondBatteryIndex];
			break;
		}
	}
	if (secondBatteryIndex == -1)
	{
		throw new Exception($"Couldn't find a second battery in {bank}");
	}

	if (firstBattery != ' ' && secondBattery != ' ')
	{
		joltages.Add(int.Parse($"{firstBattery}{secondBattery}"));
	} else
	{
		throw new Exception("Somehow, batteries were not assigned to their variables.");
	}
}

Console.WriteLine("Done!");
int sum = 0;
foreach (int joltage in joltages)
{
	sum += joltage;
}
Console.WriteLine($"Sum of joltages: {sum}");