using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APCScript : UnitScript {
	public int apcMovement;
	public string apcMovementType;
	public int apcAttackRange;

	public override int movement {
		get {
			return apcMovement;
		}
	}

	public override string movementType {
		get {
			return apcMovementType;
		}
	}

	public override int attackRange {
		get {
			return apcAttackRange;
		}
	}
}
