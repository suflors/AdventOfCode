Console.WriteLine("Hello!");
string path = "input.txt";
List<string> input = File.ReadAllLines(path).ToList();

List<ulong> currentOperands = [];
List<Equation> equations = [];
for (int i = input[input.Count - 1].Length - 1; i >= 0; i--)
{
	string currentOperand = string.Empty;
	for (int j = 0; j < input.Count; j++)
	{
		char currentChar = input[j][i];
		if (currentChar == ' ')
		{
			continue;
		} else if (currentChar == '*' || currentChar == '+')
		{
			if (!string.IsNullOrEmpty(currentOperand))
			{
				currentOperands.Add(ulong.Parse(currentOperand));
				currentOperand = string.Empty;
			}
			equations.Add(new(currentOperands, currentChar));
			currentOperands = [];
		} else
		{
			currentOperand += currentChar;
		}
	}
	if (!string.IsNullOrEmpty(currentOperand))
	{
		currentOperands.Add(ulong.Parse(currentOperand));
		currentOperand = string.Empty;
	}
}
List<ulong> answers = equations.Select(e => e.Solve()).ToList();

ulong total = 0;
foreach (ulong answer in answers)
{
	total += answer;
}
Console.WriteLine("Done!");
Console.WriteLine($"Total of all answers: {total}");

internal record Equation(List<ulong> operands, char operatorChar)
{
	public ulong Solve()
	{
		ulong result = 0;
		if (operatorChar == '+')
		{
			foreach (ulong operand in operands)
			{
				result += operand;
			}
		} else if (operatorChar == '*')
		{
			result = 1; // don't forget about multiplying by 0
			foreach (ulong operand in operands)
			{
				result *= operand;
			}
		} else
		{
			throw new Exception($"invalid operator \"{operatorChar}\"");
		}
		return result;
	}
}