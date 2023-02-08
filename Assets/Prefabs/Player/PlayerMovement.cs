using UnityEngine;
using UnityEngine.InputSystem;

namespace AlexM
{
	public class PlayerMovement : PlayerBase
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

		private void Awake()
		{
			GetReferences();
			originalSpeed = maxSpeed;
		}

		public void Start()
		{
			GetComponentInChildren<AudioListener>().enabled = true;
		}


		private void OnEnable()
		{
			controls = new Inputs();
			controls.Movement.Enable();
			controls.Movement.Move.performed += ctx => _movementInput = controls.Movement.Move.ReadValue<Vector2>();
		}

		private void OnDisable()
		{
			controls.Movement.Disable();
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
			if (obj.performed)
			{
				//Debug.LogWarning("JumpInput Performed.");
				if (_isGrounded)
				{
					Jump();
				}
			}

			if (obj.canceled)
			{
				//Debug.LogWarning("JumpInput Cancelled.");
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