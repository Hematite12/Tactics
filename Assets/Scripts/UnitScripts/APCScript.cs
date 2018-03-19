using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APCScript : UnitScript {

	[SerializeField]
	public int apcMovement;
	[SerializeField]
	public string apcMovementType;

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
}
