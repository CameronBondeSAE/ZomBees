using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class HearingComp : MonoBehaviour, IHear
{
    // Hearing Component uses IHear takes the gameObject Sound Emitter as source
    // calculates distance between HearingComp and source and fires a RaycastAll the length of distance at source
    // hitCount returns how many objects are in between HearingComp and source
    
    // Hearing Comp tracks number of sounds heard with soundsList
    // soundsList is sorted by input volume, loudest to the top
    // 

    public bool heardSound;

    public Vector3 loudestRecentSound;

    private float soundDistance;

    private int thingsBetweenSound;

    public struct SoundData
    {
        public GameObject source;
        public float volume;
        public float fear;
        public float beeness;
        public Team team;
    }

    public List<SoundData> soundsList = new List<SoundData>();

    public void Update()
    {
        if (soundsList.Count > 0)
        {
            heardSound = true;
            loudestRecentSound = soundsList[0].source.transform.position;
        }

        heardSound = false;
    }

    public void SoundHeard(GameObject source, SoundEmitter.SoundType soundType, float volume, float fear, float beeness, Team heardTeam)
    {
        soundDistance = Vector3.Distance(transform.position, source.transform.position);
        RaycastHit[] hits =
            Physics.RaycastAll(transform.position, source.transform.position - transform.position, soundDistance);
        thingsBetweenSound = hits.Length;

        SoundData soundData = new SoundData();
        soundData.source = source;
        soundData.volume = volume;
        soundData.fear = fear;
        soundData.beeness = beeness;
        soundsList.Add(soundData);

        soundsList.Sort((a, b) => -1 * a.volume.CompareTo(b.volume));
        OnSoundHeardEvent(source,volume,fear,beeness,heardTeam);
    }
    
    public event Action<HearingEventArgs> SoundHeardEvent;

    public void OnSoundHeardEvent(GameObject source, float volume, float fear, float beeness, Team heardTeam)
    {
        SoundHeardEvent?.Invoke(new HearingEventArgs{ Source = source, Volume = volume, Fear = fear, Beeness = beeness, Team = heardTeam});
        
        Debug.Log("Heard something"+soundDistance+" far away");
        Debug.Log( + thingsBetweenSound + " number of objects between");
        Debug.Log("Scary level :"+fear);
        Debug.Log("Bee level :"+beeness);
    }


    //where to put perlin randomiser for sound?
         // float noiseX = Mathf.PerlinNoise(point.x * scale, 0f);
         // float noiseY = Mathf.PerlinNoise(point.y * scale, 0f);
         // float noiseZ = Mathf.PerlinNoise(point.z * scale, 0f);
         //
         // // Add the Perlin noise values to each component of the point
         // point.x += noiseX;
         // point.y += noiseY;
         // point.z += noiseZ;
         //public float perlionScale = 1f;

}