using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearingComp : MonoBehaviour, IHear
{
    public QueenEvent eventManager;

    private void OnEnable()
    {
        eventManager = GetComponent<QueenEvent>();
    }

    // Hearing Component uses IHear takes the gameObject Sound Emitter as source
    // calculates distance between HearingComp and source and fires a RaycastAll the length of distance at source
    // hitCount returns how many objects are in between HearingComp and source
    public void SoundHeard(GameObject source)
    {
        float distance = Vector3.Distance(transform.position, source.transform.position);
        RaycastHit[] hits =
            Physics.RaycastAll(transform.position, source.transform.position - transform.position, distance);
        int hitCount = hits.Length;
        Debug.Log("Heard something with " + hitCount + " number of objects between");

        
    }
}