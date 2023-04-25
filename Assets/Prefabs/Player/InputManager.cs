using System;
using Oscar;
using UnityEngine;
using UnityEngine.InputSystem;
using Virginia;

namespace AlexM
{
	public class InputManager : MonoBehaviour
	{
	#region Variables

		private Inputs          _controls;
		private PlayerModel  playerModel;
		private CamMouseLook    _camScript;

		public InteractScript interaction;

		// [HideInInspector]
		public Vector2 moveDirection;

		public Inventory leftHandInventory;
		public Inventory rightHandInventory;

		public event Action playerInteractedWithCivEvent;
		
		#endregion
		

		private void GetReferences()
		{
			playerModel = GetComponent<PlayerModel>();
			_camScript = GetComponentInChildren<CamMouseLook>();
		}

		public void InGameTalking()
		{
			_controls.Disable();
		
			_controls.Ingametalking.Enable();
			_controls.Ingametalking.Sendmessage.performed += SendmessageOnperformed;
			_controls.Ingametalking.Cancel.performed+= CancelOnperformed;
			
		}

		void CancelOnperformed(InputAction.CallbackContext obj)
		{
			playerModel.chatBox.Deactivate();
			InGameNotTalking();
		}

		void SendmessageOnperformed(InputAction.CallbackContext obj)
		{
			// TODO: Needed? Due to inputfield already knowing what submitting keys are
			InGameNotTalking(); // CHECK: Turn off with every message?
		}

		public void InGameNotTalking()
		{
			_controls.Disable();
			// _controls.Ingametalking.Disable();
			
			_controls.Ingame.Enable();
			_controls.Ingame.Move.performed         += playerModel.MovementInput;
			_controls.Ingame.Move.canceled          += playerModel.MovementInput;
			_controls.Ingame.Jump.performed         += playerModel.JumpInput;
			_controls.Ingame.Sprint.performed       += playerModel.Sprint;
			_controls.Ingame.Sprint.canceled        += playerModel.Sprint;
			_controls.Ingame.Crouch.performed       += playerModel.CrouchInput;
			_controls.Ingame.Crouch.canceled        += playerModel.CrouchInput;
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
				DynamicObject dynamicObject = interaction.Interact();
				
				// Player interacted with Civ?
				if (dynamicObject != null && dynamicObject.GetComponent<CivilianModel>())
				{
					playerInteractedWithCivEvent?.Invoke();
				}
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