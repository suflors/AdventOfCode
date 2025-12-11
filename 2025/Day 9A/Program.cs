Console.WriteLine("Hello!");
string path = "input.txt";
List<(long, long)> points = File
	.ReadAllLines(path)
	.Select(s => s
		.Split(',')
		.Select(s => long.Parse(s))
		.ToList()
	)
	.Select(i => (i[0], i[1]))
	.ToList();

List<long> areas = [];
for (int i = 0; i < points.Count; i++)
{
	for (int j = i + 1; j < points.Count; j++)
	{
		areas.Add((1 + Math.Abs(points[i].Item1 - points[j].Item1)) * (1 + Math.Abs(points[i].Item2 - points[j].Item2)));
	}
}
long result = areas.Max();
Console.WriteLine("Done!");
Console.WriteLine($"Largest rectangle area: {result}");
