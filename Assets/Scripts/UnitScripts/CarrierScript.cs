using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrierScript : UnitScript {
	public int carrierMovement;
	public string carrierMovementType;
	public int carrierAttackRange;

	public override int movement {
		get {
			return carrierMovement;
		}
	}

	public override string movementType {
		get {
			return carrierMovementType;
		}
	}

	public override int attackRange {
		get {
			return carrierAttackRange;
		}
	}
}
