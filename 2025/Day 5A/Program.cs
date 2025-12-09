Console.WriteLine("Hello!");
string path = "input.txt";
List<string> input = File.ReadAllLines(path).Where(line => !string.IsNullOrWhiteSpace(line)).ToList();
List<(ulong start, ulong end)> ranges = input
	.Where(line => line
		.Contains('-'))
	.Select(s =>
	{
		var parts = s.Split('-');
		return (ulong.Parse(parts[0]), ulong.Parse(parts[1]));
	})
	.ToList();
List<ulong> ids = input.Where(line => !line.Contains('-')).Select(ulong.Parse).ToList();

int fresh = 0;
foreach (ulong id in ids)
{
	foreach ((ulong start, ulong end) range in ranges)
	{
		if (id >= range.start && id <= range.end)
		{
			fresh++;
			break;
		}
	}
}
Console.WriteLine("Done!");
Console.WriteLine($"{fresh} ingredients are fresh!");