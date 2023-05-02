using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AlexM
{
	public class CamMouseLook : MonoBehaviour
	{
		public float mouseSensitivity = 1f;
		public bool  invert = false;
		
		public Camera    camera;
		public Transform playerbody;

		//public float mouseSensitivity;
		public float pitch;
		public bool  cursorVisible;

		[HideInInspector]
		public float mouseX, mouseY;

		private Vector2 mouseDelta;

		private Quaternion lookAngle;

		// Start is called before the first frame update
		void Start()
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible   = false;
		}

		void FixedUpdate()
		{
			InternalLockUpdate();
			// Debug.Log(Cursor.visible + " : " +Cursor.lockState);
			if (Cursor.lockState == CursorLockMode.Locked)
			{
				OldMouseMovement();
			}
		}
		
		bool m_cursorIsLocked;
		private void InternalLockUpdate()
		{
#if UNITY_EDITOR
			if (InputSystem.GetDevice<Keyboard>().escapeKey.wasPressedThisFrame)
			{
				m_cursorIsLocked = false;
			}
			else if (InputSystem.GetDevice<Mouse>().leftButton.wasPressedThisFrame)
			{
				m_cursorIsLocked = true;
			}
#endif
			if (m_cursorIsLocked)
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
			else if (!m_cursorIsLocked)
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
		}

		void MouseInput()
		{
			mouseDelta = Mouse.current.delta.ReadValue();
		}

		void LookAngle(Quaternion angle)
		{
			//Head's up/down movement
			camera.transform.localRotation = angle;
		}

		void OldMouseMovement()
		{
			MouseInput();

			float mouseXSpeed = mouseDelta.x;
			float mouseYSpeed = mouseDelta.y;

			if (invert)
			{
				pitch += mouseYSpeed * mouseSensitivity;
			}
			else
			{
				pitch -= mouseYSpeed * mouseSensitivity;
			}
			pitch =  Mathf.Clamp(pitch, -89f, 89f);

			//Rotate the main body of the player on the horizontal axis
			playerbody.Rotate(Vector3.up * (mouseXSpeed * mouseSensitivity));


			//client side prediction..
			//camera.transform.localRotation = Quaternion.Euler(pitch, 0, 0);
			lookAngle = Quaternion.Euler(pitch, 0, 0);
			LookAngle(lookAngle);
			// Cursor.visible = false;
		}
	}
}