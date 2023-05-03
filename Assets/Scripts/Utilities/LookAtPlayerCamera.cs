using System.Collections;
using System.Collections.Generic;
using Lloyd;
using UnityEngine;
using UnityEngine.Animations;

public class LookAtPlayerCamera : MonoBehaviour
{
    public Transform target;
    
    // Start is called before the first frame update
    void Start()
    {
	    target = ZombeeGameManager.Instance.playerModel.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
	    if (target != null)
	    {
		    var targetPosition = target.position;
		    targetPosition.y = transform.position.y;
		    transform.LookAt(targetPosition);
		    transform.Rotate(0,180f,0);
	    }

    }
}
