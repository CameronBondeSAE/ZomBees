using System;
using UnityEngine;
using Sirenix.OdinInspector;


namespace Johns
{
    public class SoundWeaponLogic : MonoBehaviour, IItem, IPowered, IInteractable, ISwitchable
    {
        public AudioClip sonicShot;
        public AudioSource audioSource;
        public bool poweredOn;
        

        private void OnEnable()
        {
            audioSource.clip = sonicShot;
        }

        
        [Button]
        public void PoweredOn()
        {
            poweredOn = true;
        }

        [Button]
        public void PoweredOff()
        {
            poweredOn = false;
        }

        [Button]
        public void TurnOn()
        {
            if (poweredOn)
            {
                audioSource.loop = true;
                audioSource.Play();
            }
        }

        [Button]
        public void TurnOff()
        {
            if (poweredOn == false)
            {
                audioSource.loop = false;
                audioSource.Stop();
            }
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
        
        public void Interact()
        {
            throw new NotImplementedException();
        }

        public void Inspect()
        {
            throw new NotImplementedException();
        }


    } 
}

