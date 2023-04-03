using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCivBrain : MonoBehaviour
{
    [Header("CIV HAS A THIS % CHANCE TO TALK EVERY EVENT")]
    public int talkPercent;

    public TileCivMouth civMouth;
    //public TileCivHead civHead;
    //public TileCivEyes civEyes;

    public enum CivBrainState
    {
        Talking,
        Listening
    }
    public CivBrainState myState;
    
    private void StartGame()
    {
        civMouth = GetComponent<TileCivMouth>();
    }

    private HearingComp ears;
    public bool listening;
    public float hearingThreshold;
    private HearingEventArgs lastSound;
    
    private MonoBehaviour eyes;
    
    private MonoBehaviour mouth;

    private TileCivHead lookTowards;

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
            lookTowards.SetTarget(heardSound.Source.transform.position);
        }

        lastSound = heardSound;
        
        
    }

    public void SawSomething()
    {
        
    }

    public void Exclaim(CivEventArgs.Topic topic)
    {
        int randomValue = UnityEngine.Random.Range(0, 100);
        if (randomValue <= talkPercent) ;
        
        
    }

    private void OnDisable()
    {
        ears.SoundHeardEvent -= HeardSomething;
    }
}
