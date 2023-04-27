using System.Collections;
using System.Collections.Generic;
using Oscar;
using System;
using UnityEngine;
using UnityEngine.AI;

public class CivilianModel : CharacterBase, IInteractable
{
	// public string GPTInfo;
	// public string mainPrompt;

	// public CivGPT civGpt;
	public RandomNavmeshTest randomNavmeshTest;

	
	
	/// <summary>
	/// HACK SECTION
	/// </summary>

	// View
	public CivilianModel civilianModel;

	public Pistol pistol; // HACK

	public Transform gun;

	public Transform        head;
	public List<GameObject> decals;

	[SerializeField]
	private float gunForce = 10f;

	public CivGPT civGpt;
	// HACK: Demo suicide sequence
	// Sequence sequence = DOTween.Sequence();
	// sequence.AppendInterval(7.7f);
	// sequence.Append(gun.transform.DORotate(new Vector3(-109.594f, 0, 0), 0.3f, RotateMode.LocalAxisAdd)
	// 	.SetEase(Ease.InOutQuad));
	// sequence.Play();


	
	void Awake()
	{
		// Init();


		// HACK
		// if (gun != null)
		// {
		// gun.GetComponent<Rigidbody>().isKinematic = true;
		// gun.GetComponent<Collider>().enabled = false;
		// }

		// foreach (var decal in decals)
		// {
		// decal.SetActive(false);
		// }
		
		civGpt.GPTPerformingActionEvent += CivGptOnGPTPerformingActionEvent;
	}

	void CivGptOnGPTPerformingActionEvent(object sender, CivGPT.CivAction action)
	{
		DoAction(action);
	}


	public void DoAction(CivGPT.CivAction civAction)
	{
		switch (civAction)
		{
			case CivGPT.CivAction.Shoot:
				// pistol.Shoot();
				break;
			case CivGPT.CivAction.GatherFood:
				// Check memories for food, else go to resource points


				break;
			case CivGPT.CivAction.FollowPlayer:
				break;
			case CivGPT.CivAction.RunAway:
				randomNavmeshTest.FindRandomSpot();
				break;
			case CivGPT.CivAction.FrozenWithFear:
				break;
			case CivGPT.CivAction.DoNothing:
				break;
			case CivGPT.CivAction.CommitSuicide:
				// pistol.Shoot();
				//
				// // HACK move to model for general death
				// gun.transform.parent                      = null;
				// gun.GetComponent<Rigidbody>().isKinematic = false;
				// gun.GetComponent<Collider>().enabled      = true;
				//
				// GetComponent<NavMeshAgent>().enabled  = false;
				// GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
				// foreach (var decal in decals)
				// {
				// 	decal.SetActive(true);
				// }

				// bloodParticles.Emit(150);

				// HACK move to gun itself
				// head.GetComponent<Rigidbody>()
				// 	.AddForceAtPosition(transform.InverseTransformVector(new Vector3(-30f, 50f, -150f)) * gunForce,
				// 						transform.position + new Vector3(0, 0, 0));

				break;
			case CivGPT.CivAction.Shout:
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}

	}
	
	public void ActivateChatInterface()
	{
		
	}
	
	public void Interact()
	{
		
	}

	public void Inspect()
	{
		throw new System.NotImplementedException();
	}
}
