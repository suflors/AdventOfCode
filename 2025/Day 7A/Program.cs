using System.Text;

Console.WriteLine("Hello!");
string path = "input.txt";
List<string> input = File.ReadAllLines(path).ToList();
StringBuilder beam = new(input[0]);
input.RemoveAt(0);
int splits = 0;
foreach (string line in input)
{
	for (int i = 0; i < beam.Length; i++)
	{
		if (line[i] == '^')
		{
			if (beam[i] == 'S')
			{
				splits++;
				beam[i] = '.';
				if (i - 1 >= 0)
				{
					beam[i - 1] = 'S';
				}
				if (i + 1 < beam.Length)
				{
					beam[i + 1] = 'S';
				}
			}
		}
	}
}
Console.WriteLine("Done!");
Console.WriteLine($"Number of splits: {splits}");