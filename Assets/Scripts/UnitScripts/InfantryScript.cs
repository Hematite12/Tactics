using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfantryScript : UnitScript {
	public int infantryMovement;
	public string infantryMovementType;
	public int infantryAttackRange;

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

	public override int attackRange {
		get {
			return infantryAttackRange;
		}
	}
}
