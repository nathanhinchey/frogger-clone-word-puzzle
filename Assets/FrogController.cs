﻿using UnityEngine;
using System.Collections;

public class FrogController : MonoBehaviour {

//	private enum D {UP, DOWN, LEFT, RIGHT, STOP}
//
//	private D moveDirection;

	public float travelTimeSeconds = 1.0f;
	public float travelDistance = 1.0f;
	public float xMax = 7f;
	public float xMin = -7f;
	public float yMax = 7f;
	public float yMin = -6f;

	private Transform frog;
	private Vector3 jumpFrom;
	private Vector3 jumpTo;
	private bool isJumping;

	private GameObject gameController;
	private InputController inputController;

	private Vector3 movement = Vector3.zero;

	private bool moving = false;

	private float timeToTarget = 0f;

	//PUBLIC METHODS


	public void Move(string direction){
		switch(direction) {
			case "up":
				movement.y = travelDistance;
				break;
			case "down":
				movement.y = -1f * travelDistance;
				break;
			case "left":
				movement.x = -1f * travelDistance;
				break;
			case "right":
				movement.x = travelDistance;
				break;
		}

		AfterDirection();
	}

	//PRIVATE METHODS

	// Use this for initialization
	void Awake () {
		frog = this.gameObject.transform;

		gameController = GameObject.FindWithTag("GameController");
		inputController = gameController.GetComponent<InputController>();
		inputController.SetActiveFrog(this.gameObject);

	}

	bool IsInBounds(Vector3 location){
		if (location.x <= xMax &&
		 		location.x >= xMin &&
				location.y <= yMax &&
				location.y >= yMin)
			return true;
		else
			return false;
	}

	void AfterDirection(){
		if (IsInBounds (frog.position + movement)){
			moving = true;
			timeToTarget = Time.time + travelTimeSeconds;
			jumpFrom = frog.position;
			jumpTo = frog.position + movement;
		}
		else {
			movement = Vector3.zero;
		}
	}

	// Update is called once per frame
	void Update () {

		movement.x = 0f;
		movement.y = 0f;

		if (moving && (frog.position - jumpTo).magnitude < 0.1) {
			frog.position = jumpTo;
			moving = false;
		}
		frog.position = Vector3.Lerp(jumpTo,jumpFrom, (timeToTarget - Time.time) / travelTimeSeconds );
	}
}