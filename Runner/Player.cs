using Newtonsoft.Json;

namespace CSharpRunner
{
    public class Player : MapObject
    {
        [JsonProperty()]
        internal Cell currentCell;
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
