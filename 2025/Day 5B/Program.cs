Console.WriteLine("Hello!");
string path = "input.txt";
List<string> input = File.ReadAllLines(path).Where(line => !string.IsNullOrWhiteSpace(line)).ToList();
List<Range> ranges = input
	.Where(line => line
		.Contains('-'))
	.Select(s =>
	{
		var parts = s.Split('-');
		return new Range(ulong.Parse(parts[0]), ulong.Parse(parts[1]));
	})
	.OrderBy(r => r.Start)
	.ToList();
List<Range> mergedRanges = [];

while (ranges.Count > 0)
{
	Range currentRange = ranges[0];
	ranges.RemoveAt(0);
	int rangesWithinCount = -1;
	while (rangesWithinCount != 0)
	{
		List<Range> rangesWithin = ranges.Where(r => r.Start <= currentRange.End).ToList();
		rangesWithinCount = rangesWithin.Count;
		ranges.RemoveAll(r => r.Start <= currentRange.End);
		if (rangesWithin.Count > 0)
		{
			currentRange = new(currentRange.Start, Math.Max(currentRange.End, rangesWithin.Max(r => r.End)));
		}
	}
	mergedRanges.Add(currentRange);
}

List<ulong> lengths = mergedRanges.Select(r => r.Length).ToList();
ulong fresh = 0;
foreach (ulong length in lengths)
{
	fresh += length;
}

Console.WriteLine("Done!");
Console.WriteLine($"{fresh} ingredients are fresh!");

internal record Range(ulong Start, ulong End)
{
	public ulong Length => End - Start + 1;
};