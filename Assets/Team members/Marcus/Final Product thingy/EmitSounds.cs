using System;
using System.Collections;
using System.Collections.Generic;
using Lloyd;
using UnityEngine;

namespace Marcus
{
    public class EmitSounds : MonoBehaviour
    {
        public SoundEmitter soundEmitter;
        
        private void Start()
        {
            StartCoroutine(FakeEmitSound());
        }

        IEnumerator FakeEmitSound()
        {
            while (true)
            {
                soundEmitter.EmitSound(soundEmitter.testProperties);
                yield return new WaitForSeconds(2f);
            }
        }
    }
}
