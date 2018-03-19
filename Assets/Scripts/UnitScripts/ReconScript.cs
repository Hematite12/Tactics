using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReconScript : UnitScript {

	[SerializeField]
	public int reconMovement;
	[SerializeField]
	public string reconMovementType;

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
