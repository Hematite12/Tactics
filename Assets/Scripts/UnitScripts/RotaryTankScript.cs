using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaryTankScript : UnitScript {
	public int rotaryMovement;
	public string rotaryMovementType;
	public int rotaryAttackRange;

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

	public override int attackRange {
		get {
			return rotaryAttackRange;
		}
	}
}
