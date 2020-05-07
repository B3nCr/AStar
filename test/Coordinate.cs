using System;

namespace test
{
    public struct Coordinate
    {
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X{get;}
        public int Y{get;}

        public int DistanceTo(Coordinate to)
        {
            return Math.Abs((to.X -this.X )) + Math.Abs((to.Y - this.Y));
        }
    }
}