namespace AdventOfCode.Core
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public Point(Point p)
        {
            X = p.X;
            Y = p.Y;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Point);
        }

        private bool Equals(Point other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return X * Y + X + Y;
        }
    }
}