using UnityEngine;
using UnityEngine.InputSystem;
using Virginia;

namespace AlexM
{
	public class InputManager : MonoBehaviour
	{
	#region Variables

		private Inputs          _controls;
		private PlayerModel  _pModel;
		private CamMouseLook    _camScript;

		public InteractScript interaction;

		// [HideInInspector]
		public Vector2 moveDirection;

		public Inventory leftHand;
		public Inventory rightHand;

		#endregion

		private void GetReferences()
		{
			_pModel = GetComponent<PlayerModel>();
			_camScript = GetComponentInChildren<CamMouseLook>();
		}

		private void ControlSetup()
		{
			_controls = new Inputs();
			_controls.Enable();
			_controls.Movement.Move.performed   += _pModel.MovementInput;
			_controls.Movement.Move.canceled    += _pModel.MovementInput;
			_controls.Movement.Jump.performed   += _pModel.JumpInput;
			_controls.Movement.Jump.canceled    += _pModel.JumpInput;
			_controls.Movement.Sprint.performed += _pModel.Sprint;
			_controls.Movement.Sprint.canceled  += _pModel.Sprint;
			_controls.Movement.Crouch.performed += _pModel.CrouchInput;
			_controls.Movement.Crouch.canceled  += _pModel.CrouchInput;
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