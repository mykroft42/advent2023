using System.Diagnostics;

internal class Program
{
    static Dictionary<string, int> Values = new Dictionary<string, int>
    {
        {"one", 1 }, {"two", 2}, {"three",  3}, {"four", 4}, {"five", 5 }, {"six", 6}, {"seven", 7 }, {"eight", 8 }, {"nine", 9 }
    };

    private static void Main(string[] args)
    {
        List<string> input = File.ReadAllLines("./data.txt").ToList();
        List<int> nums = new List<int>();
        foreach (string line in input)
        {
            Console.WriteLine(line);
            Debug.WriteLine(line);
            if (!string.IsNullOrEmpty(line))
            {
                int? first = null;
                int last = 0;
                string hold = "";
                for (int i = 0; i < line.Length; i++)
                {
                    char c = line[i];
                    if (int.TryParse(c.ToString(), out int res))
                    {
                        if (first == null) first = res;
                        last = res;
                        hold = "";
                    }
                    else
                    {
                        hold += c;
                        string? key = Values.Keys.SingleOrDefault(k => !((i + k.Length - 1 >= line.Length || line[i..(i + k.Length)] != k)));
                        if (!string.IsNullOrEmpty(key))
                        {
                            if (first == null) first = Values[key];
                            last = Values[key];
                            hold = "";
                        }
                    }
                }
                if ((first.Value * 10) + last < 11 || (first.Value * 10) + last > 99) throw new Exception("wat");
                nums.Add((first.Value * 10) + last);
                Console.WriteLine((first.Value *10) + last);
                Debug.WriteLine((first.Value * 10) + last);
            }
        }

        Console.WriteLine(nums.Sum());
    }
}