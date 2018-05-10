namespace Assets.Scripts.Models
{
    public class Furniture
    {
        // This represents the BASE tile of the object -- but in practice, large objects may actually occupy
        // multile tiles.
        public Tile Tile
        {
            get; protected set;
        }

        // This "objectType" will be queried by the visual system to know what sprite to render for this object
        public string objectType
        {
            get; protected set;
        }

        // This is a multipler. So a value of "2" here, means you move twice as slowly (i.e. at half speed)
        // Tile types and other environmental effects may be combined.
        // For example, a "rough" tile (cost of 2) with a table (cost of 3) that is on fire (cost of 3)
        // would have a total movement cost of (2+3+3 = 8), so you'd move through this tile at 1/8th normal speed.
        // SPECIAL: If movementCost = 0, then this tile is impassible. (e.g. a wall).
        public float MovementCost { get; protected set; }

        // For example, a sofa might be 3x2 (actual graphics only appear to cover the 3x1 area, but the extra row is for leg room.)
        public int Width { get; protected set; }
        public int Height { get; protected set; }

        public bool LinksToNeighbour
        {
            get; protected set;
        }
    }
}