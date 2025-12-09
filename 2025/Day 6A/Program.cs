Console.WriteLine("Hello");
string path = "input.txt";
List<List<string>> input = File.ReadAllLines(path).Select(l => l.Split(null as char[], StringSplitOptions.RemoveEmptyEntries).ToList()).ToList();
List<ulong> answers = [];

for (int i = 0; i < input[input.Count - 1].Count; i++)
{
	string operatorStr = input[input.Count - 1][i];
	ulong answer = 0;
	if (operatorStr == "+")
	{
		for (int j = 0; j < input.Count - 1; j++)
		{
			answer += ulong.Parse(input[j][i]);
		}
	} else if (operatorStr == "*")
	{
		answer = 1; // don't forget about multiplying by 0
		for (int j = 0; j < input.Count - 1; j++)
		{
			answer *= ulong.Parse(input[j][i]);
		}
	} else
	{
		throw new Exception($"invalid operator \"{operatorStr}\"");
	}
	answers.Add(answer);
}

ulong total = 0;
foreach (ulong answer in answers)
{
	total += answer;
}
Console.WriteLine("Done!");
Console.WriteLine($"Total of all answers: {total}");