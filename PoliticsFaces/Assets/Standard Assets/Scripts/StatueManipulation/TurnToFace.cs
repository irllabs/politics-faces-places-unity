using UnityEngine;
using System.Collections;

public class TurnToFace : MonoBehaviour {

	public float strength;

	private GameObject FPSController;
	private Transform target;

	// Use this for initialization
	void Start () {

		FPSController = GameObject.FindWithTag ("Player");
		target = FPSController.transform;
		Turn ();
	}

	void Turn() {
		float dx = transform.position.x - target.transform.position.x;
		float dy = transform.position.z - target.transform.position.z;
		float radians = Mathf.Atan2(dx,dy);
		float angle = radians * 180 / Mathf.PI - 90;
		transform.eulerAngles = new Vector3(transform.rotation.x, angle, transform.rotation.z);
	}
	
	void Update () {
		Turn ();
	}
}