using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Controllers
{
    class WorldController : MonoBehaviour
    {
        public static WorldController Instance { get; protected set; }

        public World World { get; protected set; }

        static string loadWorldFromFile = null;

        static bool loadWorld = false;

        // Use this for initialization
        void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There should never be two world controllers.");
            }
            Instance = this;

            if (loadWorld)
            {
                loadWorld = false;
                CreateWorldFromSaveFile();
            }
            else
            {
                CreateEmptyWorld();
            }
        }

        void Update()
        {
            // TODO: Add pause/unpause, speed controls, etc...
            World.Update(Time.deltaTime);
        }

        /// <summary>
        /// Gets the tile at the unity-space coordinates
        /// </summary>
        /// <returns>The tile at world coordinate.</returns>
        /// <param name="coord">Unity World-Space coordinates.</param>
        public Tile GetTileAtWorldCoord(Vector3 coord)
        {
            int x = Mathf.RoundToInt(coord.x);
            int y = Mathf.RoundToInt(coord.y);

            return World.GetTileAt(x, y);
        }

        public void NewWorld()
        {
            Debug.Log("WorldController :: NewWorld -- ");

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public string FileSaveBasePath() { throw new NotImplementedException(); }

        public void SaveWorld()
        {

        }

        public void LoadWorld()
        {
            Debug.Log("WorldController :: LoadWorld -- ");

            // Reload the scene to reset all data and purge old references.
            loadWorld = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        void CreateEmptyWorld()
        {
            // Create a world with Empty tiles
            World = new World();

            // Center the camera
            Camera.main.transform.position = new Vector3(World.Width / 2, World.Width / 2, Camera.main.transform.position.z);
        }

        void CreateWorldFromSaveFile()
        {

        }
    }
}
