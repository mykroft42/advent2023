internal class Program
{
    private static void Main(string[] args)
    {
        List<string> lines = File.ReadAllLines("./data.txt").ToList();

        List<int> seconds = lines[0].Substring(lines[0].IndexOf(':') + 1).Split(' ').Where(l => !string.IsNullOrEmpty(l)).Select(l => int.Parse(l)).ToList();
        List<int> distances = lines[1].Substring(lines[1].IndexOf(':') + 1).Split(' ').Where(l => !string.IsNullOrEmpty(l)).Select(l => int.Parse(l)).ToList();

        List<int> recs = new List<int>();
        for (int i = 0; i < seconds.Count; i++)
        {
            int rec = 0;
            for (int j = 0; j <= seconds[i]; j++)
            {
                if (j * (seconds[i] - j) > distances[i]) 
                {
                    rec++;
                }
            }

            recs.Add(rec);
        }

        Console.WriteLine(recs.Aggregate(1, (r, s) => r * s));

        long secs = long.Parse(lines[0].Substring(lines[0].IndexOf(':') + 1).Replace(" ", ""));
        long dist = long.Parse(lines[1].Substring(lines[1].IndexOf(':') + 1).Replace(" ", ""));

        long ways = 0;
        for (int i = 0; i < secs; i++)
        {
            if (i * (secs - i) > dist) ways++;
        }

        Console.WriteLine(ways);
    }
}