using System.Numerics;

Console.WriteLine("Hello!");
string path = "input.txt";
List<Vector3> points = File
	.ReadAllLines(path)
	.Select(s => s
		.Split(',')
		.Select(s => float.Parse(s))
		.ToList()
	)
	.Select(f => new Vector3(f[0], f[1], f[2]))
	.ToList();

List<Edge> edges = [];
for (int i = 0; i < points.Count; i++)
{
	for (int j = i + 1; j < points.Count; j++)
	{
		edges.Add(new Edge(i, j, points[i], points[j], Vector3.Distance(points[i], points[j])));
	}
}
edges = edges.OrderBy(e => e.Distance).ToList();

List<Edge> selectedEdges = [];
int edgesNeeded = 999;
UnionFind unionFind = new(points.Count);

foreach (Edge edge in edges)
{
	if (unionFind.Union(edge.IndexA, edge.IndexB))
	{
		selectedEdges.Add(edge);
		if (selectedEdges.Count >= edgesNeeded)
			break;
	}
}

ulong result = (ulong)(selectedEdges[edgesNeeded - 1].PointA.X * selectedEdges[edgesNeeded - 1].PointB.X);
Console.WriteLine("Done!");
Console.WriteLine($"Product of the last selected edge's point's x values: {result}");

internal record Edge(int IndexA, int IndexB, Vector3 PointA, Vector3 PointB, double Distance);

internal class UnionFind
{
	private int[] parent;
	private int[] rank;

	public UnionFind(int size)
	{
		parent = new int[size];
		rank = new int[size];

		for (int i = 0; i < size; i++)
		{
			parent[i] = i;
		}
	}

	public int Find(int x)
	{
		if (parent[x] != x)
		{
			parent[x] = Find(parent[x]);
		}
		return parent[x];
	}

	public bool Union(int x, int y)
	{
		int rootX = Find(x);
		int rootY = Find(y);

		if (rootX == rootY)
			return false;

		if (rank[rootX] < rank[rootY])
		{
			parent[rootX] = rootY;
			rank[rootY] += rank[rootX];
		} else
		{
			parent[rootY] = rootX;
			rank[rootX] += rank[rootY];
		}

		return true;
	}
}