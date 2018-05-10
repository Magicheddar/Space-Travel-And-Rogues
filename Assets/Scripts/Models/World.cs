using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class World
    {
        Tile[,] tiles;
        public List<Character> characters;
        public List<Furniture> furnitures;

        Dictionary<string, Furniture> FurniturePrototypes;

        // The tile width of the world.
        public int Width { get; protected set; }

        // The tile height of the world
        public int Height { get; protected set; }

        static public World Current { get; protected set; }

        Action<Furniture> cbFurnitureCreated;
        Action<Character> cbCharacterCreated;
        Action<Tile> cbTileChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="World"/> class.
        /// </summary>
        /// <param name="width">Width in tiles.</param>
        /// <param name="height">Height in tiles.</param>
        public World(int width = 100, int height = 100)
        {
            // Creates an empty world.
            SetupWorld(width, height);

            // Make one character
            CreateCharacter(GetTileAt(Width / 2, Height / 2));
        }

        public void SetupPathfindingExample()
        {
            Debug.Log("SetupPathfindingExample");

            // Make a set of floors/walls to test pathfinding with.

            int l = Width / 2 - 5;
            int b = Height / 2 - 5;

            for (int x = l - 5; x < l + 15; x++)
            {
                for (int y = b - 5; y < b + 15; y++)
                {
                    tiles[x, y].Type = TileType.Floor;


                    if (x == l || x == (l + 9) || y == b || y == (b + 9))
                    {
                        if (x != (l + 9) && y != (b + 4))
                        {
                            PlaceFurniture("furn_SteelWall", tiles[x, y]);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the tile data at x and y.
        /// </summary>
        /// <returns>The <see cref="Tile"/>.</returns>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public Tile GetTileAt(int x, int y)
        {
            if (x >= Width || x < 0 || y >= Height || y < 0)
            {
                //Debug.LogError("Tile ("+x+","+y+") is out of range.");
                return null;
            }
            return tiles[x, y];
        }

        public void Update(float deltaTime)
        {
            //foreach (Character c in characters)
            //{
            //    c.Update(deltaTime);
            //}

            //foreach (Furniture f in furnitures)
            //{
            //    f.Update(deltaTime);
            //}
        }

        public Furniture PlaceFurniture(string objectType, Tile t)
        {
            //Debug.Log("PlaceInstalledObject");
            // TODO: This function assumes 1x1 tiles -- change this later!

            if (FurniturePrototypes.ContainsKey(objectType) == false)
            {
                Debug.LogError("FurniturePrototypes doesn't contain a proto for key: " + objectType);
                return null;
            }

            Furniture furn = null;//Furniture.PlaceInstance(FurniturePrototypes[objectType], t);

            if (furn == null)
            {
                // Failed to place object -- most likely there was already something there.
                return null;
            }

            furnitures.Add(furn);

            if (cbFurnitureCreated != null)
            {
                cbFurnitureCreated(furn);
            }

            return furn;
        }

        public Character CreateCharacter(Tile tile)
        {
            Debug.Log("CreateCharacter");
            Character c = new Character(tile);

            characters.Add(c);

            if (cbCharacterCreated != null)
                cbCharacterCreated(c);

            return c;
        }

        void SetupWorld(int width, int height)
        {
            // Set the current world to be this world.
            // TODO: Do we need to do any cleanup of the old world?
            Current = this;

            Width = width;
            Height = height;

            tiles = new Tile[Width, Height];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    tiles[x, y] = new Tile(x, y);
                    //tiles[x, y].RegisterTileTypeChangedCallback(OnTileChanged);
                }
            }

            Debug.Log("World created with " + (Width * Height) + " tiles.");

            //CreateFurniturePrototypes();

            characters = new List<Character>();
            furnitures = new List<Furniture>();
        }
    }
}
