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

static IEnumerable<int[]> GetCombinationsWithRepetition(int[] digits, int length)
{
    int[] result = new int[length];
    return GenerateCombinations(digits, result, 0, 0);
}

static IEnumerable<int[]> GenerateCombinations(int[] digits, int[] result, int index, int start)
{
    if (index == result.Length)
    {
        yield return (int[])result.Clone();
        yield break;
    }

    for (int i = start; i < digits.Length; i++)
    {
        result[index] = digits[i];
        foreach (var combo in GenerateCombinations(digits, result, index + 1, i))
            yield return combo;
    }
}

static IEnumerable<int> GetUniquePermutations(int[] digits)
{
    return GetPermutations(digits, 0).Select(arr => ConvertToInt(arr)).Distinct();
}

static IEnumerable<int[]> GetPermutations(int[] digits, int index)
{
    if (index == digits.Length - 1)
    {
        yield return (int[])digits.Clone();
    }
    else
    {
        HashSet<int> used = new HashSet<int>();
        for (int i = index; i < digits.Length; i++)
        {
            if (used.Add(digits[i]))
            {
                Swap(digits, index, i);
                foreach (var perm in GetPermutations(digits, index + 1))
                    yield return perm;
                Swap(digits, index, i);
            }
        }
    }
}

static void Swap(int[] arr, int i, int j)
{
    int temp = arr[i];
    arr[i] = arr[j];
    arr[j] = temp;
}

static int ConvertToInt(int[] arr)
{
    int num = 0;
    foreach (var digit in arr)
    {
        num = num * 10 + digit;
    }
    return num;
}

static bool IsPrime(int num)
{
    if (num < 2) return false;
    if (num % 2 == 0 && num != 2) return false;
    for (int i = 3; i * i <= num; i += 2)
    {
        if (num % i == 0) return false;
    }
    return true;
}

int count = 0;
var digits = Enumerable.Range(0, 10).ToArray(); 

foreach (var combination in GetCombinationsWithRepetition(digits, 4))
{
    var uniquePermutations = GetUniquePermutations(combination)
        .Where(n => n >= 1000) 
        .ToHashSet(); 

    int primeCount = uniquePermutations.Count(IsPrime);

    if (primeCount >= 6)
    {
        count++;
    }
}

Console.WriteLine($"A resz: {count}");
