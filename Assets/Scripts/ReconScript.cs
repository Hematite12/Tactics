using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReconScript : UnitScript {

	public int reconMovement = 5;
	public string reconMovementType = "tread";

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
}
