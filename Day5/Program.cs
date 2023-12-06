
using System.Data;

internal class Program
{
    private static void Main(string[] args)
    {
        List<string> lines = File.ReadAllLines("./data.txt").ToList();

        List<long> seeds = lines[0].Substring(lines[0].IndexOf(":") + 1).Split(" ").Where(l => !string.IsNullOrEmpty(l)).Select(l => long.Parse(l)).ToList();
        Mapping seedToSoil = new Mapping();
        int i = 3;
        for (; i < lines.Count; i++)
        {
            string line = lines[i];
            if (string.IsNullOrEmpty(line)) break;

            List<long> items = line.Split(" ").Select(l => long.Parse(l)).ToList();
            seedToSoil.AddRange(items[0], items[1], items[2]);
        }

        i += 2;
        Mapping soilToFertilizer = new Mapping();
        for (; i < lines.Count; i++)
        {
            string line = lines[i];
            if (string.IsNullOrEmpty(line)) break;

            List<long> items = line.Split(" ").Select(l => long.Parse(l)).ToList();
            soilToFertilizer.AddRange(items[0], items[1], items[2]);
        }

        i += 2;
        Mapping fertilizerToWater = new Mapping();
        for (; i < lines.Count; i++)
        {
            string line = lines[i];
            if (string.IsNullOrEmpty(line)) break;

            List<long> items = line.Split(" ").Select(l => long.Parse(l)).ToList();
            fertilizerToWater.AddRange(items[0], items[1], items[2]);
        }

        i += 2;
        Mapping waterToLight = new Mapping();
        for (; i < lines.Count; i++)
        {
            string line = lines[i];
            if (string.IsNullOrEmpty(line)) break;

            List<long> items = line.Split(" ").Select(l => long.Parse(l)).ToList();
            waterToLight.AddRange(items[0], items[1], items[2]);
        }

        i += 2;
        Mapping lightToTemp = new Mapping();
        for (; i < lines.Count; i++)
        {
            string line = lines[i];
            if (string.IsNullOrEmpty(line)) break;

            List<long> items = line.Split(" ").Select(l => long.Parse(l)).ToList();
            lightToTemp.AddRange(items[0], items[1], items[2]);
        }

        i += 2;
        Mapping tempToHumidity = new Mapping();
        for (; i < lines.Count; i++)
        {
            string line = lines[i];
            if (string.IsNullOrEmpty(line)) break;

            List<long> items = line.Split(" ").Select(l => long.Parse(l)).ToList();
            tempToHumidity.AddRange(items[0], items[1], items[2]);
        }

        i += 2;
        Mapping humidityToLocation = new Mapping();
        for (; i < lines.Count; i++)
        {
            string line = lines[i];
            if (string.IsNullOrEmpty(line)) break;

            List<long> items = line.Split(" ").Select(l => long.Parse(l)).ToList();
            humidityToLocation.AddRange(items[0], items[1], items[2]);
        }

        Console.WriteLine(seeds
            .Select(s => humidityToLocation.Map(tempToHumidity.Map(lightToTemp.Map(waterToLight.Map(fertilizerToWater.Map(soilToFertilizer.Map(seedToSoil.Map(s)))))))).Min());

        long minVal = long.MaxValue;
        for (int j = 0; j < seeds.Count; j += 2)
        {
            Console.WriteLine($"Starting {j} of {seeds.Count}");
            for (long k = 0; k < seeds[j + 1]; k++)
            {
                long newVal = humidityToLocation.Map(tempToHumidity.Map(lightToTemp.Map(waterToLight.Map(fertilizerToWater.Map(soilToFertilizer.Map(seedToSoil.Map(seeds[j] + k)))))));
                if (newVal < minVal)
                {
                    minVal = newVal;
                }
            }
        }

        Console.WriteLine(minVal);
    }
}

internal class Mapping
{
    private List<Range> sources = new List<Range>();
    private List<Range> dests = new List<Range>();

    public void AddRange(long destStart, long sourceStart, long range)
    {
        dests.Add(new Range(destStart, destStart + range));
        sources.Add(new Range(sourceStart, sourceStart + range));
    }

    public long Map(long source)
    {
        Range? range = sources.Where(s => s.Start <= source && s.End >= source).FirstOrDefault();
        if (range == null) return source;

        int index = sources.IndexOf(range);
        Range dest = dests[index];

        return source + (dest.Start - range.Start);
    }

    private class Range
    {
        public Range(long start, long end)
        {
            Start = start; End = end;
        }

        public long Start { get; set; }
        public long End { get; set; }
    }
}