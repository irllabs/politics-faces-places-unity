using UnityEngine;
using System.Collections;
using Leap;

public class DoorScript : MonoBehaviour {
	
	public int doorNumber = 1;
	public float softOpenSpeed = 0.5f;
	public float openSpeed = 2;
	public bool inRange;

	private bool audioPlayed = false;
	private bool selected = false;
	private bool open = false;
	private bool opening;
	private bool closing;

	private GameObject FPSController;
	private Transform target;
	private AudioSource audio;
	private Frame frame;
	private LeapControl leapControl;
	private float o_rotation_y;

	void Start() {
		//read player position as target
		FPSController = GameObject.FindWithTag ("Player");
		target = FPSController.transform;

		//initialize audio
		audio = GetComponent<AudioSource> ();
		leapControl = GameObject.FindGameObjectWithTag("LeapControl").GetComponent<LeapControl>();
		o_rotation_y = transform.eulerAngles.y;

		frame = leapControl.GetFrame();
	}
	
	void DetectSelection() {
		if (inRange) {

			if (leapControl.chosenID == 0 &&
				leapControl.softChosenID == doorNumber &&
				!open) {
				transform.Rotate (new Vector3 (0, softOpenSpeed, 0));
			}

			if (leapControl.chosenID == doorNumber &&
				!open) {
				opening = true;
				closing = false;
				open = true;
			}
			if (opening &&
				transform.eulerAngles.y < o_rotation_y + 90 &&
			 	transform.eulerAngles.y > o_rotation_y) {
				transform.Rotate (new Vector3 (0, openSpeed, 0));
			}
		}

		if (leapControl.chosenID == 0 &&
			leapControl.softChosenID != doorNumber &&
			transform.eulerAngles.y > o_rotation_y) {
			transform.Rotate (new Vector3 (0, -softOpenSpeed, 0));
		}

		if (leapControl.chosenID != doorNumber &&
		    open) {
			closing = true;
			opening = false;
			open = false;
		}
		if (closing &&
		    (transform.eulerAngles.y > o_rotation_y ||
		 	 transform.eulerAngles.y < o_rotation_y - 90)) {
			transform.Rotate(new Vector3(0,-openSpeed,0));
		}
	}

	public void Update() {
		frame = leapControl.GetFrame();
		DetectSelection ();
	}
}