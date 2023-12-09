using Open.Numeric.Primes;
using System.Data;

internal class Program
{
    static Dictionary<string, (string left, string right)> map = new Dictionary<string, (string left, string right)>();
    static string instructions;
    private static void Main(string[] args)
    {
        List<string> lines = File.ReadAllLines("./data.txt").ToList();
        instructions = lines[0];

        List<string> starts = new List<string>();
        for (int i = 2; i < lines.Count; i++)
        {
            string[] items = lines[i].Split('=');
            string[] node = items[1].Split(',');
            map.Add(items[0].Trim(), (node[0].Trim().TrimStart('('), node[1].Trim().TrimEnd(')')));
            if (items[0].Trim().EndsWith('A')) starts.Add(items[0].Trim());
        }

        List<ulong> ends = starts.Select(s => GetSteps(s)).ToList();
        Dictionary<ulong, int> primes = new Dictionary<ulong, int>();
        foreach (ulong end in ends)
        {
            List<ulong> factors = Prime.Factors(end).ToList();
            Dictionary<ulong, int> temp = factors.GroupBy(f => f).ToDictionary(f => f.Key, f => f.Count());
            List<ulong> keys = temp.Keys.ToList();
            for (int i =0; i < temp.Keys.Count; i++)
            {
                if (primes.Keys.Contains(keys[i]) && primes[keys[i]] < temp[keys[i]]) primes[keys[i]] = temp[keys[i]];
                else if (!primes.Keys.Contains(keys[i])) primes.Add(keys[i], temp[keys[i]]);
            }
        }
        
        ulong sum = 1;
        foreach (var keyValue in primes)
        {
            sum *= keyValue.Key * (ulong)keyValue.Value;
        }
        Console.WriteLine(sum);
    }

    private static ulong GetSteps(string start)
    {
        string current = start;
        ulong steps = 0;
        while (!current.EndsWith('Z'))
        {
            char inst = instructions[(int)(steps % (ulong)instructions.Length)];

            if (inst == 'L')
                current = map[current].left;
            else 
                current = map[current].right;

            steps++;
        }
        return steps;
    }
}