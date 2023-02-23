using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEmitter : MonoBehaviour
{
    public float radius;

    private List<IHear> listeners = new List<IHear>();

    private IHear listener;

    private void OnEnable()
    {
        EmitSound();
    }

    private void EmitSound()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        List<IHear> listeners = new List<IHear>();

        foreach (Collider collider in colliders)
        {
            IHear listener = collider.GetComponent<IHear>();

            if (listener != null)
            {
                listeners.Add(listener);
                Vector3 listenerPos = collider.transform.position;
                RaycastHit[] hits = Physics.RaycastAll(transform.position, listenerPos - transform.position, radius);
                int hitCount = hits.Length;

                if (hits.Length <= radius)
                {
                    listener.SoundHeard(gameObject, hitCount);
                }
            }
        }
    }
}