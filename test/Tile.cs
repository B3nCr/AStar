using System;

namespace test
{
    public class Tile
    {
        private readonly char type;

        public Tile(char type, Coordinate location)
        {
            this.type = type;
            Location = location;
        }

        public Coordinate Location { get; }

        public int Cost
        {
            get
            {
                switch (type)
                {
                    case '.':
                        return 1;
                    case 'V':
                        return 5;
                    default:
                        return 1;
                }
            }
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
                Tile p = (Tile)obj;
                return (Location.Equals(p.Location));
            }
        }

        public override int GetHashCode()
        {
            return Location.GetHashCode();
        }

    }
}
