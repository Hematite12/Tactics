using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberScript : UnitScript {
	public int bomberMovement;
	public string bomberMovementType;
	public int bomberAttackRange;

	public override int movement {
		get {
			return bomberMovement;
		}
	}

	public override string movementType {
		get {
			return bomberMovementType;
		}
	}

	public override int attackRange {
		get {
			return bomberAttackRange;
		}
	}
}
