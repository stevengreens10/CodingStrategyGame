namespace CSharpRunner
{
    public class Wall
    {
        public bool IsBroken { get; internal set; }
        public Wall(bool broken = false)
        {
            IsBroken = broken;
        }
        public Wall()
        {
            IsBroken = false;
        }
    }
}
