Console.WriteLine("Hello!");
string path = "input.txt";
List<List<char>> departmentMap = File.ReadAllLines(path).Select(line => line.ToList()).ToList();
int accessible = 0;

List<int> dRow = [-1, -1, -1, 0, 0, 1, 1, 1];
List<int> dCol = [-1, 0, 1, -1, 1, -1, 0, 1];
int removed = -1;
while (removed != 0)
{
	removed = 0;
	for (int x = 0; x < departmentMap.Count; x++)
	{
		for (int y = 0; y < departmentMap[x].Count; y++)
		{
			if (departmentMap[x][y] != '@')
			{
				continue;
			}
			int adjacent = 0;
			for (int i = 0; i < 8; i++)
			{
				int newX = x + dCol[i];
				int newY = y + dRow[i];
				if (newX >= 0 && newX < departmentMap.Count && newY >= 0 && newY < departmentMap[x].Count)
				{
					char adjacentChar = departmentMap[newX][newY];
					if (adjacentChar == '@')
					{
						adjacent++;
					}
				}
			}
			if (adjacent < 4)
			{
				departmentMap[x][y] = '.';
				removed++;
				accessible++;
			}
		}
	}
}

Console.WriteLine("Done!");
Console.WriteLine($"{accessible} rolls are accessible!");