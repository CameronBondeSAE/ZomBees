using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEmitter : MonoBehaviour
{
    private List<IHear> listeners = new List<IHear>();

    private IHear listener;

    private void OnEnable()
    {
        EmitSound(this.gameObject, 50f);
    }

    public void EmitSound(GameObject origin, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        List<IHear> listeners = new List<IHear>();

        foreach (Collider collider in colliders)
        {
            listener = collider.GetComponent<IHear>();

            if (listener != null)
            {
                listener.SoundHeard(origin);
            }
        }
    }
}