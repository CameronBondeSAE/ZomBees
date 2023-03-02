using UnityEngine;
using UnityEngine.InputSystem;
using Virginia;

namespace AlexM
{
	public class InputManager : MonoBehaviour
	{
	#region Variables

		private Inputs          _controls;
		private PlayerMovement  _pMovement;
		private CamMouseLook    _camScript;

		public Interaction interaction;

		// [HideInInspector]
		public Vector2 moveDirection;

		#endregion

		private void GetReferences()
		{
			_pMovement = GetComponent<PlayerMovement>();
			_camScript = GetComponentInChildren<CamMouseLook>();
		}

		private void ControlSetup()
		{
			_controls = new Inputs();
			_controls.Enable();
			_controls.Movement.Move.performed   += _pMovement.MovementInput;
			_controls.Movement.Move.canceled    += _pMovement.MovementInput;
			_controls.Movement.Jump.performed   += _pMovement.JumpInput;
			_controls.Movement.Jump.canceled    += _pMovement.JumpInput;
			_controls.Movement.Sprint.performed += _pMovement.Sprint;
			_controls.Movement.Sprint.canceled  += _pMovement.Sprint;
			_controls.Movement.Crouch.performed += _pMovement.CrouchInput;
			_controls.Movement.Crouch.canceled  += _pMovement.CrouchInput;
			_controls.Movement.Use.performed    += UseOnperformed;
		}

		void UseOnperformed(InputAction.CallbackContext obj)
		{
			interaction.Interact();
		}

		private void Awake()
		{
			GetReferences();
			ControlSetup();
		}

		private void OnEnable()
		{
			if (_controls != null)
			{
				_controls.Enable();
			}
		}

		private void OnDisable()
		{
			if (_controls != null)
			{
				_controls.Disable();
			}
		}
	}
}