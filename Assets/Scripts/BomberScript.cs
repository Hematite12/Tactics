using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberScript : UnitScript {

	[SerializeField]
	public int bomberMovement;
	[SerializeField]
	public string bomberMovementType;

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
}
