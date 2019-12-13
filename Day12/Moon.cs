using System;
using System.Collections.Generic;
using System.Numerics;

namespace AdventOfCode.Day12
{
    public class Moon
    {
        public Point3D Position { get; set; }
        public Point3D Velocity { get; set; }

        public HashSet<Tuple<Point3D, Point3D>> HistoryOfStates { get; private set; }

        public int TotalEnergy => KineticEnergy * PotentialEnergy;
        public int KineticEnergy
            => Math.Abs(Velocity.X) + Math.Abs(Velocity.Y) + Math.Abs(Velocity.Z);
        public int PotentialEnergy
            => Math.Abs(Position.X) + Math.Abs(Position.Y) + Math.Abs(Position.Z);

        public bool IsRepeatedState
            => HistoryOfStates.Contains(new Tuple<Point3D, Point3D>(new Point3D(Position), new Point3D(Velocity)));
        public Point3D SumOfAllVelocities { get; private set; }
        public Point3D SumOfAllPositions { get; private set; }


        public Moon(int x, int y, int z)
        {
            Position = new Point3D(x, y, z);
            Velocity = new Point3D();
            SumOfAllVelocities = new Point3D();
            SumOfAllPositions = new Point3D();
            HistoryOfStates = new HashSet<Tuple<Point3D, Point3D>>();
        }

        public void Step()
        {
            AddToHistory();
            Position += Velocity;
        }

        private void AddToHistory()
        {
            HistoryOfStates.Add(new Tuple<Point3D, Point3D>(new Point3D(Position), new Point3D(Velocity)));
        }
    }
}