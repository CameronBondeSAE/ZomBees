using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;
using Lloyd;
using Sirenix.OdinInspector;

public class SoundEmitter : MonoBehaviour
{
    // sound emitter uses EmitSound to create a nonAllocSphere as big as radius
    // the sphere detects everything within it with IHear and tells them they heard the sound
    // EmitSound transmits the gameObject.transform.position of itself as origin
    // EmitSound transmits the float fear for how much fear level is changed
    // EmitSound transmits the float team to communicate what team origin is (Human / Bee)

    private List<IHear> listeners = new List<IHear>();

    private IHear listener;

    [Header("SIZE OF NOISE RADIUS")] public float radius;

    //[Header("VOLUME OF SOUND")] public float volume;
    
    [Header("INCREASE/DECREASE FEAR")] public float fear;

    [Header("HOW MUCH LIKE A BEE")] public float beeness;

    [Header("TEAM?")] public Team team;

    [Header("THE MAX NUMBER OF LISTENERS (lower for performance?)")]
    public int maxListeners;

    private void OnEnable()
    {
       // EmitSound(gameObject, radius);
    }

    [Button]
    public void EmitInspectorSound()
    {
        EmitSound(this.gameObject, radius, fear, beeness, team);
    }
    public void EmitSound(GameObject origin, float volume, float fear, float beeness, Team team)
    {
        Collider[] hitColliders = new Collider[maxListeners];
        int numColliders = Physics.OverlapSphereNonAlloc(origin.transform.position, radius, hitColliders);

        for (int i = 0; i < numColliders; i++)
        {
            Collider collider = hitColliders[i];

            listener = collider.GetComponent<IHear>();
            if (listener != null)
            {
                listener.SoundHeard(origin, volume, fear, beeness, team);
            }
        }
    }
}