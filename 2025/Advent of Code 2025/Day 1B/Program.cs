Console.WriteLine("Hello!");

int position = 50; // starting pos
int zeroHits = 0;
string path = "input.txt";
List<string> commands = File.ReadAllLines(path).ToList();

foreach (string command in commands)
{
	position = turn(position, command, ref zeroHits);
	Console.WriteLine($"Current Position: {position}, Zero Hits: {zeroHits}");
}
Console.WriteLine($"All {commands.Count} turns successfully executed.");
Console.WriteLine($"End Position: {position}");
Console.WriteLine($"Total Zero Hits: {zeroHits}");

static int turn(int position, string command, ref int zeroHits)
{
	char direction = command[0];
	int turns = int.Parse(command.Substring(1));
	if (direction is 'L')
	{
		// basically, since we calculate zerohits via division, we'll skip one if it's turning left and hits one, so we add it manually
		// note: if we start from 0, it will erroneously think we are going to hit 0, so we account for that
		if (turns >= position && position is not 0)
		{
			zeroHits++;
		}
		turns *= -1;
	}
	position += turns;
	zeroHits += Math.Abs(position / 100);
	position %= 100;
	// fix negative numbers to allow rollover
	if (position < 0)
	{
		position += 100;
	}
	return position;
}