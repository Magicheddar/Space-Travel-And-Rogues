using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildModeController : MonoBehaviour {

    public BuildMode buildMode = BuildMode.Floor;
    public TileType tileType = TileType.Floor;
    public string buildModeObjectType;

    public void SetMode_BuildFloor()
    {
        buildMode = BuildMode.Floor;
        tileType = TileType.Floor;

        GameObject.FindObjectOfType<InputController>().StartBuildMode();
    }

    public void SetMode_Bulldoze()
    {
        buildMode = BuildMode.Floor;
        tileType = TileType.Empty;
        GameObject.FindObjectOfType<InputController>().StartBuildMode();
    }

    public void SetMode_BuildFurniture(string objectType)
    {
        // Wall is not a Tile!  Wall is an "Furniture" that exists on TOP of a tile.
        buildMode = BuildMode.Furniture;
        buildModeObjectType = objectType;
        GameObject.FindObjectOfType<InputController>().StartBuildMode();
    }

    public void SetMode_Deconstruct()
    {
        buildMode = BuildMode.Deconstruct;
        GameObject.FindObjectOfType<InputController>().StartBuildMode();
    }

    public void SetupPathfindingExample()
    {
        World.Current.SetupPathfindingExample();
    }

    public void DoBuild(Tile tile)
    {
        if (buildMode == BuildMode.Furniture)
        {
            Debug.Log("BuildModeController :: DoBuild -- build mode furniture not yet implemented.");
            return;
            
            //World.Current.PlaceFurniture
        }
        else if (buildMode == BuildMode.Floor)
        {
            // We are in tile-changing mode.
            tile.Type = tileType;
        }
        else if (buildMode == BuildMode.Deconstruct)
        {
            if (tile.Furniture != null)
            {
                // TODO:
                Debug.Log("BuildModeController :: DoBuild -- Furniture deleted. Should probably do more tehn just poof it into nonexistance.");
                tile.Furniture = null;
            }

        }
        else
        {
            Debug.LogError("UNIMPLMENTED BUILD MODE");
        }

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
