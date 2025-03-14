Console.ForegroundColor = ConsoleColor.Green;

// 1. task (a part) ------------------------------------------------------------------

Console.WriteLine("-----------------------------------------------------");

Console.WriteLine("1. feladat: \n");

char[] boxesIn = File.ReadAllText("dobozok.txt").ToCharArray();

List<Stack<char>> productionLine = [];
List<Stack<char>> doneProducts = [];

char[] boxSizes = ['C', 'B', 'A'];

int max = -1;

foreach (char c in boxesIn)
{

    bool added = false;

    for (int i = 0; i < productionLine.Count; i++)
    {
        if (Array.IndexOf(boxSizes, c) < Array.IndexOf(boxSizes, productionLine[i].Peek()))
        {
            productionLine[i].Push(c);
            added = true;
            if (c == 'C')
            {
                doneProducts.Add(new Stack<char>(productionLine[i].Reverse()));
                productionLine.RemoveAt(i);
            }
            break;
        }
    }

    if (!added)
    {
        productionLine.Add(new());
        productionLine[productionLine.Count - 1].Push(c);
    }

    if (productionLine.Count > max) max = productionLine.Count;

}

/*int charcount = 0;
foreach (var item in doneProducts)
{
    char[] asd = item.ToArray().Reverse().ToArray();
    charcount += asd.Length;
}
foreach (var item in productionLine)
{
    char[] asd = item.ToArray();
    charcount += asd.Length;
}
Console.WriteLine(charcount);
Console.WriteLine(boxesIn.Length);*/

Console.WriteLine($"A resz: {doneProducts.Count}");
Console.WriteLine($"B resz: {max}");

max = -1;

productionLine.Clear();
doneProducts.Clear();

int[] counts = [0, 0]; // elso az A betuk szama, masodik a B betuk szama
//ebben az algoritmusban a hibas A dobozok H betuvel

foreach (char c in boxesIn)
{
    bool added = false;

    if (c == 'A') counts[0]++;
    if (c == 'B') counts[1]++;

    for (int i = 0; i < productionLine.Count; i++)
    {
        if (productionLine[i].Peek() == 'H' && c == 'C')
        {
            productionLine[i].Push(c);
            added = true;
            doneProducts.Add(new Stack<char>(productionLine[i].Reverse()));
            productionLine.RemoveAt(i);
            break;
        }
        if (productionLine[i].Peek() == 'A' && counts[1] == 25 && c == 'B')
        {
            productionLine[i].Push(c);
            added = true;
            doneProducts.Add(new Stack<char>(productionLine[i].Reverse()));
            productionLine.RemoveAt(i);
            break;
        }
        if (Array.IndexOf(boxSizes, c) < Array.IndexOf(boxSizes, productionLine[i].Peek()))
        {
            productionLine[i].Push(c);
            added = true;
            if (c == 'C')
            {
                doneProducts.Add(new Stack<char>(productionLine[i].Reverse()));
                productionLine.RemoveAt(i);
            }
            break;
        }

    }

    if (!added)
    {
        productionLine.Add(new());
        if (counts[0] == 25 && c == 'A')
        {
            productionLine[productionLine.Count - 1].Push('H');
            counts[0] = 0;
        }
        else
        {
            productionLine[productionLine.Count - 1].Push(c);
        }
    }

    if (productionLine.Count > max) max = productionLine.Count;

}

Console.WriteLine($"C resz: {max}\n");

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

for (int i = 1000; i <= 9999; i++)
{
    szamokRaw.Add(i.ToString(), new());
}

//permutációk
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
    // Kezeljük a speciális eseteket
    if (number <= 1)
        return false;
    if (number == 2 || number == 3)
        return true;
    if (number % 2 == 0 || number % 3 == 0)
        return false;

    // Csak a 6k ± 1 alakú számokat ellenõrizzük
    for (long i = 5; i * i <= number; i += 6)
    {
        if (number % i == 0 || number % (i + 2) == 0)
            return false;
    }

    return true;
}

Dictionary<string, List<string>> szamok = new();
List<List<string>> duplicatedPairs = new();
//számnégyes duplikációk szûrése
foreach (var negyes in szamokRaw)
{
    bool dupl = false;
    List<string> value = negyes.Value.OrderBy(x => x).ToList();
    if (duplicatedPairs.Count == 0)
    {
        duplicatedPairs.Add(value);
        szamok.Add(negyes.Key, value);
        continue;
    }
    foreach (var vót in duplicatedPairs)
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
        duplicatedPairs.Add(value);
        szamok.Add(negyes.Key, value);
    }
}


Dictionary<string, List<string>> Primes = new();

//számlálás
int counter = 0;
foreach (var item in szamok)
{
    int primeCounter = 0;

    foreach (var num in item.Value)
    {
        //if (num[0] == '0') Console.WriteLine($"{int.Parse(num)}");
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
            Console.WriteLine($"B resz: {fnum}{snum}{tnum}");
            found = true;
            break;
        }
    }

    if (found) break;

}

// 4. task --------------------------------------------------------------------------

Console.WriteLine("\n-----------------------------------------------------");

Console.WriteLine("4. feladat: \n");

List<Papi> papik = [];

for (int i = 1; i < 300; i++)
{
    papik.Add(new Papi(i));
    if (i == 2) papik.Find(x => x.Id == 2).GetInfected(1);
}

using (StreamReader sr = new("elek.txt"))
{
    while (!sr.EndOfStream)
    {
        string[] line = sr.ReadLine().Split(' ');
        int id = int.Parse(line[0]);
        int contact = int.Parse(line[1]);
        papik.Find(x => x.Id == id).Contacts.Add(contact);
        if (!papik.Find(x => x.Id == contact).Contacts.Contains(id))
            papik.Find(x => x.Id == contact).Contacts.Add(id);
    }
}

int counter2 = 1;
int step = 1;

while (papik.Count(x => x.Infected) != 0)
{

    foreach (Papi p in papik)
    {
        p.InfTick();
    }

    using (StreamReader sr = new("elek.txt"))
    {
        while (!sr.EndOfStream)
        {
            string[] talalkozas = sr.ReadLine().Split(' ');
            int elso = int.Parse(talalkozas[0]);
            int masodik = int.Parse(talalkozas[1]);
            papik[elso - 1].Infect(papik[masodik - 1], step);
        }
    }

    if (counter2 == 5)
    {
        Console.WriteLine($"az 5. lepesben a fertozottek szama: {papik.Count(x => x.Infected)}");
    }
    if (counter2 == 11)
    {
        Console.WriteLine($"a 11. lepesben a fertozottek szama: {papik.Count(x => x.Infected)}");
    }
    if (papik.Count(x => x.Infected) == 0)
    {
        Console.WriteLine($"a {counter2}. lepesben lesz 0 a fertozottek szama ");
    }

    counter2++;
    step++;

}