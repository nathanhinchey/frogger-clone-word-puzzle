using UnityEngine;
using System.Collections;

public class FrogController : MonoBehaviour {

	// Variables to be set in Inspector
	public float travelTimeSeconds = 1.0f;
	public float travelDistance = 1.0f;

			// boundaries
			// TODO: boundaries belong in a different file
	public float xMax = 7f;
	public float xMin = -7f;
	public float yMax = 7f;
	public float yMin = -6f;

	// Private Variables
	private Vector3 activeMovement;
	private Vector3 basicMove;
	private Vector3 target;
	private string moveDimension; // HACK -- I'm using this to hold "x" or "y"
	private Vector3 velocity;
	private float snapToDistance = 0.05f;


	private Transform frog;
	private GameObject gameController;
	private InputController inputController;

	private bool moving = false;



	// Variables used by other scripts
		// input controller will check this
	public bool isMoving {
		get {return moving;}
	}

		//logs and turtles will set this
	public Vector3 restingSpeed {
		get {return basicMove;}
		set {basicMove = value;
		}
	}

	//PUBLIC METHODS

		// input controller will set this
	public void Move(string direction){
		moving = true;
		SetActiveMovement(direction);
		SetFrogRotation(direction);
	}

	//PROTECTED STATIC METHOD
	// these are for frog rotation
	protected static Quaternion Direction(string direction){
		// TODO There has GOT to be a better way to do this.
		if (direction == "up")	  {return Quaternion.Euler (new Vector3 (0,	0,   0));}
		if (direction == "down")  {return	Quaternion.Euler (new Vector3 (0,	0, 180));}
		if (direction == "left")  {return	Quaternion.Euler (new Vector3 (0,	0,  90));}
		if (direction == "right") {return Quaternion.Euler (new Vector3 (0,	0, -90));}
		// TODO: figure out how to throw an error to the console. :P
		return new Quaternion(); //this should never happen, which is why I wanted to throw
	}

	//PRIVATE METHODS
	void SetFrogRotation(string direction) {
		if (direction == "stop")
			return;
		frog.rotation = FrogController.Direction(direction);
	}

				//TODO: there must be a cleaner way to do this
	void SetActiveMovement(string direction){
		moving = true;
		moveDimension = direction;

		switch(direction) {
			case "up":
				activeMovement.y = travelDistance;
				break;
			case "down":
				activeMovement.y = -1f * travelDistance;
				break;
			case "left":
				activeMovement.x = -1f * travelDistance;
				break;
			case "right":
				activeMovement.x = travelDistance;
				break;
		}

		target = activeMovement + basicMove;
	}

// HACK this seems really ungainly
	float GetDimension(Vector3 v3, string direction){
		switch(direction){
			case "up":
				return v3.y;
			case "down":
				return v3.y * -1f;
			case "left":
				return v3.x * -1f;
			case "right":
				return v3.x;
		}
		return 1000f; //shouldn't ever happen
	}

		// Use this for initialization
	void Awake () {
		frog = this.gameObject.transform;

		gameController = GameObject.FindWithTag("GameController");
		inputController = gameController.GetComponent<InputController>();
		//there should only be one FrogController in the game at any time
		inputController.SetActiveFrog(this.gameObject);
		basicMove = Vector3.zero;
	}

		// TODO: this should be in another file with the boundaries
	bool IsInBounds(Vector3 location){
		if (location.x <= xMax &&
		 		location.x >= xMin &&
				location.y <= yMax &&
				location.y >= yMin)
			return true;
		else
			return false;
	}

	void MoveFrog() {
		float targetPart = GetDimension(target, moveDimension);
		float positionPart = GetDimension(frog.position, moveDimension);
		float distanceToTarget = positionPart - targetPart;
		if (moving && distanceToTarget <= snapToDistance) {
			Debug.Log("Arrived.");
			Debug.Log(target);
			Debug.Log(frog.position);
			activeMovement = Vector3.zero;
			moving = false;
		}
		velocity = activeMovement + basicMove;
		frog.position += velocity * Time.deltaTime;
	}

	// Update is called once per frame
	void Update () {
		MoveFrog();
	}
}
