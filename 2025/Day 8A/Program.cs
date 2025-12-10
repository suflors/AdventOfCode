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
		edges.Add(new Edge(i, j, Vector3.Distance(points[i], points[j])));
	}
}
edges = edges.OrderBy(e => e.Distance).ToList();

List<Edge> selectedEdges = [];
int edgesNeeded = 1000;
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

List<int> sizes = unionFind.GetComponentSizes();
sizes = sizes.OrderDescending().ToList();
ulong result = (ulong)(sizes[0] * sizes[1] * sizes[2]);
Console.WriteLine("Done!");
Console.WriteLine($"Product of 3 largest circuits: {result}");

internal record Edge(int IndexA, int IndexB, double Distance);

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


	public List<int> GetComponentSizes()
	{
		var map = new Dictionary<int, int>();
		for (int i = 0; i < parent.Length; i++)
		{
			int root = Find(i);
			if (!map.ContainsKey(root))
				map[root] = 0;
			map[root]++;
		}
		return map.Values.ToList();
	}
}