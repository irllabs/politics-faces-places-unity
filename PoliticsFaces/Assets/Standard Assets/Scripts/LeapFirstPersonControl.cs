using UnityEngine;
using System.Collections;
using Leap;

namespace UnityStandardAssets.CrossPlatformInput 
{
	public class LeapFirstPersonControl
	{
		float vertical;
		float mouseX;
		float mouseY;

		public float rightCenter_z = -40; // zero position for hand movement
		public float rightVerMin = 20; // sensitivity threshold for hand movment
		public float mouseYThreshold = 0.1f;
		public float mouseXThreshold = 0.2f;
		public float maxSphereRadius;
		public float switchTime = 1f;

		public string horizontalAxisName = "Horizontal"; // The name given to the horizontal axis for the cross platform input
		public string verticalAxisName = "Vertical"; // The name given to the vertical axis for the cross platform input
		public string mouseXAxisName = "Mouse X"; // The name given to the mouse x axis for the cross platform input
		public string mouseYAxisName = "Mouse Y"; // The name given to the mouse y axis for the cross platform input

		CrossPlatformInputManager.VirtualAxis m_HorizontalVirtualAxis; // Reference to the joystick in the cross platform input
		CrossPlatformInputManager.VirtualAxis m_VerticalVirtualAxis; // Reference to the joystick in the cross platform input
		CrossPlatformInputManager.VirtualAxis m_MouseXVirtualAxis; // Reference to the joystick in the cross platform input
		CrossPlatformInputManager.VirtualAxis m_MouseYVirtualAxis; // Reference to the joystick in the cross platform input

		public int softChosenID = 0;
		public int chosenID = 0;
		private bool grabStart;
		private float initialGrabTime;

		public void Init(float maxRadius)
		{
			// create new axes based on axes to use
			m_HorizontalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxisName);
			CrossPlatformInputManager.RegisterVirtualAxis(m_HorizontalVirtualAxis);
			
			m_VerticalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(verticalAxisName);
			CrossPlatformInputManager.RegisterVirtualAxis(m_VerticalVirtualAxis);
			
			m_MouseXVirtualAxis = new CrossPlatformInputManager.VirtualAxis(mouseXAxisName);
			CrossPlatformInputManager.RegisterVirtualAxis(m_MouseXVirtualAxis);
			
			m_MouseYVirtualAxis = new CrossPlatformInputManager.VirtualAxis(mouseYAxisName);
			CrossPlatformInputManager.RegisterVirtualAxis(m_MouseYVirtualAxis);

			maxSphereRadius = maxRadius;
		}
		
		public void Update(Frame frame, bool selected)
		{

			vertical = 0f;
			mouseX = 0f;
			mouseY = 0f;

			for (int i = 0; i < frame.Hands.Count; i++) {

				Leap.Vector palmPosition = frame.Hands [i].PalmPosition;

				//palm open
				if (frame.Fingers[4].IsExtended &&
				    frame.Hands[i].IsRight) 
				{
					if (Mathf.Abs(frame.Hands[i].Direction.x) > mouseXThreshold)
					{
						mouseX = frame.Hands[i].Direction.x > 0 ? frame.Hands[i].Direction.x - mouseXThreshold
							: frame.Hands[i].Direction.x + mouseXThreshold;
					}
					if (Mathf.Abs(frame.Hands[i].Direction.y) > mouseYThreshold)
					{
						mouseY = frame.Hands[i].Direction.y > 0 ? frame.Hands[i].Direction.y - mouseYThreshold 
							: frame.Hands[i].Direction.y + mouseYThreshold;
					}
					//over sensitivity threshold
					if (-palmPosition.z > rightCenter_z + rightVerMin) 
					{
						vertical = (-palmPosition.z - (rightCenter_z + rightVerMin)) / 100;
					}
					if (-palmPosition.z < rightCenter_z - rightVerMin)
					{
						vertical = (-palmPosition.z - (rightCenter_z - rightVerMin)) / 100;
					}
				}
			}

			DetectFingers (frame, selected);

			m_VerticalVirtualAxis.Update(vertical);
			
			m_MouseXVirtualAxis.Update(mouseX);
			
			m_MouseYVirtualAxis.Update(mouseY);

		}

		void DetectFingers(Frame frame, bool selected) {

			softChosenID = 0;
			chosenID = 0;
			for (int i = 0; i < frame.Hands.Count; i++) {			
				Leap.Hand hand = frame.Hands [i];
				//finger detection
				if (!hand.Fingers [0].IsExtended &&
				    !hand.Fingers [4].IsExtended &&
				    hand.Fingers [1].IsExtended) {
					softChosenID = 1;
					if (hand.Fingers [2].IsExtended) {
						softChosenID = 2;
						if (hand.Fingers [3].IsExtended) {
							softChosenID = 3;
						}
					}
				}
			}

			//selecting statuechosenid
			if (!selected) {
				if (!grabStart && softChosenID != 0) {
					grabStart = true;
					initialGrabTime = Time.time;
				}
				
				if (grabStart && softChosenID != 0 &&
					Time.time > initialGrabTime + switchTime) {
					chosenID = softChosenID;
					grabStart = false;
				}
				
				if (grabStart && softChosenID == 0) {
					grabStart = false;
				}
			}
		}	
	}	
}