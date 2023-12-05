internal class Program
{
    private static void Main(string[] args)
    {
        List<string> lines = File.ReadAllLines("./data.txt").ToList();
        List<int> counts = new List<int>(Enumerable.Repeat(1, lines.Count));
        int sum = 0;
        int card = 0;
        foreach (string line in lines)
        {
            string item = line.Substring(line.IndexOf(":") + 1);
            string[] lists = item.Split(" | ");
            List<int> need = lists[0].Split(" ").Where(l => !string.IsNullOrEmpty(l)).Select(l => int.Parse(l)).ToList();
            List<int> have = lists[1].Split(" ").Where(l => !string.IsNullOrEmpty(l)).Select(l => int.Parse(l)).ToList();

            List<int> matches = need.Intersect(have).ToList();

            for (int i = card + 1; i <= card + matches.Count; i++)
            {
                counts[i] += counts[card];
            }

            sum += counts[card++];
        }

        Console.WriteLine(sum);
    }
}