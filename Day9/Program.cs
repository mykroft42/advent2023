internal class Program
{
    private static void Main(string[] args)
    {
        List<string> lines = File.ReadAllLines("./data.txt").ToList();

        int sum = 0;
        int sum2 = 0;
        foreach (string line in lines)
        {
            List<int> nums = line.Split(' ').Select(l => int.Parse(l)).ToList();
            List<int> res = Process(nums);
            sum += res.Last();
            res = Process2(nums);
            sum2 += res.First();
        }

        Console.WriteLine(sum);
        Console.WriteLine(sum2);
    }

    private static List<int> Process(List<int> nums)
    {
        if (nums.All(n => n == 0))
        {
            List<int> res = nums.ToList();
            res.Add(0);
            return res;
        }

        List<int> result = new List<int>();

        for (int i = 1; i < nums.Count; i++)
        {
            result.Add(nums[i] - nums[i - 1]);
        }

        List<int> response = Process(result);

        List<int> newNums = nums.ToList();
        newNums.Add(nums.Last() + response.Last());
        return newNums;
    }

    private static List<int> Process2(List<int> nums)
    {
        if (nums.All(n => n == 0))
        {
            List<int> res = nums.ToList();
            res.Insert(0, 0);
            return res;
        }

        List<int> result = new List<int>();

        for (int i = 1; i < nums.Count; i++)
        {
            result.Add(nums[i] - nums[i - 1]);
        }

        List<int> response = Process2(result);

        List<int> newNums = nums.ToList();
        newNums.Insert(0, nums.First() - response.First());
        return newNums;
    }
}