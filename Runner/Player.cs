using Newtonsoft.Json;

namespace CSharpRunner
{
    public class Player : MapObject
    {

        private Cell currentCell;

        [JsonProperty()]
        private Location Location;

        internal Cell CurrentCell
        {
            get
            {
                return currentCell;
            }
            set
            {
                currentCell = value;
                currentCell.Visited++;
            }
        }
        internal Player(Cell start)
        {
            CurrentCell = start;
            Location = currentCell.GetLocation();
        }

        public override Location GetLocation() => CurrentCell.GetLocation();

    }
}
