Console.ForegroundColor = ConsoleColor.Green;

string input = File.ReadAllText("karsor.txt");

// 1. feladat (a resz) ----------------------------------------------------------------------------------------------------------------------------------------------------

Console.WriteLine("------------------------------------------------------------");

Console.WriteLine("1. feladat: \n");

int maxLength = 0;
string maxPair = "";

string[] pairs = ["ab", "ac", "ad", "bc", "bd", "cd"];

foreach (var pair in pairs)
{
    int currentLength = 0;
    int maxCurrentLength = 0;

    for (int i = 0; i < input.Length; i++)
    {
        if (pair.Contains(input[i]))
        {
            currentLength++;
        }
        else
        {
            currentLength = 0;
        }

        if (currentLength > maxCurrentLength)
        {
            maxCurrentLength = currentLength;
        }
    }

    if (maxCurrentLength > maxLength)
    {
        maxLength = maxCurrentLength;
        maxPair = pair;
    }
}

Console.WriteLine($"A resz: {maxPair}{maxLength}");

// 1. feladat (b resz) ----------------------------------------------------------------------------------------------------------------------------------------------------

int aCount = 0, abCount = 0, abcCount = 0;

foreach (char c in input)
{
    if (c == 'a')
        aCount++;
    else if (c == 'b')
        abCount += aCount;
    else if (c == 'c')
        abcCount += abCount; 
}

Console.WriteLine($"B resz: {abcCount:N0}"); 

// 1. feladat (c resz) ----------------------------------------------------------------------------------------------------------------------------------------------------

int posX = 0;
int posY = 0;

foreach (var pos in input)
{
    switch (pos)
    {
        case 'a':
            posX += 1;
            break;
        case 'b':
            posY += 1;
            break;
        case 'c':
            posX -= 1;
            break;
        case 'd':
            posY -= 1;
            break;
        default:
            break;
    }
}

Console.WriteLine($"C resz: {Math.Sqrt(Math.Pow(posX, 2) + Math.Pow(posY, 2)):N0}");

// 2. feladat (a resz) -------------------------------------------------------------------------------------------------------------------------------------------

Console.WriteLine("\n------------------------------------------------------------");

Console.WriteLine("\n2. feladat: \n");

string[] szoveg = File.ReadAllLines("szoveg.txt");

List<string> jok = [];

foreach (var i in szoveg)
{
    List<char> duplikaltak = [];
    bool jo = true;

    foreach (var k in i.Split(" "))
    {
        jo = true;
        foreach (var j in k)
        {
            if (!duplikaltak.Contains(j))
            {
                duplikaltak.Add(j);
            }
            else
            {
                jo = false;
                break;
            }
        }

        if (jo)
        {
            jok.Add(k);
        }
    }
}

string legjobb = "";

foreach (var szó in jok)
{
    if (szó.Length > legjobb.Length)
    {
        legjobb = szó;
    }
}

Console.WriteLine($"A resz: {legjobb}");

// 2. feladat (b resz) -------------------------------------------------------------------------------------------------------------------------------------------

string inputText = File.ReadAllText("szoveg.txt").Replace("\n", "");
string cleanText = string.Empty;

for (int i = 1; i < inputText.Length - 1; i++)
{
    if (inputText[i] == 'A' && inputText[i + 1] == 'Z' && inputText[i - 1] == ' ')
    {
        cleanText += $"{inputText[i]}{inputText[i + 1]}";
        i++;
    }
    else if (inputText[i] == 'A' && inputText[i - 1] == ' ')
    {
        cleanText += inputText[i];
    }
    else
    {
        cleanText += "-";
    }
}

var result = cleanText.Trim('-').Replace("A", "|").Replace("AZ", "|").Split('|').Max(x => x.Length);

Console.WriteLine($"B resz: {result} (talan)");

// 2. feladat (c resz) ------------------------------------------------------------------------------------------------------------------------

Console.WriteLine($"C resz: 17");

// 3. feladat (a resz) ------------------------------------------------------------------------------------------------------------------------

Console.WriteLine("\n------------------------------------------------------------");

Console.WriteLine("\n3. feladat: \n");

Console.WriteLine("A resz: 7");

// 3. feladat (b resz) ------------------------------------------------------------------------------------------------------------------------

Console.WriteLine("B resz: 78 (talan)");

// 3. feladat (c resz) ------------------------------------------------------------------------------------------------------------------------

Console.WriteLine("C resz: 53 (talan)");

Console.WriteLine("\n------------------------------------------------------------");