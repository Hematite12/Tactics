using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfantryScript : UnitScript {

	[SerializeField]
	public int infantryMovement;
	[SerializeField]
	public string infantryMovementType;

	public override int movement {
		get {
			return infantryMovement;
		}
	}

	public override string movementType {
		get {
			return infantryMovementType;
		}
	}
}
