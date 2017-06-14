using static System.Math;

namespace CSharpRunner
{
    public abstract class MapObject
    {
        public abstract Location GetLocation();
        public int Distance(MapObject other)
        {
            var loc = GetLocation();
            var loc2 = other.GetLocation();
            return Abs(loc.X - loc2.X) + Abs(loc.Y - loc2.Y);
        }
    }
}
