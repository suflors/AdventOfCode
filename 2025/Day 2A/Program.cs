Console.WriteLine("Hello!");
List<ulong> invalidIDs = [];
string path = "input.txt";
List<string> ranges = File.ReadAllLines(path).First().Split(',').ToList();

foreach (string range in ranges)
{
	List<string> rangeList = range.Split('-').ToList();
	ulong from = ulong.Parse(rangeList[0]);
	ulong to = ulong.Parse(rangeList[1]);
	for (ulong i = from; i <= to; i++)
	{
		if (i.ToString().Length % 2 == 1)
		{
			continue;
		}
		string id = i.ToString();
		string firstHalf = id.Substring(0, id.Length / 2);
		string secondHalf = id.Substring(id.Length / 2, id.Length / 2);
		if (firstHalf == secondHalf)
		{
			invalidIDs.Add(i);
		}
	}
}

Console.WriteLine("Done!");
ulong sum = 0;
foreach (ulong id in invalidIDs)
{
	sum += id;
}
Console.WriteLine($"Sum of invalid IDs: {sum}");