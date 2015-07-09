using UnityEngine;
using System.Collections;

public class PanelSet : MonoBehaviour {

	public float maximumDistance = 100000;

	private PanelScript[] panels;
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
		panels = new PanelScript[3];
		for (int i = 0; i < panels.Length; i++) {
			panels[i] = transform.GetChild(i).GetComponent<PanelScript>();
		}
	}
	
	void Update () {
		playerDistance = Vector3.Distance (target.position, this.transform.position);
		inRange = playerDistance < maximumDistance ? true : false;
		if (previouslyRange && !inRange) {
			leapControl.Deselect();
		}
		previouslyRange = inRange;
		for(int i = 0; i < panels.Length; i++)
		{
			panels[i].inRange = inRange;
		}
	}
}