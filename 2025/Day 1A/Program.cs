Console.WriteLine("Hello!");

int position = 50; // starting pos
int zeroHits = 0;
string path = "input.txt";
List<string> commands = File.ReadAllLines(path).ToList();

foreach (string command in commands)
{
	position = turn(position, command);
	if (position is 0)
	{
		zeroHits++;
	}
}
Console.WriteLine($"All {commands.Count} turns successfully executed.");
Console.WriteLine($"End Position: {position}");
Console.WriteLine($"Total Zero Hits: {zeroHits}");

static int turn(int position, string command)
{
	char direction = command[0];
	int turns = int.Parse(command.Substring(1));
	if (direction is 'L')
	{
		position -= turns;
		// this is kind of disgusting but it's simple and effective, extract the negative number we get and turn it into a positive one by adding it to 100
		position %= 100;
		position += 100;
	} else
	{
		position += turns;
	}
	position %= 100;
	return position;
}