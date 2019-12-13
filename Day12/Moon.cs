using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AdventOfCode.Day12
{
    public class Moon
    {
        public Point3D Position { get; set; }
        public Point3D Velocity { get; set; }

        public int TotalEnergy => KineticEnergy * PotentialEnergy;
        public int KineticEnergy
            => Math.Abs(Velocity.X) + Math.Abs(Velocity.Y) + Math.Abs(Velocity.Z);
        public int PotentialEnergy
            => Math.Abs(Position.X) + Math.Abs(Position.Y) + Math.Abs(Position.Z);

        public Moon(int x, int y, int z)
        {
            Position = new Point3D(x, y, z);
            Velocity = new Point3D();
        }

        public Moon(Moon other)
        {
            Position = new Point3D(other.Position);
            Velocity = new Point3D(other.Velocity);
        }

        public void Step()
        {
            Position += Velocity;
        }
    }
}