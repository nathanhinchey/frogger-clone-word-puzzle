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
