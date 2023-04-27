using System;
using Lloyd;
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
            soundEmitter?.EmitSound(placeholderForSoundProperties);
        }

        public void OnDisable()
        {
            audioSource.loop = false;
            audioSource.Stop();
            GetComponent<SoundGunModel>().TurnOff();
        }
    }
}
