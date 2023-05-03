using System;
using Lloyd;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Johns
{
    public class SoundGunSwitchedOn : StateBase
    {
        public AudioClip sonicShot;
        public AudioSource audioSource;
        public SoundEmitter soundEmitter;
        public SoundProperties placeholderForSoundProperties;
        
        public void OnEnable()
        {
            audioSource.clip = sonicShot;
            audioSource.loop = true;
            audioSource.Play();
//            soundEmitter.GetComponent<SoundEmitter>();
            soundEmitter.EmitSound(placeholderForSoundProperties);
        }

        [Button]
        public void MakeSound()
        {
            soundEmitter.EmitSound(placeholderForSoundProperties);
        }

        public void OnDisable()
        {
            audioSource.loop = false;
            audioSource.Stop();
            GetComponent<SoundGunModel>().TurnOff();
        }
    }
}
