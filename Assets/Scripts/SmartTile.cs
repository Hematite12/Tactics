using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using UnityEditor;

public class SmartTile : Tile {
	[SerializeField]
	public string tileType;
	public bool walkable;
	public bool treadable;
	public bool sailable;
	public bool flyable;
	public bool moveHighlighted;

	public override bool StartUp (Vector3Int position, ITilemap tilemap, GameObject go)
	{
		if (tileType == "Grass"){
			walkable = true;
			treadable = true;
			sailable = false;
			flyable = true;
		}
		else if (tileType == "Water"){
			walkable = false;
			treadable = false;
			sailable = true;
			flyable = true;
		}
		else if (tileType == "Mountain"){
			walkable = true;
			treadable = false;
			sailable = false;
			flyable = true;
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
