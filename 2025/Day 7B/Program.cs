Console.WriteLine("Hello!");
string path = "input.txt";
List<string> input = File.ReadAllLines(path).ToList();
Dictionary<int, ulong> timelines = [];
timelines.Add(input[0].IndexOf('S'), 1);

foreach (string line in input.Skip(1))
{
	foreach (int key in timelines.Keys.ToList())
	{
		if (line[key] == '^')
		{
			ulong value = timelines[key];
			if (key - 1 >= 0)
			{
				if (timelines.TryGetValue(key - 1, out ulong oldValue))
				{
					timelines[key - 1] = value + oldValue;
				} else
				{
					timelines[key - 1] = value;
				}
			}
			if (key + 1 < line.Length)
			{
				if (timelines.TryGetValue(key + 1, out ulong oldValue))
				{
					timelines[key + 1] = value + oldValue;
				} else
				{
					timelines[key + 1] = value;
				}
			}
			timelines.Remove(key);
		}
	}
}

ulong totalTimelines = 0;
foreach (ulong value in timelines.Values)
{
	totalTimelines += value;
}

Console.WriteLine("Done!");
Console.WriteLine($"Number of timelines: {totalTimelines}");