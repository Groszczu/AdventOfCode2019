namespace AdventOfCode.Day12
{
    public class Point3D
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Point3D(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point3D(Point3D other)
        {
            X = other.X;
            Y = other.Y;
            Z = other.Z;
        }

        public Point3D()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

        public static Point3D operator +(Point3D left, Point3D right)
        {
            return new Point3D(left.X+right.X, left.Y+right.Y, left.Z+right.Z);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Point3D);
        }

        public bool Equals(Point3D other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }
        
        public override int GetHashCode()
        {
            return X*Y*Z + X+Y+Z;
        }
    }
}