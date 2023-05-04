using System;
using System.Collections;
using System.Collections.Generic;
using Lloyd;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Marcus
{
    public class EmitSounds : MonoBehaviour
    {
        public SoundEmitter soundEmitter;
        public AudioSource  audioSource;
        
        private void Start()
        {
            StartCoroutine(FakeEmitSound());
        }
        
        

        IEnumerator FakeEmitSound()
        {
            yield return new WaitForSeconds(Random.Range(0, 5f));
            audioSource.Play();
            audioSource.pitch = Random.Range(0.6f, 1.5f);
            
            while (true)
            {
                soundEmitter.EmitSound(soundEmitter.testProperties);
                yield return new WaitForSeconds(2f);
            }
        }
    }
}
