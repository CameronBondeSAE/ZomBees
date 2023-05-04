using System.Collections;
using System.Collections.Generic;
using Oscar;
using System;
using DG.Tweening;
using Lloyd;
using UnityEngine;
using UnityEngine.AI;

public class CivilianModel : CharacterBase, IInteractable, IHear
{
	// public string GPTInfo;
	// public string mainPrompt;

	// public CivGPT civGpt;
	public RandomNavmeshTest randomNavmeshTest;

	public NavMeshAgent navMeshAgent;
	
	
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
		
		civGpt.GPTOutputDialogueEvent += CivGptOnGPTOutputDialogueEvent;
		civGpt.GPTPerformingActionEvent += CivGptOnGPTPerformingActionEvent;
	}

	private void CivGptOnGPTOutputDialogueEvent(object sender, string e)
	{
		Debug.Log("Look at character talking to me : "+civGpt.currentChat.CharacterBase.gameObject.name, gameObject);
		transform.DOLookAt(civGpt.currentChat.CharacterBase.transform.position, 2f, AxisConstraint.Y, Vector3.up);
	}

	void CivGptOnGPTPerformingActionEvent(object sender, CivGPT.GPTResponseData gptResponseData)
	{
		DoAction(gptResponseData);
	}


	public void DoAction(CivGPT.GPTResponseData gptResponseData)
	{
		Debug.Log("Cam's civ action = "+gptResponseData.CivAction, gameObject);
		switch (gptResponseData.CivAction)
		{
			case CivGPT.CivAction.ActivateLightsToAttractCreatures:
				break;
			case CivGPT.CivAction.ActivateSonicWeapon:
				break;
			case CivGPT.CivAction.CloseDoor:
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
			case CivGPT.CivAction.DoNothing:
				navMeshAgent.ResetPath();
				randomNavmeshTest.enabled = false;
				break;
			case CivGPT.CivAction.DropItem:
				break;
			case CivGPT.CivAction.FindAndShootCreature:
				break;
			case CivGPT.CivAction.FindSafeLocation:
				break;
			case CivGPT.CivAction.FollowOtherCharacter:
				break;
			case CivGPT.CivAction.FindFood:
				// Check memories for food, else go to resource points
				// GetComponent<MemoryManger>().AddMemory();
				
				navMeshAgent.SetDestination(ZombeeGameManager.Instance.ConvertGridSpaceToWorldSpace(gptResponseData.GridCoordinateForAction));
				break;
			case CivGPT.CivAction.FindBomb:
				break;
			case CivGPT.CivAction.RunAndHide:
				randomNavmeshTest.FindRandomSpot();
				break;
			case CivGPT.CivAction.SearchArea:
				randomNavmeshTest.FindRandomSpot();
				randomNavmeshTest.enabled = true;
				break;
			case CivGPT.CivAction.ShootOtherCharacter:
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
		// navMeshAgent.SetDestination(ZombeeGameManager.Instance.ConvertGridSpaceToWorldSpace("K8"));
		// navMeshAgent.SetDestination(new Vector3(400,0,400));

	}

	public void Inspect()
	{
		throw new System.NotImplementedException();
	}

	public void SoundHeard(SoundProperties soundProperties)
	{
		// TODO look at other sounds
		if (soundProperties.Team == Team.Human)
		{
			Debug.Log(gameObject.name + " heard something from "+soundProperties.Source.name);
			transform.DOLookAt(soundProperties.Source.transform.position, 1f, AxisConstraint.Y, Vector3.up).SetEase(Ease.InOutSine);
		}
	}
}
