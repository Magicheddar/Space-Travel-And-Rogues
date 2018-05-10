namespace Assets.Scripts.Models
{
    public class Character
    {
        Tile _currentTile;
        public Tile CurrentTile { get; protected set; }

        // If we aren't moving then destination tile is the current tile.
        Tile _destTile;
        Tile DestinationTile
        {
            get
            {
                return _destTile;
            }
            set
            {
                if (_destTile != value)
                {
                    _destTile = value;
                    pathAStar = null;
                }
            }
        }

        Tile nextTile; // The next tile in the pathfinding sequence.
        object pathAStar; //PathAStar

        float speed = 6f; // Tiles per second

        public Character(Tile tile)
        {
            CurrentTile = DestinationTile = nextTile = tile;
        }
    }
}