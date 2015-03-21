using UnityEngine;
using System.Collections;

public class FrogController : MonoBehaviour {

	// Variables to be set in Inspector
	public float travelTimeSeconds = 1.0f;
	public float travelDistance = 1.0f;
	public float xMax = 7f;
	public float xMin = -7f;
	public float yMax = 7f;
	public float yMin = -6f;

	// Private Variables
	private Vector3 basicMove;
	private Transform frog;
	private Vector3 jumpFrom;
	private Vector3 jumpTo;
	private Vector3 activeMovement;
	private GameObject gameController;
	private InputController inputController;
	private Vector3 movement = Vector3.zero;

	private Vector3 jumpValue = Vector3.zero;
	private Vector3 oldPosition;
	private Vector3 referenceFrame = Vector3.zero;

	private bool moving = false;
	private float timeToTarget = 0f;

	private Quaternion up = 		Quaternion.Euler (new Vector3 (0,	0,   0));
	private Quaternion down = 	Quaternion.Euler (new Vector3 (0,	0, 180));
	private Quaternion left = 	Quaternion.Euler (new Vector3 (0,	0,  90));
	private Quaternion right = 	Quaternion.Euler (new Vector3 (0,	0, -90));

	// Variables used by other scripts

	public bool isMoving {
		get {return moving;}
	}
	public Vector3 restingSpeed {
		get {return basicMove;}
		set {
			basicMove = value;
		}
	}

	//PUBLIC METHODS


	public void Move(string direction){
		moving = true;
		switch(direction) {
			case "up":
				frog.rotation = up;
				movement.y = travelDistance;
				break;
			case "down":
				frog.rotation = down;
				movement.y = -1f * travelDistance;
				break;
			case "left":
				frog.rotation = left;
				movement.x = -1f * travelDistance;
				break;
			case "right":
				frog.rotation = right;
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
		basicMove = Vector3.zero;
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
		if (IsInBounds (frog.localPosition + movement)){
			moving = true;
			timeToTarget = Time.time + travelTimeSeconds;
			jumpTo = frog.localPosition + movement;
			jumpFrom = frog.localPosition;
		}
		// Debug.Log("jumpTo = " + jumpTo + "; jumpFrom = " + jumpFrom + "; frog.Position = " + frog.position);
		Debug.Log("Local position: " + frog.localPosition + "; Global position: " + frog.position);
		movement = Vector3.zero;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (moving){
			if ( (jumpValue - jumpTo).magnitude < 0.1f || ((timeToTarget - Time.time) / travelTimeSeconds) < -0.1){
			 	frog.localPosition = jumpTo;
				moving = false;
			}
			else
				frog.localPosition = Vector3.Lerp
						(jumpTo, jumpFrom, (timeToTarget - Time.time) / travelTimeSeconds);
		}
	}
}
