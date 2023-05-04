using Lloyd;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace AlexM
{
	public class PlayerModel : PlayerBase
	{
		public Inputs controls;
		public float  maxSpeed         = 100f;
		public float  sprintMultiplier = 1.5f;

		public float jumpForce    = 70f;
		public float onGroundDrag = 5f;
		public float inAirDrag    = 0f;

		[SerializeField, Header("= Debug =")]
		private float _speed;

		private float originalSpeed;

		[SerializeField]
		private float groundAngle, groundAngleOffset;

		public Health health;

		public InputManager inputManager;
		[FormerlySerializedAs("chatBoxViewModel")]
		public ChatBox chatBox;

		private void Awake()
		{
			GetReferences();
			originalSpeed = maxSpeed;

			health.HealthReducedToZeroEvent += HealthOnHealthReducedToZeroEvent;
			
			inputManager.playerInteractedWithCivEvent += OnplayerInteractedWithCivEvent;
		}
		public void Start()
		{
			GetComponentInChildren<AudioListener>().enabled = true;
		}


		void HealthOnHealthReducedToZeroEvent()
		{
			Debug.Log("PLAYER DIED!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

			// HACK
			SceneManager.LoadScene("Menu");
		}
		private void OnplayerInteractedWithCivEvent()
		{
			// TUrn on chat view
			chatBox.Activate();
			inputManager.InGameTalking();
		}



		void GetReferences()
		{
			_rb           = GetComponent<Rigidbody>();
			_inputManager = GetComponent<InputManager>();
			_slip         = Resources.Load<PhysicMaterial>("Materials/Slip");
			_grip         = Resources.Load<PhysicMaterial>("Materials/Grip");
			// _gripStub     = GetComponentInChildren<SphereCollider>();
		}

		private Vector3 GetForward()
		{
			// RaycastHit hit;
			// bool ray = Physics.Raycast(_gripStub.transform.position, -transform.up, out hit, 0.25f);
			// return _fwdDirection += (transform.forward + hit.normal);
			var T = transform;
			_fwdDirection = T.forward;
			if (_isGrounded)
			{
				_fwdDirection = Vector3.Cross(_groundHitInfo.normal, -T.right);
			}

			return _fwdDirection;
		}


		private Vector3 GetRight()
		{
			return _rightDirection = transform.right;
		}

		private void GetGroundAngle()
		{
			if (_isGrounded)
			{
				_groundAngle = Vector3.Angle(transform.forward, _groundHitInfo.normal);
			}
			else
			{
				return;
			}

			groundAngle        = _groundAngle;
			_groundAngleOffset = groundAngle - 90f;
			groundAngleOffset  = _groundAngleOffset;
		}

		private void CheckGround()
		{
			var rayDown = Physics.Raycast(_gripStub.transform.position, Vector3.down, out _groundHitInfo, 0.35f);

			Debug.DrawLine(_gripStub.transform.forward, _groundHitInfo.point, Color.green);

			_isGrounded = rayDown;

			if (_isGrounded)
			{
				_rb.drag = onGroundDrag;
			}
			else
			{
				_rb.drag = inAirDrag;
			}
		}

		private float GetSpeed()
		{
			_speed = _rb.velocity.magnitude;

			return _speed;
		}

		public void MovementInput(InputAction.CallbackContext obj)
		{
			if (obj.performed)
			{
				_gripStub.material = _slip;
			}

			_inputManager.moveDirection = obj.ReadValue<Vector2>();
			if (_groundAngleOffset > 0)
			{
				maxSpeed = (maxSpeed + _groundAngleOffset / 10f);
			}

			if (obj.canceled)
			{
				_gripStub.material = _grip;

				maxSpeed = originalSpeed;
			}
		}

		public void Sprint(InputAction.CallbackContext obj)
		{
			if (obj.performed)
			{
				_isSprinting = true;
			}

			if (obj.canceled)
			{
				_isSprinting = false;
			}
		}


		public void CrouchInput(InputAction.CallbackContext obj)
		{
			//This needs to be split and networked.
			var T = transform;

			if (obj.performed)
			{
				T.Translate(Vector3.down, Space.Self);
				T.localScale -= new Vector3(0, 0.5f, 0);
			}

			if (obj.canceled)
			{
				T.localScale += new Vector3(0, 0.5f, 0);
			}
		}

		public void JumpInput(InputAction.CallbackContext obj)
		{
			if (_isGrounded)
			{
				Jump();
			}
		}


		public void Jump()
		{
			//Debug.Log("Jumping");
			_rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
		}

		private void Update()
		{
			_movement = (_inputManager.moveDirection.y * _fwdDirection) + (_inputManager.moveDirection.x * _rightDirection);


			// ISwitchable switchable;
			// switchable.TurnOn();
		}


		private void ApplyMovement()
		{
			if (GetSpeed() < maxSpeed)
			{
				if (_isSprinting && _isGrounded)
				{
					_rb.AddForce(_movement.normalized * ((maxSpeed * sprintMultiplier) * Time.fixedDeltaTime), ForceMode.VelocityChange);
				}
				else if (_isGrounded)
				{
					// if (GetSpeed() < 10f)
					// {
					_rb.AddForce(_movement.normalized * (maxSpeed * Time.fixedDeltaTime), ForceMode.VelocityChange);
					// }
				}
			}
		}

		private void FixedUpdate()
		{
			ApplyMovement();
			GetForward();
			GetRight();
			CheckGround();
			GetGroundAngle();
		}

		#region Examples

		/// <summary>
		/// INPUT happens LOCALLY
		/// >>sends COMMAND to SERVER
		/// >>>SERVER RPC to all CLIENTS
		/// </summary>
		private bool examples;

		#endregion
	}
}