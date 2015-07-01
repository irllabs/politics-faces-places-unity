using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [Serializable]
    public class MouseLook
    {
        public float XSensitivity = 2f;
        public float YSensitivity = 2f;
        public bool clampVerticalRotation = true;
        public float MinimumX = 120F;
        public float MaximumX = 300F;
        public bool smooth;
        public float smoothTime = 5f;


		private Quaternion m_CameraOriginalPosition;
        private Quaternion m_CharacterTargetRot;
        private Quaternion m_CameraTargetRot;
		private Quaternion m_OldCameraTargetRot;


        public void Init(Transform character, Transform camera)
        {
            m_CharacterTargetRot = character.localRotation;
            m_CameraTargetRot = camera.localRotation;
			m_CameraOriginalPosition = camera.localRotation;
			m_OldCameraTargetRot = camera.localRotation;
		}

		public void ResetY()
		{
			//interpolate angle
			m_OldCameraTargetRot = m_CameraTargetRot;
			m_CameraTargetRot = Quaternion.Lerp (m_OldCameraTargetRot, m_CameraOriginalPosition, 0.2f);
		}


        public void LookRotation(Transform character, Transform camera)
        {
            float yRot = CrossPlatformInputManager.GetAxis("Mouse X") * XSensitivity;
            float xRot = CrossPlatformInputManager.GetAxis("Mouse Y") * YSensitivity;
			m_CameraTargetRot = Quaternion.Lerp (m_OldCameraTargetRot, Quaternion.Euler (-xRot * 10, 0f, 0f), 0.2f);
			m_OldCameraTargetRot = m_CameraTargetRot;

			m_CharacterTargetRot *= Quaternion.Euler (0f, yRot, 0f);

            if(clampVerticalRotation)
                m_CameraTargetRot = ClampRotationAroundXAxis (m_CameraTargetRot);

			character.localRotation = m_CharacterTargetRot;
            camera.localRotation = m_CameraTargetRot;
        }


        Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

            angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);

            q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }

    }
}
