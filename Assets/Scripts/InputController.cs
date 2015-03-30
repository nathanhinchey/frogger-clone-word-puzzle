using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

	private GameObject activeFrog;
	private FrogController activeFrogController;

	public void SetActiveFrog (GameObject newFrog) {
		activeFrog = newFrog;
		activeFrogController = activeFrog.GetComponent<FrogController>();

	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if (!activeFrogController.isMoving){
			if (Input.GetKey (KeyCode.UpArrow)) {
				activeFrogController.Move("up");

			} else if (Input.GetKey (KeyCode.DownArrow)) {
				activeFrogController.Move("down");

			} else if (Input.GetKey (KeyCode.LeftArrow)) {
				activeFrogController.Move("left");

			} else if (Input.GetKey (KeyCode.RightArrow)) {
				activeFrogController.Move("right");
			}
		}
	}
}
