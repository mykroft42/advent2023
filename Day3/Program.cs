using System.Runtime.CompilerServices;

internal class Program
{
    private static void Main(string[] args)
    {
        //var input = File.ReadAllLines("./test.txt");
        var input = File.ReadAllLines("./data.txt");

        List<(int x1, int x2, int y)> nums = new List<(int x1, int x2, int y)>();
        List<(int x, int y, char c)> specials = new List<(int x, int y, char c)>();
        int y = 0;
        foreach (string line in input)
        {
            int x = 0;
            (int x1, int x2, int y)? coord = null;
            foreach (char c in line)
            {
                if (int.TryParse(c.ToString(), out int cnum))
                {
                    if (coord == null)
                    {
                        coord = (x, x, y);
                    }
                    else
                    {
                        coord = (coord.Value.x1, coord.Value.x2 + 1, coord.Value.y);
                    }
                    //Console.Write(c);
                }
                else
                {
                    if (coord.HasValue)
                    {
                        nums.Add(coord.Value);
                        coord = null;
                    }

                    if (c != '.')
                    {
                        specials.Add((x, y, c));
                    }
                }
                x++;
            }
            if (coord.HasValue)
            {
                nums.Add(coord.Value);
                coord = null;
            }
            y++;
        }

        List<(int x1, int x2, int y)> found = new List<(int x1, int x2, int y)>();
        foreach (var point in specials)
        {
            found.AddRange(nums.Where(n => (point.x >= n.x1 - 1 && point.x <= n.x2 + 1) && Math.Abs(n.y - point.y) <= 1));
        }

        found = found.Distinct().ToList();

        int sum = 0;
        List<int> numsFound = new List<int>();
        foreach (var point in found)
        {
            string num = input[point.y].Substring(point.x1, point.x2 - point.x1 + 1);
            sum += int.Parse(num);
            numsFound.Add(int.Parse(num));
        }

        Console.WriteLine(sum);

        sum = 0;
        foreach (var point in specials.Where(s => s.c == '*'))
        {
            var res = nums.Where(n => (point.x >= n.x1 - 1 && point.x <= n.x2 + 1) && Math.Abs(n.y - point.y) <= 1).ToList();
            if (res.Count == 2)
            {
                string num1 = input[res[0].y].Substring(res[0].x1, res[0].x2 - res[0].x1 + 1);
                string num2 = input[res[1].y].Substring(res[1].x1, res[1].x2 - res[1].x1 + 1);
                sum += int.Parse(num1) * int.Parse(num2);
            }
        }
        Console.WriteLine(sum);
    }
}