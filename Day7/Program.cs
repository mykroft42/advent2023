using System.Reflection.Metadata.Ecma335;

internal class Program
{
    private static void Main(string[] args)
    {
        List<string> lines = File.ReadAllLines("./data.txt").ToList();

        List<Hand> hands = lines.Select(l =>
        {
            string[] parts = l.Split(" ");
            return new Hand
            {
                Cards = parts[0],
                Wager = int.Parse(parts[1]),
            };
        }).ToList();

        
        Comparison<Hand> comparison = new Comparison<Hand>((left, right) =>
        {
            if (left.GetHandType() > right.GetHandType()) { return 1; }
            else if (left.GetHandType() < right.GetHandType()) { return -1; }
            else
            {
                for (int i = 0; i < left.Cards.Length; i++)
                {
                    int res = ConvertChar(left.Cards[i]).CompareTo(ConvertChar(right.Cards[i]));
                    if (res != 0) return res;
                }
                throw new Exception("WatWat");
            }
            return 0;
        });

        hands.Sort(comparison);

        long sum = 0;
        for (int i = 0; i < hands.Count; i++)
        {
            Hand hand = hands[i];

            sum += (i + 1) * hand.Wager;
            //Console.WriteLine($"Hand: {hand.Cards}, Wager: {hand.Wager}, Rank: {i + 1}");
        }

        Console.WriteLine(sum);

        Comparison<Hand> comparison2 = new Comparison<Hand>((left, right) =>
        {
            if (left.GetHandType2() > right.GetHandType2()) { return 1; }
            else if (left.GetHandType2() < right.GetHandType2()) { return -1; }
            else
            {
                for (int i = 0; i < left.Cards.Length; i++)
                {
                    int res = ConvertChar2(left.Cards[i]).CompareTo(ConvertChar2(right.Cards[i]));
                    if (res != 0) return res;
                }
                throw new Exception("WatWat");
            }
            return 0;
        });

        hands.Sort(comparison2);

        sum = 0;
        for (int i = 0; i < hands.Count; i++)
        {
            Hand hand = hands[i];

            sum += (i + 1) * hand.Wager;
            //Console.WriteLine($"Hand: {hand.Cards}, Wager: {hand.Wager}, Rank: {i + 1}");
        }

        Console.WriteLine(sum);
    }

    static int ConvertChar(char c)
    {
        return c switch
        {
            'A' => 14,
            'K' => 13,
            'Q' => 12,
            'J' => 11,
            'T' => 10,
            _ => int.Parse(c.ToString())
        };
    }

    static int ConvertChar2(char c)
    {
        return c switch
        {
            'A' => 14,
            'K' => 13,
            'Q' => 12,
            'J' => 1,
            'T' => 10,
            _ => int.Parse(c.ToString())
        };
    }
}

internal enum HandType
{
    HighCard,
    Pair,
    TwoPair,
    ThreeOfAKind,
    FullHouse,
    FourOfAKind,
    FiveOfAKind,
}

internal class Hand
{
    public string Cards { get; set; }

    public int Wager { get; set; }

    public HandType GetHandType()
    {
        Dictionary<char, int> groups = Cards.GroupBy(c => c).ToDictionary(c => c.Key, c => c.Count());

        if (groups.Count == 5) return HandType.HighCard;
        else if (groups.Count == 1) return HandType.FiveOfAKind;
        else
        {
            int twos = 0;
            int threes = 0;
            int fours = 0;
            foreach (var group in groups)
            {
                if (group.Value == 2)
                    twos++;
                else if (group.Value == 3)
                    threes++;
                else if (group.Value == 4)
                    fours++;
            }

            if (fours > 0) return HandType.FourOfAKind;
            else if (threes > 0 && twos > 0) return HandType.FullHouse;
            else if (threes > 0) return HandType.ThreeOfAKind;
            else if (twos == 2) return HandType.TwoPair;
            else if (twos == 1) return HandType.Pair;
            else throw new Exception("Wat");
        }
    }

    public HandType GetHandType2()
    {
        Dictionary<char, int> groups = Cards.GroupBy(c => c).ToDictionary(c => c.Key, c => c.Count());
        int js = Cards.Where(c => c == 'J').Count();

        if (groups.Count == 5)
        {
            return js switch
            {
                0 => HandType.HighCard,
                1 => HandType.Pair,
                _ => throw new NotImplementedException()
            };
        }
        else if (groups.Count == 1) return HandType.FiveOfAKind;
        else
        {
            int twos = 0;
            int threes = 0;
            int fours = 0;
            foreach (var group in groups)
            {
                if (group.Value == 2)
                    twos++;
                else if (group.Value == 3)
                    threes++;
                else if (group.Value == 4)
                    fours++;
            }

            if (fours > 0 && js > 0) return HandType.FiveOfAKind;
            else if (fours > 0) return HandType.FourOfAKind;
            else if (threes > 0 && js == 1) return HandType.FourOfAKind;
            else if (threes > 0 && js == 2) return HandType.FiveOfAKind;
            else if (threes > 0 && js == 3 && twos > 0) return HandType.FiveOfAKind;
            else if (threes > 0 && js == 3) return HandType.FourOfAKind;
            else if (threes > 0 && twos > 0) return HandType.FullHouse;
            else if (threes > 0) return HandType.ThreeOfAKind;
            else if (twos == 2 && js == 1) return HandType.FullHouse;
            else if (twos == 2 && js == 2) return HandType.FourOfAKind;
            else if (twos == 2) return HandType.TwoPair;
            else if (twos == 1 && js > 0) return HandType.ThreeOfAKind;
            else if (twos == 1) return HandType.Pair;
            else throw new Exception("Wat");
        }
    }
}