using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Configuration;
using UnityEngine.Tilemaps;
using System.CodeDom;

public abstract class UnitScript : MonoBehaviour {
	[SerializeField]
	public Vector2Int position;

	public bool selected = false;

	public abstract int movement {
		get;
	}

	public abstract string movementType {
		get;
	}

	void toggleSelect(){
		if (selected){
			selected = false;
			BoardManager.removePossibleMovements (this);
		}
		else if (BoardManager.showPossibleMovements (this)) {
			selected = true;
		}
	}

	void OnMouseDown(){
		toggleSelect ();
	}
}
