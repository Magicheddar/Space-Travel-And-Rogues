using System;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class Tile
    {
        private TileType _type = TileType.Empty;
        public TileType Type
        {
            get { return _type; }
            set
            {
                TileType oldType = _type;
                _type = value;
                // Call the callback and let things know we've changed.

                if (cbTileChanged != null && oldType != _type)
                {
                    cbTileChanged(this);
                }
            }
        }

        // Furniture is something like a wall, door, or sofa.
        public Furniture Furniture
        {
            get; set;
        }

        public int X { get; protected set; }
        public int Y { get; protected set; }

        const float baseTileMovementCost = 1;

        public float MovementCost
        {
            get
            {

                if (Type == TileType.Empty)
                    return 0;   // 0 is unwalkable

                if (Furniture == null)
                    return baseTileMovementCost;

                return baseTileMovementCost * Furniture.MovementCost;
            }
        }

        // The function we callback any time our tile's data changes
        Action<Tile> cbTileChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tile"/> class.
        /// </summary>
        /// <param name="world">A World instance.</param>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public Tile(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }


        /// <summary>
        /// Register a function to be called back when our tile type changes.
        /// </summary>
        public void RegisterTileTypeChangedCallback(Action<Tile> callback)
        {
            cbTileChanged += callback;
        }

        /// <summary>
        /// Unregister a callback.
        /// </summary>
        public void UnregisterTileTypeChangedCallback(Action<Tile> callback)
        {
            cbTileChanged -= callback;
        }

        // Tells us if two tiles are adjacent.
        public bool IsNeighbour(Tile tile, bool diagOkay = false)
        {
            // Check to see if we have a difference of exactly ONE between the two
            // tile coordinates.  Is so, then we are vertical or horizontal neighbours.
            return
                Mathf.Abs(this.X - tile.X) + Mathf.Abs(this.Y - tile.Y) == 1 ||  // Check hori/vert adjacency
                (diagOkay && (Mathf.Abs(this.X - tile.X) == 1 && Mathf.Abs(this.Y - tile.Y) == 1)) // Check diag adjacency
                ;
        }

        public bool PlaceFurniture(Furniture furn)
        {
            if (furn == null)
            {
                // We are uninstalling whatever was here before.
                Furniture = null;
                return true;
            }

            // Obj instance isn't null
            if (Furniture != null)
            {
                Debug.LogError("Tile :: PlaceFurniture -- Trying to assign an installed object to a tile that already has one.");
                return false;
            }

            // At this point, everything's fine!
            Furniture = furn;
            return true;
        }

        /// <summary>
        /// Gets the neighbours.
        /// </summary>
        /// <returns>The neighbours.</returns>
        /// <param name="diagOkay">Is diagonal movement okay?.</param>
        public Tile[] GetNeighbours(bool diagOkay = false)
        {
            Tile[] ns;

            if (diagOkay == false)
            {
                ns = new Tile[4];   // Tile order: N E S W
            }
            else
            {
                ns = new Tile[8];   // Tile order : N E S W NE SE SW NW
            }

            Tile n;

            n = World.Current.GetTileAt(X, Y + 1);
            ns[0] = n;  // Could be null, but that's okay.
            n = World.Current.GetTileAt(X + 1, Y);
            ns[1] = n;  // Could be null, but that's okay.
            n = World.Current.GetTileAt(X, Y - 1);
            ns[2] = n;  // Could be null, but that's okay.
            n = World.Current.GetTileAt(X - 1, Y);
            ns[3] = n;  // Could be null, but that's okay.

            if (diagOkay == true)
            {
                n = World.Current.GetTileAt(X + 1, Y + 1);
                ns[4] = n;  // Could be null, but that's okay.
                n = World.Current.GetTileAt(X + 1, Y - 1);
                ns[5] = n;  // Could be null, but that's okay.
                n = World.Current.GetTileAt(X - 1, Y - 1);
                ns[6] = n;  // Could be null, but that's okay.
                n = World.Current.GetTileAt(X - 1, Y + 1);
                ns[7] = n;  // Could be null, but that's okay.
            }

            return ns;
        }
    }
}