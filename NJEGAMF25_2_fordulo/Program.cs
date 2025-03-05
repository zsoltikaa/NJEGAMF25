Console.ForegroundColor = ConsoleColor.Green;

// 1. task (a part) --------------------------------------------------------------------------

string[] times = File.ReadAllLines("idopontok.txt");

Console.WriteLine("-----------------------------------------------------");

Console.WriteLine("1. feladat: \n");

double maxAngle = 0;
string maxTime = "";

foreach (string time in times)
{

    string[] parts = time.Split(' ');
    int h = int.Parse(parts[0]) % 12;
    int m = int.Parse(parts[1]);

    double minuteAngle = m * 6;

    double hourAngle = h * 30 + m * 0.5;

    double angle = Math.Abs(hourAngle - minuteAngle);

    angle = Math.Min(angle, 360 - angle);

    if (angle > maxAngle)
    {
        maxAngle = angle;
        maxTime = $"{h:D2}:{m:D2}";
    }

}

Console.WriteLine($"A resz: {maxTime}");

// 1. task (b part) --------------------------------------------------------------------------

double minAngleChange = double.MaxValue;
double prevAngle = 0;
bool first = true;

foreach (string time in times)
{

    string[] parts = time.Split(' ');
    int h = int.Parse(parts[0]) % 12;
    int m = int.Parse(parts[1]);

    double minuteAngle = m * 6;

    double hourAngle = h * 30 + m * 0.5;

    double angle = Math.Abs(hourAngle - minuteAngle);
    angle = Math.Min(angle, 360 - angle);

    if (!first)
    {
        double change = Math.Abs(angle - prevAngle);
        if (change < minAngleChange)
        {
            minAngleChange = change;
        }
    }

    prevAngle = angle;
    first = false;

}

Console.WriteLine($"B resz: {minAngleChange:0.###}");

// 1. task (c part) --------------------------------------------------------------------------

string[] lines = File.ReadAllLines("szogek.txt");
double[] angles = Array.ConvertAll(lines[0].Split(' '), double.Parse);

int day = 1;
int hour = 0;
int minute = 0;

foreach (double targetAngle in angles)
{

    while (true)
    {

        double minuteAngle = minute * 6;

        double hourAngle = (hour % 12) * 30 + minute * 0.5;

        double angle = Math.Abs(hourAngle - minuteAngle);
        angle = Math.Min(angle, 360 - angle);

        if (Math.Abs(angle - targetAngle) < 1e-9)
            break;

        minute++;
        if (minute == 60)
        {
            minute = 0;
            hour++;
            if (hour == 24)
            {
                hour = 0;
                day++;
            }
        }
    }

}

Console.WriteLine($"C resz: {day}|{hour:D2}:{minute:D2}");

// 2. task (a part) --------------------------------------------------------------------------

Console.WriteLine("\n-----------------------------------------------------");

Console.WriteLine("2. feladat: ");