using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierServiceLibrary
{
    public class Coords
    {
        public double X { get; }
        public double Y { get; }
        public Coords(int x, int y)
        {
            X = x; 
            Y = y; 
        }
        public double DistanceTo(Coords endCoords)
        {
            return Math.Abs(endCoords.X - X) + Math.Abs(endCoords.Y - Y);
        }
        public override string ToString()
        {
            return $"X: {X}, Y = {Y}";
        }
        public override bool Equals(object obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Coords c = (Coords)obj;
                return (X == c.X) && (Y == c.Y);
            }
        }
        public override int GetHashCode()
        {
            return X.GetHashCode() * Y.GetHashCode();
        }
    }
}
