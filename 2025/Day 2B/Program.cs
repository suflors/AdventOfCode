Console.WriteLine("Hello!");
List<ulong> invalidIDs = [];
string path = "input.txt";
List<string> ranges = File.ReadAllLines(path).First().Split(',').ToList();
var factors = new Dictionary<int, List<int>>();


foreach (string range in ranges)
{
	List<string> rangeList = range.Split('-').ToList();
	ulong from = ulong.Parse(rangeList[0]);
	ulong to = ulong.Parse(rangeList[1]);

	for (ulong i = from; i <= to; i++)
	{
		string id = i.ToString();
		List<int> currentFactors = [];
		int length = i.ToString().Length;

		if (!factors.TryGetValue(length, out List<int>? value))
		{
			for (int j = 1; j < length; j++)
			{
				if (length % j == 0)
				{
					currentFactors.Add(j);
				}
			}
			factors[length] = currentFactors;
		} else
		{
			currentFactors = value;
		}

		foreach (int factor in currentFactors)
		{
			List<string> parts = [];

			for (int j = 0; j < length; j += factor)
			{
				parts.Add(id.Substring(j, factor));
			}

			if (parts.Distinct().Count() == 1)
			{
				invalidIDs.Add(i);
				break;
			}
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