Console.ForegroundColor = ConsoleColor.Green;

string input = File.ReadAllText("C:\\Users\\zsolt\\Downloads\\karsor.txt");

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

Console.WriteLine($"A resz: {maxPair}{maxLength}\n");

int count = 0;

for (int i = 0; i < input.Length; i++)
{
    if (input[i] == 'a')
    {
        for (int j = i + 1; j < input.Length; j++)
        {
            if (input[j] == 'b')
            {
                for (int k = j + 1; k < input.Length; k++)
                {
                    if (input[k] == 'c')
                    {
                        count++;
                    }
                }
            }
        }
    }
}

Console.WriteLine($"B resz (nem biztos, meg kell nezni): {count:N0} \n");

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

Console.WriteLine("C resz: ");
Console.WriteLine($"X pozicio: {posX} | Y pozicio: {posY}");
Console.WriteLine($"tavolsag: {Math.Sqrt(Math.Pow(posX, 2) + Math.Pow(posY, 2))}");

string[] szoveg = File.ReadAllLines("C:\\Users\\zsolt\\Downloads\\szoveg.txt");

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

Console.WriteLine("\n2. feladat: ");

Console.WriteLine($"A resz: {legjobb}");