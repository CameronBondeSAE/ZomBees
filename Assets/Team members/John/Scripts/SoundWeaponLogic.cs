using System;
using UnityEngine;
using Sirenix.OdinInspector;


namespace Johns
{
    public class SoundWeaponLogic : MonoBehaviour, IItem, ISwitchable
    {
        public AudioClip sonicShot;
        public AudioSource audioSource;

        private void OnEnable()
        {
            audioSource.clip = sonicShot;
        }

        public void Consume()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public string Description()
        {
            throw new System.NotImplementedException();
        }

        public void Pickup(GameObject whoPickedMeUp)
        {
            throw new System.NotImplementedException();
        }

        [Button]
        public void TurnOn()
        {
            audioSource.loop = true;
            audioSource.Play(); 
        }

        [Button]
        public void TurnOff()
        {
            audioSource.loop = false;
            audioSource.Stop();
        }
    } 
}

