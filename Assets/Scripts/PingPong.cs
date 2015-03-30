using UnityEngine;
using System.Collections;

public class PingPong : MonoBehaviour {

	public Vector3 startPosition = new Vector3 (-3,1,0);
	public Vector3 endPosition = new Vector3 (3,1,0);
	public float travelTimeSeconds = 2.0f;

	private Vector3 pointA;
	private Vector3 pointB;
	private Vector3 temp;

	private float arrivalTime;
	private Transform thisThing;
	private float switchTime;

	private GameObject frog;

	public Vector3 speed {
		get {return (pointA - pointB) / ( travelTimeSeconds * Time.deltaTime) ;}
	}

	void OnTriggerEnter2D(Collider2D other){
		frog = other.gameObject;
		if (frog.CompareTag("Player")){
			// NOTE: I haven't tested this before commiting.
			if (!frog.isMoving)
				frog.transform.parent = gameObject.transform;
		}
	}
	void OnTriggerExit2D(Collider2D other){
		frog = other.gameObject;
		if (frog.CompareTag("Player")){
			// this probably needs to be different, too.
			frog.transform.parent = null;
		}
	}

	// Use this for initialization
	void Start () {
		arrivalTime = travelTimeSeconds;
		pointA = startPosition;
		pointB = endPosition;
		thisThing = gameObject.transform;
	}

	// Update is called once per frame
	void Update () {

		thisThing.position = Vector3.Lerp(
			pointB,
			pointA,
			(arrivalTime - Time.time) / travelTimeSeconds
		);

		if (arrivalTime - Time.time <= 0.01f){
			temp = pointA;
			pointA = pointB;
			pointB = temp;
			arrivalTime = Time.time + travelTimeSeconds;
		}
	}
}
