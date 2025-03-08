Console.ForegroundColor = ConsoleColor.Green;

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

Console.WriteLine("\n-----------------------------------------------------");

Console.WriteLine("\n2. feladat: \n");

List<char> dobasokIn = File.ReadAllText("dobasok.txt").ToCharArray().ToList();
List<char> dontesekIn = File.ReadAllText("dontesek.txt").ToCharArray().ToList();

List<Dobas> dobasok = [];
List<int> dobas = [];

int playerNum = 0;

while (dobasokIn.Count != 0)
{

    while (dobas.Count != 5)
    {
        if (dontesekIn[0] == '1') dobas.Add(dobasokIn[0] - '0');
        dontesekIn.RemoveAt(0);
        dobasokIn.RemoveAt(0);
    }

    dobasok.Add(new(dobas.ToList(), playerNum));
    if (playerNum == 2) playerNum = 0;
    else playerNum++;
    dobas.Clear();

}

Console.WriteLine($"a) A játékban {dobasok.Count(x => x.Player == Player.GAMMA)} teljes kör volt.");
Console.WriteLine($"b) A játékot {dobasok[..^1].First().Player} játékos nyerte.");

int roundCount = 1;

foreach (var item in dobasok.Where(x => x.Player == Player.GAMMA))
{
    if (item.IsFullHouse)
        break;
    roundCount++;
}

Console.WriteLine($"c) GAMMA játékos a {roundCount}. körben dobott fullt.");
Console.WriteLine($"d) A legnagyobb póker a [{dobasok.Where(x => x.IsPoker).OrderByDescending(x => x.Dobasok.Distinct().OrderByDescending(x => x).First()).First()}] számokból állt.");
Console.WriteLine($"e) A három játékos összesen {dobasok.Sum(x => x.Pairs)} párt dobott.");

Console.WriteLine("\n-----------------------------------------------------");

Console.WriteLine("\n3. feladat: \n");

string[] cipher = File.ReadAllText("szoveg.txt").Split(['\n', ' ']);
char[] cipherChars = ['A', 'E', 'I', 'O', 'U'];
char[,] cipherTable =
{
    { 'A', 'B', 'C', 'D', 'E' },
    { 'F', 'G', 'H', 'I', 'J' },
    { 'K', 'L', 'M', 'N', 'O' },
    { 'P', 'Q', 'R', 'S', 'T' },
    { 'U', 'V', 'X', 'Y', 'Z' }
};
string decodedText = string.Empty;
bool _switch = false;
foreach (string word in cipher)
{
    for (int i = 0; i < word.Length - 1; i += 2)
    {
        if (!_switch)
        {
            decodedText += cipherTable[Array.IndexOf(cipherChars, word[i]), Array.IndexOf(cipherChars, word[i + 1])];
        }
        else
        {
            decodedText += cipherTable[Array.IndexOf(cipherChars, word[i + 1]), Array.IndexOf(cipherChars, word[i])];
        }
        _switch = !_switch;
    }
    decodedText += ' ';
}

string[] splitDecodedText = decodedText.Split(' ');
string[] reverseSplitDecodedText = decodedText.Split(' ').Reverse().ToArray();

int firstIndex = 0;
int lastIndex = 0;

for (int i = 0; i < splitDecodedText.Length; i++)
{
    if (splitDecodedText[i].Contains('Q'))
    {
        firstIndex = i;
        break;
    }
}

for (int i = 0; i < reverseSplitDecodedText.Length; i++)
{
    if (reverseSplitDecodedText[i].Contains('Q'))
    {
        lastIndex = reverseSplitDecodedText.Length - 1 - i;
        break;
    }
}

Console.WriteLine($"a) Az 1. és az utolsó szó között, amelynek megfejtésében szerepel a Q karakter, {lastIndex - firstIndex - 1} szó szerepel.");

string ClearConsonants(string input)
{
    string output = string.Empty;

    for (int i = 0; i < input.Length; i++)
    {
        if (cipherChars.Contains(input[i])) output += input[i];
    }

    return output;
}

string dirtyCipher2 = File.ReadAllText("szoveg2.txt");
string cipher2 = ClearConsonants(dirtyCipher2);
string decodedText2 = string.Empty;

for (int i = 0; i < cipher2.Length - 1; i += 2)
{
    decodedText2 += cipherTable[Array.IndexOf(cipherChars, cipher2[i]), Array.IndexOf(cipherChars, cipher2[i + 1])];
}

Console.WriteLine($"b) A szoveg2.txt megfejtése: {decodedText2}");

string EncryptCharacter(char c)
{
    for (int i = 0; i < cipherTable.GetLength(0); i++)
    {
        for (int j = 0; j < cipherTable.GetLength(1); j++)
        {
            if (cipherTable[i, j] == c)
            {
                return $"{cipherChars[i]}{cipherChars[j]}";
            }
        }
    }
    return "00";
}

string plainText = File.ReadAllText("szoveg3.txt");
string[] words = File.ReadAllText("szavak.txt").Split(' ');
string cipher3 = string.Empty;

foreach (char c in plainText)
{
    cipher3 += $"{EncryptCharacter(c)} ";
}
cipher3 = cipher3.Trim();

string obfuscatedCipher = string.Empty;

foreach (string vowelPair in cipher3.Split(' '))
{
    for (int i = 0; i < words.Length; i++)
    {
        if (vowelPair == ClearConsonants(words[i]))
        {
            obfuscatedCipher += $"{words[i]} ";
        }
    }
}

Console.WriteLine($"c) A szoveg3.txt fájl tartalmának kódolt állapota: {obfuscatedCipher.Trim()}");