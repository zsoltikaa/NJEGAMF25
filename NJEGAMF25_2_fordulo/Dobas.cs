class Dobas
{
    public List<int> Dobasok { get; set; }
    public Player Player { get; set; }
    public bool IsFullHouse;
    public bool IsPoker;
    public int Pairs;

    public Dobas(List<int> dobasok, int i)
    {
        Dobasok = dobasok;
        Player = (Player)Enum.GetValues(typeof(Player)).GetValue(i);

        IsFullHouse = dobasok.GroupBy(x => x).Select(g => g.Count()).OrderByDescending(x => x).ToList().SequenceEqual([3, 2]);
        IsPoker = dobasok.GroupBy(x => x).Select(g => g.Count()).OrderByDescending(x => x).ToList().SequenceEqual([4, 1]);
        Pairs = !IsFullHouse && !IsPoker ? dobasok.GroupBy(x => x).Count(g => g.Count() == 2) : 0;
    }

    public override string ToString()
    {
        return String.Join(',', Dobasok);
    }
}