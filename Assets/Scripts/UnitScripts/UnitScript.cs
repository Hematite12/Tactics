using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Configuration;
using UnityEngine.Tilemaps;
using System.CodeDom;

public abstract class UnitScript : MonoBehaviour {
	public Vector2Int position;
	public int health;
	public GameObject[] healthValues;
	public GameObject healthDisplay;
	public Sprite normalSprite;
	public Sprite redSprite;
	public bool hasAnimator;

	public abstract int attackRange {
		get;
	}

	public abstract int movement {
		get;
	}

	public abstract string movementType {
		get;
	}

	public SpriteRenderer getSpriteRenderer(){
		return (gameObject.GetComponent (typeof(SpriteRenderer)) as SpriteRenderer);
	}

	public Animator getAnimator(){
		return (gameObject.GetComponent (typeof(Animator)) as Animator);
	}

	public void setHealth(int h){
		health = h;
		updateHealthDisplay ();
	}

	public void move(int destX, int destY){
		position = new Vector2Int (destX, destY);
		Vector3 worldPos = BoardManager.gridToWorld (new Vector2Int (destX, destY));
		gameObject.transform.position = worldPos;
		if (healthDisplay != null){
			healthDisplay.transform.position = worldPos;
		}
	}

	public void updateHealthDisplay(){
		Destroy (healthDisplay);
		healthDisplay = Instantiate (healthValues [(int)(health - 1)], BoardManager.gridToWorld (position), Quaternion.identity).gameObject as GameObject;
	}

	public void changeRed(){
		getSpriteRenderer ().sprite = redSprite;
		if (hasAnimator){
			getAnimator ().SetBool (getAnimator ().parameters[0].name, true);
		}
	}

	public void changeNormal(){
		getSpriteRenderer ().sprite = normalSprite;
		if (hasAnimator){
			getAnimator ().SetBool ("isRed", false);
		}
	}

	public bool damage(int d){
		health -= d;
		if (health <= 0){
			return true;
		}
		else {
			updateHealthDisplay ();
			return false;
		}
	}

	void OnDestroy(){
		Destroy (healthDisplay);
	}

	void Start(){
		health = 10;
		updateHealthDisplay ();
	}
}
