using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Oscar;
using UnityEngine;


namespace Virginia
{
	public class InteractScript : MonoBehaviour
	{
		[Button]
		public DynamicObject Interact()
		{
			RaycastHit hitInfo;
			if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 3f))
			{
				Debug.DrawLine(transform.position, hitInfo.point, Color.blue, 4f);
				Debug.Log("hit");

				IInteractable thinginfront = hitInfo.transform.GetComponent<IInteractable>();
				if (thinginfront != null)
				{
					thinginfront.Interact();
					
					return hitInfo.transform.GetComponent<DynamicObject>();
				}

				return null;
			}
			return null;
		}
	}
}