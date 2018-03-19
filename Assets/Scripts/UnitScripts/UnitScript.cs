using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Configuration;
using UnityEngine.Tilemaps;
using System.CodeDom;

public abstract class UnitScript : MonoBehaviour {
	public Vector2Int position;
	public int health;
	public GameObject[] healthValues;
	public GameObject healthDisplay;

	public abstract int movement {
		get;
	}

	public abstract string movementType {
		get;
	}

	public void setHealth(int h){
		health = h;
		updateHealthDisplay ();
	}

	public void move(int destX, int destY){
		position = new Vector2Int (destX, destY);
		Vector3 worldPos = BoardManager.gridToWorld (new Vector2Int (destX, destY));
		gameObject.transform.position = worldPos;
		if (healthDisplay != null){
			healthDisplay.transform.position = worldPos;
		}
	}

	public void updateHealthDisplay(){
		healthDisplay = Instantiate (healthValues [(int)(health - 1)], BoardManager.gridToWorld (position), Quaternion.identity).gameObject as GameObject;
	}

	void Start(){
		health = 10;
		updateHealthDisplay ();
	}
}
