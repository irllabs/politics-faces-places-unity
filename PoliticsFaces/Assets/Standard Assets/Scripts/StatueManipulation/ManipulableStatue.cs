using UnityEngine;
using System.Collections;
using Leap;

public class ManipulableStatue : MonoBehaviour {
	
	public float maxDistance = 40;
	public float leftCenter_x = -100;
	public float malleability = 0.5f;
	public bool recording = false;

	public float bendAmount;
	public float bendAngle;
	public float eyeLevel;
	public float twistAmount;
	public float height = 1;

	private float oldBendAmount;
	private float oldEyeLevel;
	private float oldTwistAmount;
	private float oldHeight = 1;

	private Material[] materials;
	private Transform childStatue;
	private Frame frame;
	private LeapControl leapControl;

	void Start() {

		//initialize reference to materials 
		childStatue = this.gameObject.transform.GetChild (0);
		materials = childStatue.GetComponent<Renderer> ().materials;

		leapControl = GameObject.FindGameObjectWithTag("LeapControl").GetComponent<LeapControl>();
		
		frame = leapControl.GetFrame();
		Manipulate ();

		oldBendAmount = bendAmount;
		oldTwistAmount = twistAmount;
		oldEyeLevel = eyeLevel;
		oldHeight = height;
		
		//apply manipulation
		for (int i = 0; i < materials.Length; i++)
		{
			materials[i].SetFloat("_BendAmount", bendAmount);
			materials[i].SetFloat("_BendAngle", bendAngle);
			materials[i].SetFloat("_EyeLevel", eyeLevel);
			materials[i].SetFloat("_TwistAmount", twistAmount);
			materials[i].SetFloat("_Height", height);			
		}
	}

	void Manipulate() {
		frame = leapControl.GetFrame();
		recording = false;
		
		for (int i = 0; i < frame.Hands.Count; i++) {
			Leap.Vector palmPosition = frame.Hands [i].PalmPosition;
			
			if (frame.Hands [i].IsLeft)
			{
				recording = true;
				float dx = palmPosition.x;
				float dz = palmPosition.z;
				bendAngle = Mathf.Atan2 (dx - leftCenter_x, dz) * 180f / Mathf.PI + 180f;
				bendAmount = new Vector2(dx - leftCenter_x, dz).magnitude * 0.0003f;
				twistAmount = frame.Hands[i].Direction.x * 10;
				eyeLevel = palmPosition.y * 0.01f;
				height = palmPosition.y * 0.01f - 1;
			}
		}
	}
	
	public void Update() {

		Manipulate ();

		//interpolate manipulation
		bendAmount = Mathf.Lerp (oldBendAmount, bendAmount, malleability);
		twistAmount = Mathf.Lerp (oldTwistAmount, twistAmount, malleability);
		eyeLevel = Mathf.Lerp (oldEyeLevel, eyeLevel, malleability);
		height = Mathf.Lerp (oldHeight, height, malleability);

		oldBendAmount = bendAmount;
		oldTwistAmount = twistAmount;
		oldEyeLevel = eyeLevel;
		oldHeight = height;

		//apply manipulation
		for (int i = 0; i < materials.Length; i++)
		{
			materials[i].SetFloat("_BendAmount", bendAmount);
			materials[i].SetFloat("_BendAngle", bendAngle);
			materials[i].SetFloat("_EyeLevel", eyeLevel);
			materials[i].SetFloat("_TwistAmount", twistAmount);
			materials[i].SetFloat("_Height", height);			
		}
		
	}
}
