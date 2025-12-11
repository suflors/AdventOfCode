Console.WriteLine("Hello!");
string path = "input.txt";
List<WokeVector2> points = File
	.ReadAllLines(path)
	.Select(s => s
		.Split(',')
		.Select(s => long.Parse(s))
		.ToList()
	)
	.Select(i => new WokeVector2(i[0], i[1]))
	.ToList();

List<(WokeVector2, WokeVector2)> verticalLines = [];
List<(WokeVector2, WokeVector2)> horizontalLines = [];
for (int i = 0; i < points.Count; i++)
{
	WokeVector2 currentPoint = points[i];
	WokeVector2 nextPoint;
	if (i == points.Count - 1)
	{
		nextPoint = points[0];
	} else
	{
		nextPoint = points[i + 1];
	}

	if (currentPoint.Y == nextPoint.Y)
	{
		horizontalLines.Add((currentPoint, nextPoint));
	} else if (currentPoint.X == nextPoint.X)
	{
		verticalLines.Add((currentPoint, nextPoint));
	} else
	{
		throw new Exception("We seem to have misunderstood the input data!");
	}
}

List<long> areas = [];



for (int i = 0; i < points.Count; i++)
{
	for (int j = i + 1; j < points.Count; j++)
	{
		WokeVector2 pointA = points[i], pointB = points[j];
		var maxY = Math.Max(pointA.Y, pointB.Y);
		var minY = Math.Min(pointA.Y, pointB.Y);
		var maxX = Math.Max(pointA.X, pointB.X);
		var minX = Math.Min(pointA.X, pointB.X);
		var horizontalLinesBetween = horizontalLines.Where(l =>
			l.Item1.Y < maxY &&
			l.Item1.Y > minY &&
			(
				(l.Item1.X > minX && l.Item1.X < maxX) ||
				(l.Item2.X > minX && l.Item2.X < maxX) ||
				(Math.Min(l.Item1.X, l.Item2.X) <= minX && Math.Max(l.Item1.X, l.Item2.X) >= maxX)
			)
		);
		if (horizontalLinesBetween.Any())
			continue;

		var verticalLinesBetween = verticalLines.Where(l =>
			l.Item1.X < maxX &&
			l.Item1.X > minX &&
			(
				(l.Item1.Y > minY && l.Item1.Y < maxY) ||
				(l.Item2.Y > minY && l.Item2.Y < maxY) ||
				(Math.Min(l.Item1.Y, l.Item2.Y) <= minY && Math.Max(l.Item1.Y, l.Item2.Y) >= maxY)
			)
		);
		if (verticalLinesBetween.Any())
			continue;

		areas.Add((1 + Math.Abs(pointA.X - pointB.X)) * (1 + Math.Abs(pointA.Y - pointB.Y)));
	}
}
long result = areas.Max();
Console.WriteLine("Done!");
Console.WriteLine($"Largest rectangle area: {result}");

internal record WokeVector2(long X, long Y);