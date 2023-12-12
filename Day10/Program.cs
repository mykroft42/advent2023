using System.Numerics;

internal class Program
{
    private static void Main(string[] args)
    {
        List<string> lines = File.ReadAllLines("./data.txt").ToList();
        Dictionary<Point, Node> map = new Dictionary<Point, Node>();
        Point start = new Point(0,0);

        for (int y = 0; y < lines.Count; y++)
        {
            string line = lines[y];
            for (int x = 0; x < line.Length; x++)
            {
                char c = line[x];
                Point? l = null;
                Point? r = null;
                switch(c)
                {
                    case '-':
                        if (x > 0 && line[x - 1] != '.') l = new Point(x - 1, y);
                        if (x < line.Length - 1 && line[x + 1] != '.') r = new Point(x + 1, y);
                        break;
                    case '|':
                        if (y > 0 && lines[y - 1][x] != '.') l = new Point(x, y - 1);
                        if (y < lines.Count - 1 && lines[y + 1][x] != '.') r = new Point(x, y + 1);
                        break;
                    //case 'S':
                    case 'F':
                        if (x < line.Length - 1 && line[x + 1] != '.') r = new Point(x + 1, y);
                        if (y < lines.Count - 1 && lines[y + 1][x] != '.') l = new Point(x, y + 1);
                        break;
                    case 'S':
                    case '7':
                        if (x > 0 && line[x - 1] != '.') l = new Point(x - 1, y);
                        if (y < lines.Count - 1 && lines[y + 1][x] != '.') r = new Point(x, y + 1);
                        break;
                    case 'J':
                        if (y > 0 && lines[y - 1][x] != '.') r = new Point(x, y - 1);
                        if (x > 0 && line[x - 1] != '.') l = new Point(x - 1, y);
                        break;
                    case 'L':
                        if (y > 0 && lines[y - 1][x] != '.') l = new Point(x, y - 1);
                        if (x < line.Length - 1 && line[x + 1] != '.') r = new Point(x + 1, y);
                        break;
                }
                map.Add(new Point(x, y), new Node
                {
                    Left = l,
                    Right = r,
                });
                if (c == 'S') 
                    start = new Point(x, y);
            }
        }

        Point move(Point p, Point prev)
        {
            Node n = map[p];
            if (n.Left == prev) return n.Right;
            else return n.Left;
        }

        Point pleft = start;
        Point pright = start;
        Point left = map[start].Left;
        Point right = map[start].Right;
        int steps = 1;
        while (left != right)
        {
            Point nleft = move(left, pleft);
            Point nright = move(right, pright);
            pleft = left;
            left = nleft;
            pright = right;
            right = nright;
            steps++;
        }

        Console.WriteLine(steps);
    }
}

internal class Node
{
    public Point? Left { get; set; }
    public Point? Right { get; set; }
}

internal class Point : IEquatable<Point>
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {  X = x; Y = y; }

    public bool Equals(Point? other)
    {
        if (other is null) return false;
        return other.X == X && other.Y == Y;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Point);
    }

    public static bool operator == (Point? left, Point? right)
    {
        return left?.Equals(right) ?? false;
    }

    public static bool operator != (Point? left, Point? right)
    {
        return !(left == right);
    }

    public override int GetHashCode()
    {
        return X.GetHashCode() ^ Y.GetHashCode();
    }
}