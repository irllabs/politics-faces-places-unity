using UnityEngine;
using System.Collections;

public class DoorSet : MonoBehaviour {

	public float maximumDistance = 100;

	private DoorScript[] doors;
	private GameObject FPSController;
	private Transform target;
	private float playerDistance; 
	private bool inRange;
	private LeapControl leapControl;
	private bool previouslyRange;

	void Start () {
		FPSController = GameObject.FindWithTag ("Player");
		target = FPSController.transform;
		playerDistance = Vector3.Distance (target.position, this.transform.position);
		leapControl = GameObject.FindGameObjectWithTag("LeapControl").GetComponent<LeapControl>();
		doors = new DoorScript[3];
		for (int i = 0; i < doors.Length; i++) {
			doors[i] = transform.GetChild(i).GetComponent<DoorScript>();
		}
	}
	
	void Update () {
		playerDistance = Vector3.Distance (target.position, this.transform.position);
		inRange = playerDistance < maximumDistance ? true : false;
		if (previouslyRange && !inRange) {
			leapControl.Deselect();
		}
		previouslyRange = inRange;
		for(int i = 0; i < doors.Length; i++)
		{
			doors[i].inRange = inRange;
		}
	}
}