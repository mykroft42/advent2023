internal class Program
{
    private static void Main(string[] args)
    {
        //List<string> lines = File.ReadAllLines("./test.txt").ToList();
        List<string> lines = File.ReadAllLines("./data.txt").ToList();

        List<Round> rounds = new List<Round>();
        foreach (string line in lines)
        {
            string[] parts = line.Split(':');
            int game = int.Parse(parts[0].Substring(5));
            parts = parts[1].Split(";");
            Round r = new Round() {  RoundNum = game };

            foreach (string part in parts)
            {
                Dictionary<string, int> colors = new Dictionary<string, int>();
                string[] marbs = part.Split(",");
                foreach (string marb in marbs)
                {
                    string[] item = marb.Split(" ").Where(i => !string.IsNullOrEmpty(i)).ToArray();
                    colors.Add(item[1], int.Parse(item[0]));
                }
                r.Revealed.Add(colors);
            }
            rounds.Add(r);
            Console.WriteLine(game);
        }
        Console.WriteLine(rounds.Select(r => r.RoundNum).Sum());
        Console.WriteLine(
        rounds.Where(r => r.Revealed.Select(rev => rev.ContainsKey("red") ? rev["red"] : 0).Max() <= 12 && 
                          r.Revealed.Select(rev => rev.ContainsKey("green") ? rev["green"] : 0).Max() <= 13 &&
                          r.Revealed.Select(rev => rev.ContainsKey("blue") ? rev["blue"] : 0).Max() <= 14).Select(r => r.RoundNum).Sum());

        Console.WriteLine(
        rounds.Select(r => r.Revealed.Select(rev => rev.ContainsKey("red") ? rev["red"] : 0).Max() *
                           r.Revealed.Select(rev => rev.ContainsKey("green") ? rev["green"] : 0).Max() *
                           r.Revealed.Select(rev => rev.ContainsKey("blue") ? rev["blue"] : 0).Max()).Sum());
    }
}

internal class Round
{
    public int RoundNum { get; set; }
    public List<Dictionary<string, int>> Revealed { get; set; } = new List<Dictionary<string, int>>();
}