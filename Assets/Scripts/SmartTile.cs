using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using UnityEditor;

public class SmartTile : Tile {
	public string tileType;
	public bool moveHighlighted;

	public bool walkable;
	public bool treadable;
	public bool sailable;
	public bool flyable;
	public int walkCost = 0;
	public int treadCost = 0;
	public int sailCost = 0;
	public int flyCost = 0;

	public override bool StartUp (Vector3Int position, ITilemap tilemap, GameObject go)
	{
		if (tileType == "Grass"){
			walkable = true;
			treadable = true;
			sailable = false;
			flyable = true;

			walkCost = 1;
			treadCost = 1;
			flyCost = 1;
		}
		else if (tileType == "Water"){
			walkable = false;
			treadable = false;
			sailable = true;
			flyable = true;

			sailCost = 1;
			flyCost = 1;
		}
		else if (tileType == "Mountain"){
			walkable = true;
			treadable = false;
			sailable = false;
			flyable = true;

			walkCost = 2;
			flyCost = 1;
		}
		return base.StartUp (position, tilemap, go);
	}

	#if UNITY_EDITOR
	[MenuItem("Assets/Create/Tiles/SmartTile")]
	public static void CreateSmartTile(){
		string path = EditorUtility.SaveFilePanelInProject ("Save SmartTile", "New SmartTile", "asset", "Save SmartTile", "Assets");
		if (path==""){
			return;
		}
		AssetDatabase.CreateAsset (ScriptableObject.CreateInstance <SmartTile>(),path);
	}
	#endif
}
