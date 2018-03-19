using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaryTankScript : UnitScript {

	[SerializeField]
	public int rotaryMovement;
	[SerializeField]
	public string rotaryMovementType;

	public override int movement {
		get {
			return rotaryMovement;
		}
	}

	public override string movementType {
		get {
			return rotaryMovementType;
		}
	}
}
