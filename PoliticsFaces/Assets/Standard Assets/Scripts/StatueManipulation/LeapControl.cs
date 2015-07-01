using UnityEngine;
using System.Collections;
using Leap;

	public class LeapControl : MonoBehaviour 
	{
		public float maxSphereRadius = 35f;
		public int chosenID;
		public int softChosenID;
		public bool selected;

		private Controller controller;
		private Frame frame;
		private UnityStandardAssets.CrossPlatformInput.LeapFirstPersonControl leapFirstPersonControl;
		private UnityStandardAssets.Characters.FirstPerson.FirstPersonController firstPersonController;

		// Use this for initialization
		void Start () 
		{
			controller = new Controller ();
			leapFirstPersonControl = new UnityStandardAssets.CrossPlatformInput.LeapFirstPersonControl ();
			leapFirstPersonControl.Init(maxSphereRadius);
			firstPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
			frame = controller.Frame ();
		}
		
		// Update is called once per frame
		void Update () 
		{
			frame = controller.Frame ();

			//check that there are hands are on right side
			for (int i = 0; i < frame.Hands.Count; i++) 
			{
				if (Mathf.Abs(frame.Hands[i].Direction.y) <= leapFirstPersonControl.mouseYThreshold)
				{
					firstPersonController.ResetY();
				}
			}
	
			leapFirstPersonControl.Update (frame, selected);
			if (leapFirstPersonControl.chosenID != 0) {
				Select (leapFirstPersonControl.chosenID);
			}
			softChosenID = leapFirstPersonControl.softChosenID;
		print (selected);
		}

	public void Select(int chosen) {
		chosenID = chosen;
		selected = true;
	}

	public void Deselect() {
		chosenID = 0;
		selected = false;
	}

		public Frame GetFrame()
		{
			return frame;
		}
	}	