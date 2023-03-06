using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using HarmonyLib;
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

    public GameObject loudestSound;

    public struct SoundData
    {
        public GameObject source;
        public float volume;
        public float fear;
        public float beeness;
    }

    public List<SoundData> soundsList = new List<SoundData>();

    public void Update()
    {
        if (soundsList.Count > 0)
        {
            heardSound = true;
            loudestSound = soundsList[0].source;
        }

        heardSound = false;
    }


    public void SoundHeard(GameObject source, float volume, float fear, float beeness)
    {
        float distance = Vector3.Distance(transform.position, source.transform.position);
        RaycastHit[] hits =
            Physics.RaycastAll(transform.position, source.transform.position - transform.position, distance);
        int hitCount = hits.Length;
        
        Debug.Log("Heard something with " + hitCount + " number of objects between");

        SoundData soundData = new SoundData();
        soundData.source = source;
        soundData.volume = volume;
        soundData.fear = fear;
        soundData.beeness = beeness;
        soundsList.Add(soundData);
        
        soundsList.Sort((a, b) => -1 * a.volume.CompareTo(b.volume));
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