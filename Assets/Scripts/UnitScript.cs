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

	public abstract int movement {
		get;
	}

	public abstract string movementType {
		get;
	}
}
