namespace CSharpRunner
{
    public class Player : MapObject
    {
        private Cell currentCell;
        public Cell CurrentCell
        {
            get
            {
                return currentCell;
            }
            internal set
            {
                currentCell = value;
                currentCell.Visited++;
            }
        }
        public Player(Cell start)
        {
            CurrentCell = start;
        }

        public override Location GetLocation() => CurrentCell.GetLocation();

    }
}
