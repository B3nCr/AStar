using System;

namespace test
{
    public class Coordinate
    {
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public int DistanceTo(Coordinate to)
        {
            return Math.Abs((to.X - this.X)) + Math.Abs((to.Y - this.Y));
        }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Coordinate p = (Coordinate)obj;
                return (X == p.X) && (Y == p.Y);
            }
        }

        public override int GetHashCode()
        {
            return (X << 2) ^ Y;
        }

        public override string ToString() 
        {
            return $"X: {X} Y: {Y}";
        }
    }
}