using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrierScript : UnitScript {

	[SerializeField]
	public int carrierMovement;
	[SerializeField]
	public string carrierMovementType;

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
}
