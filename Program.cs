using System.Numerics;

bool successfulTry = true;
decimal sessionLength = 0;
string input;
int breakCount = 0;

do
{
    Console.Clear();
    successfulTry = true;
    Console.WriteLine("Enter the length of your study session in minutes:");
    input = Console.ReadLine();
    if (!(decimal.TryParse(input, out sessionLength)) || sessionLength <= 0)
    {
        Console.WriteLine("That didn't seem to be a valid number or is less than or equal to 0. Please try again.");
        successfulTry = false;
    }
} while (!successfulTry);

do
{
    Console.Clear();
    successfulTry = true;
    Console.WriteLine("Enter the integer number of ten-second breaks you want to have:");
    input = Console.ReadLine();
    if (!(int.TryParse(input, out breakCount)) || breakCount < 0)
    {
        Console.WriteLine("That didn't seem to be a valid integer or is less than 0. Please try again.");
        successfulTry = false;
    }
} while (!successfulTry);

Console.WriteLine($"Press any key to start your {sessionLength} minute(s) study session with {breakCount} break(s)");
if (sessionLength >= 60)
{
    int sessLengthHours = (int)(sessionLength / 60);
    int sessLengthHoursMins = (int)(sessionLength % 60);
    Console.WriteLine($"That's {sessLengthHours} hour(s) and {sessLengthHoursMins} minute(s)!");
}

Console.ReadKey(true);

Console.Clear();

int sessionLengthSeconds = (int)(sessionLength * 60);
List<int> breakTimes = new List<int>();
Random rnd = new Random();

// Generate random break times
while (breakTimes.Count < breakCount)
{
    int breakTime = rnd.Next(10, sessionLengthSeconds - 10); // Random time for break start (must fit within session length)

    // Ensure breaks are not overlapping (each break is 10 seconds long)
    if (!breakTimes.Any(b => Math.Abs(b - breakTime) < 10))
    {
        breakTimes.Add(breakTime);
    }
}

// Sort break times for easier management during the session
breakTimes.Sort();

Console.WriteLine("Stop looking here, focus on your revision");
int elapsed = 0;
int breakIndex = 0;

// Study session loop
while (elapsed < sessionLengthSeconds)
{
    Thread.Sleep(1000); // Wait for 1 second
    elapsed++;

    // Check if it's time for a break
    if (breakIndex < breakTimes.Count && elapsed == breakTimes[breakIndex])
    {
        Console.Beep(); // Signal the break start
        Console.WriteLine($"Time for a 10-second break! Break {breakIndex + 1}/{breakCount}");
        Thread.Sleep(10000); // 10-second break duration
        elapsed += 10;

        breakIndex++;
        Console.WriteLine("Break over, back to work!");
        Console.Beep();
    }
}

Console.WriteLine("Study session complete!");
