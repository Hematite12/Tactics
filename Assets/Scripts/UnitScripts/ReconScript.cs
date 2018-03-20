using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReconScript : UnitScript {
	public int reconMovement;
	public string reconMovementType;
	public int reconAttackRange;

	public override int movement {
		get {
			return reconMovement;
		}
	}

	public override string movementType {
		get {
			return reconMovementType;
		}
	}

	public override int attackRange {
		get {
			return reconAttackRange;
		}
	}
}
