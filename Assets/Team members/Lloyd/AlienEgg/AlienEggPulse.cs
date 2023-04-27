using System;
using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lloyd
{
    public class AlienEggPulse : MonoBehaviour, IInteractable
    {
        // Alien Egg starts with custom HP and a countdown Timer set by timeUntilHatch. Time counts down every second, when it reaches 0, the egg will hatch.
        //
        // Alien Egg is an IInteractable. Interacting with it can reduce its HP. Reducing HP to zero will result in destroying the alien egg
        //
        // Alien Egg uses DoTween to "pulse". It gets increasingly faster the less time it has left to hatch
        //
        // Alien Egg spawns either the Guy it started as or a designated Bee depending on if the Egg is destroyed before it hatches or not

        private bool pulsing;

        public GameObject Guy;
        public GameObject Bee;

        public GameObject closedEgg;
        public GameObject openEgg;

        public int maxHP;
        public int HP;
    
        public float scaledByAmount;
        public float timePulsing;

        public int timeUntilHatch;
        public int time;
        private bool ticking;

        [Button]
        public void DestroyEgg()
        {
            Interact();
        }
        public void Interact()
        {
            HP--;
            if (HP == 0)
            {
                OnSafe();
            }
        }

        public void Inspect()
        {
            Debug.Log("OhMyGodItsACreepyEgg");
        }

        private void OnEnable()
        {
            openEgg.SetActive(false);
            HP = maxHP;
            time = timeUntilHatch;
            ticking = true;
            pulsing = true;
            StartCoroutine(Pulse());
            StartCoroutine(Countdown());
        }

        private IEnumerator Countdown()
        {
            while (ticking)
            {
                time--;
                yield return new WaitForSeconds(1);

                if (time == 0)
                {
                    OnTimesUp();
                }
            }
        }

        private IEnumerator Pulse()
        {
            while (pulsing)
            {
                float pulseDuration = Mathf.Lerp(0.1f, timePulsing, (float)time / (float)timeUntilHatch);
        
                transform.DOScale(transform.localScale * scaledByAmount, pulseDuration) // scale up
                    .SetEase(Ease.OutQuad);
        
                yield return new WaitForSeconds(pulseDuration);
        
                transform.DOScale(transform.localScale / scaledByAmount, pulseDuration) // scale down
                    .SetEase(Ease.InQuad);
        
                yield return new WaitForSeconds(pulseDuration);
            }
        }

        public void FlipTime(bool tocking)
        {
            ticking = tocking;
        }
    
        public event Action SafeEvent;  

        public void OnSafe()
        {
            openEgg.SetActive(true);
            closedEgg.SetActive(false);
            SafeEvent?.Invoke();
         //   Debug.Log("SAFE!");
            ticking = false;
            pulsing = false;
        }
    
        public event Action TimesUpEvent;

        public void OnTimesUp()
        {
            TimesUpEvent?.Invoke();
        
            openEgg.SetActive(true);
            closedEgg.SetActive(false);
        
//            Debug.Log("TIMES UP!");
            ticking = false;
            pulsing = false;
        }
    
    }
}