Console.ForegroundColor = ConsoleColor.Green;

// 1. task (a part) ------------------------------------------------------------------

Console.WriteLine("-----------------------------------------------------");

Console.WriteLine("1. feladat: \n");

// 2. task (a part) ------------------------------------------------------------------

Console.WriteLine("-----------------------------------------------------");

Console.WriteLine("2. feladat: \n");

int maxStartNumber = 0;
int maxChainLength = 0;

for (int i = 1; i < 1000000; i++)
{
    int length = GetChainLength(i);
    if (length > maxChainLength)
    {
        maxChainLength = length;
        maxStartNumber = i;
    }
}

Console.WriteLine($"A resz: {maxStartNumber}|{maxChainLength}");

static int GetChainLength(long n)
{
    int count = 1;
    while (n != 1)
    {
        if (n % 2 == 0)
            n /= 2;
        else
            n = 3 * n + 1;
        count++;
    }
    return count;
}

// 3. task (a part) ------------------------------------------------------------------

Console.WriteLine("\n-----------------------------------------------------");

Console.WriteLine("3. feladat: \n");

Dictionary<string, List<string>> szamokRaw = new();

for (int i = 1; i < 10; i++)
{
    for (int j = 1; j < 10; j++)
    {
        for (int k = 1; k < 10; k++)
        {
            for (int l = 1; l < 10; l++)
            {
                string num = $"{i}{j}{k}{l}";
                szamokRaw.Add(num, new());
            }
        }
    }
}

foreach (var item in szamokRaw)
{
    List<string> duplicate = new();
    for (int i = 0; i < 4; i++)
    {
        for (int j = 0; j < 4; j++)
        {
            if (j == i) continue;
            for (int k = 0; k < 4; k++)
            {
                if (k == j || k == i) continue;
                for (int l = 0; l < 4; l++)
                {
                    if (l == k || l == j || l == i) continue;
                    string szam = $"{item.Key[i]}{item.Key[j]}{item.Key[k]}{item.Key[l]}";
                    if (!duplicate.Contains(szam))
                    {
                        szamokRaw[item.Key].Add(szam);
                        duplicate.Add(szam);
                    }
                }
            }
        }
    }
}

static bool IsPrime(long number)
{

    if (number <= 1)
        return false;
    if (number == 2 || number == 3)
        return true;
    if (number % 2 == 0 || number % 3 == 0)
        return false;

    for (long i = 5; i * i <= number; i += 6)
    {
        if (number % i == 0 || number % (i + 2) == 0)
            return false;
    }

    return true;

}

Dictionary<string, List<string>> szamok = [];
List<List<string>> voltmar = [];

foreach (var negyes in szamokRaw)
{
    bool dupl = false;
    List<string> value = negyes.Value.OrderBy(x => x).ToList();
    if (voltmar.Count == 0)
    {
        voltmar.Add(value);
        szamok.Add(negyes.Key, value);
        continue;
    }
    foreach (var vót in voltmar)
    {
        var vótOrd = vót.OrderBy(x => x).ToList();
        if (value.SequenceEqual(vótOrd))
        {
            dupl = true;
            break;
        }
    }

    if (!dupl)
    {
        voltmar.Add(value);
        szamok.Add(negyes.Key, value);
    }
}

Dictionary<string, List<string>> Primes = [];

int counter = 0;
foreach (var item in szamok)
{
    int primeCounter = 0;
    foreach (var num in item.Value)
    {
        if (IsPrime(int.Parse(num)))
        {
            if (!Primes.ContainsKey(item.Key)) Primes.Add(item.Key, new());
            Primes[item.Key].Add(num);
            primeCounter++;
        }
    }
    if (primeCounter > 5) counter++;
}
Console.WriteLine($"A resz: {counter}");

// 3. task (b part) ------------------------------------------------------------------

foreach (var item in Primes)
{

    bool found = false;
    if (item.Value.Count < 3) continue;

    for (int i = 0; i < item.Value.Count - 2; i++)
    {
        int fnum = int.Parse(item.Value[i]);
        int snum = int.Parse(item.Value[i + 1]);
        int tnum = int.Parse(item.Value[i + 2]);
        int dif = snum - fnum;
        if (fnum + dif == snum && snum + dif == tnum)
        {
            Console.WriteLine($"B resz: {fnum} {snum} {tnum}");
            found = true;
            break;
        }
    }

    if (found) break;
}

// 4. task (a part) ------------------------------------------------------------------

Console.WriteLine("\n-----------------------------------------------------");

Console.WriteLine("4. feladat: ");