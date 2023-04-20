using System;
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

		public Inventory leftHandInventory;
		public Inventory rightHandInventory;

		public Talking talking;
		
		#endregion

		private void GetReferences()
		{
			_pModel = GetComponent<PlayerModel>();
			_camScript = GetComponentInChildren<CamMouseLook>();
		}

		private void InGameTalking()
		{
			_controls.Disable();
		
			_controls.Ingametalking.Enable();
			_controls.Ingametalking.Sendmessage.performed += SendmessageOnperformed;
			_controls.Ingametalking.Cancel.performed+= CancelOnperformed;
			
		}

		void CancelOnperformed(InputAction.CallbackContext obj)
		{
			talking.gameObject.SetActive(false);
		}

		void SendmessageOnperformed(InputAction.CallbackContext obj)
		{
			talking.gameObject.SetActive(true);
		}

		private void InGameNotTalking()
		{
			_controls.Ingametalking.Disable();
			
			_controls.Ingame.Enable();
			_controls.Ingame.Move.performed         += _pModel.MovementInput;
			_controls.Ingame.Move.canceled          += _pModel.MovementInput;
			_controls.Ingame.Jump.performed         += _pModel.JumpInput;
			_controls.Ingame.Sprint.performed       += _pModel.Sprint;
			_controls.Ingame.Sprint.canceled        += _pModel.Sprint;
			_controls.Ingame.Crouch.performed       += _pModel.CrouchInput;
			_controls.Ingame.Crouch.canceled        += _pModel.CrouchInput;
			_controls.Ingame.Interact.performed     += UseOnperformed;
			_controls.Ingame.Pickupleft.performed   += context => PickupOnperformed(context, Hand.Left);
			_controls.Ingame.Dropleft.performed     += context => DropOnperformed(context, Hand.Left);
			_controls.Ingame.Pickupright.performed  += context => PickupOnperformed(context, Hand.Right);
			_controls.Ingame.Dropright.performed    += context => DropOnperformed(context, Hand.Right);
			_controls.Ingame.Uselefthand.performed  += context => UsehandOnperformed(context, Hand.Left);
			_controls.Ingame.Userighthand.performed += context => UsehandOnperformed(context, Hand.Right);
			_controls.Ingame.Menu.performed         += MenuOnperformed;
			

}

		void MenuOnperformed(InputAction.CallbackContext obj)
		{
			
		}

		enum Hand
		{
			Left,
			Right
		}

		Inventory i = null;

		void PickupOnperformed(InputAction.CallbackContext obj, Hand hand)
		{
			i = WhichHandInventoryToUse(hand);
			i?.Pickup();
		}

		void DropOnperformed(InputAction.CallbackContext obj, Hand hand)
		{
			i = WhichHandInventoryToUse(hand);
			i?.Dispose();
		}


		void UsehandOnperformed(InputAction.CallbackContext obj, Hand hand)
		{
			i = WhichHandInventoryToUse(hand);

			if (i.heldItem != null)
			{
				if (i.heldItem is IInteractable)
				{
					(i.heldItem as IInteractable)?.Interact();
				}
				else
				{
					i.heldItem.Consume();
				}
			}
		}

		Inventory WhichHandInventoryToUse(Hand hand)
		{
			switch (hand)
			{
				case Hand.Left:
					return leftHandInventory;
					break;
				case Hand.Right:
					return rightHandInventory;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(hand), hand, null);
			}
		}

		void UseOnperformed(InputAction.CallbackContext obj)
		{
			if (obj.performed)
			{
				interaction.Interact();
			}
		}

		private void Awake()
		{
			_controls = new Inputs();
			GetReferences();
			InGameNotTalking();
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