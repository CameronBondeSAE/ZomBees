using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCivBrain : MonoBehaviour
{
    public TileCivMouth civMouth;
    //public TileCivHead civHead;
    //public TileCivEyes civEyes;
    
    private void StartGame()
    {
        
    }

    private HearingComp ears;
    public bool listening;
    public float hearingThreshold;
    private HearingEventArgs lastSound;
    
    private MonoBehaviour eyes;
    
    private MonoBehaviour mouth;

    private PathFinder pathfinder;

    private void OnEnable()
    {
        ears = GetComponent<HearingComp>();
        ears.SoundHeardEvent += HeardSomething;
    }

    public void HeardSomething(HearingEventArgs heardSound)
    {
        if (listening)
        {
            //lookTowards.SetTarget(heardSound.Source.transform.position);
        }

        lastSound = heardSound;
    }

    public void SawSomething()
    {
        
    }

    private void OnDisable()
    {
        ears.SoundHeardEvent -= HeardSomething;
    }
}
