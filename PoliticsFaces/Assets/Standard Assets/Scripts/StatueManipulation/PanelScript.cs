using UnityEngine;
using System.Collections;
using Leap;

public class PanelScript : MonoBehaviour {
	
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
	private float o_position_y;

	void Start() {
		//read player position as target
		FPSController = GameObject.FindWithTag ("Player");
		target = FPSController.transform;

		//initialize audio
		audio = GetComponent<AudioSource> ();
		leapControl = GameObject.FindGameObjectWithTag("LeapControl").GetComponent<LeapControl>();
		o_position_y = transform.position.y;

		frame = leapControl.GetFrame();
	}
	
	void DetectSelection() {
		if (inRange) {
			if (leapControl.chosenID == 0 &&
				leapControl.softChosenID == doorNumber &&
				!open) {
				transform.Translate (new Vector3 (0, 0, softOpenSpeed));
			}

			if (leapControl.chosenID == doorNumber &&
				!open) {
				opening = true;
				closing = false;
				open = true;
			}
			if (opening &&
				transform.position.y < o_position_y + 90) {
				print (transform.position.y + " " + o_position_y);
				transform.Translate (new Vector3 (0, 0, openSpeed));
			}
		}

		if (leapControl.chosenID == 0 &&
			leapControl.softChosenID != doorNumber &&
			transform.position.y > o_position_y) {
			transform.Translate (new Vector3 (0, 0, -softOpenSpeed));
		}

		if (leapControl.chosenID != doorNumber &&
		    open) {
			closing = true;
			opening = false;
			open = false;
		}
		if (closing &&
		    (transform.position.y > o_position_y ||
		 	 transform.position.y < o_position_y - 90)) {
			transform.Translate (new Vector3(0,0,-openSpeed));
		}
	}

	void DetectSelection1 () {
		transform.Translate (new Vector3 (0, 0, openSpeed));
	}

	public void Update() {
		frame = leapControl.GetFrame();
		DetectSelection ();
	}
}