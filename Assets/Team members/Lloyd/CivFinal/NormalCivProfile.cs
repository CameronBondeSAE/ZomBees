using System.Collections;
using System.Collections.Generic;
using Oscar;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lloyd
{

    public class NormalCivProfile : DynamicObject, ICivilian
    {
        private CivSensor sensor;

        [ShowInInspector] public Dictionary<string, TraitStats> emoteDictionary;

        public bool hungry;

        public float hungerLevel;
        public float hungerThresh;

        public bool bee;

        public float beeness;
        public float beenessThresh;

        public bool readyToDie;

        public float suicideLevel;
        public float suicideThresh;

        private void Start()
        {
            sensor = GetComponent<CivSensor>();

            emoteDictionary = new Dictionary<string, TraitStats>();

            emoteDictionary.Add("Beeness", new TraitStats());
            emoteDictionary.Add("Hunger", new TraitStats());
            emoteDictionary.Add("Suicidal", new TraitStats());

            isCiv = true;
        }


        public void HitByBee(BeeStingAttack.BeeStingType attackType, float amount)
        {
            if (attackType == BeeStingAttack.BeeStingType.BeenessIncreaser)
            {
                UpdateTrait("Beeness", amount);
            }

            else if (attackType == BeeStingAttack.BeeStingType.Attack)
            {
                Health health = GetComponent<Health>();
                health.Change(-amount);
            }
        }

        [Button]
        public void UpdateTrait(string key, float value)
        {
            if (emoteDictionary.ContainsKey(key))
            {
                TraitStats traitStats = emoteDictionary[key];
                float oldValue = traitStats.value;
                float newValue = oldValue + value;

                if (newValue > 1.0f)
                {
                    newValue = 1.0f;
                }
                else if (newValue < 0.0f)
                {
                    newValue = 0.0f;
                }

                traitStats.value = newValue;

                if (newValue >= traitStats.threshold)
                {
                    traitStats.thresholdHit = true;
                }
                else if (newValue < traitStats.threshold)
                {
                    traitStats.thresholdHit = false;
                }

                if (key == "Hunger")
                {
                    hungerLevel = newValue;
                    hungry = traitStats.thresholdHit;
                }

                if (key == "Beeness")
                {
                    beeness = newValue;
                    bee = traitStats.thresholdHit;

                    if (beeness > beenessThresh)
                    {
                        sensor.BecomeEgg();
                    }
                }

                if (key == "Suicidal")
                {
                    suicideLevel = newValue;
                }
            }
        }

        [Button]
        public void OnIncreaseSuicidal()
        {
            StartCoroutine(IncreaseSuicidal());
        }

        private IEnumerator IncreaseSuicidal()
        {
            while (true)
            {
                UpdateTrait("Suicidal", .0025f);

                yield return new WaitForSeconds(1);
                Debug.Log("Getting more suicidal");

                if (suicideLevel > suicideThresh)
                {
                    readyToDie = true;
                }

                else
                {
                    readyToDie = false;
                }
            }
        }

        [Button]
        public void OnIncreaseHunger()
        {
            StartCoroutine(IncreaseHunger());
        }

        private IEnumerator IncreaseHunger()
        {
            while (true)
            {
                UpdateTrait("Hunger", .0025f);

                yield return new WaitForSeconds(1);
                Debug.Log("Getting hungrier");

                if (hungerLevel > hungerThresh)
                {
                    hungry = true;
                }

                else
                {
                    hungry = false;
                }
            }
        }

        [Button]
        public void OnIncreaseBeeness()
        {
            StartCoroutine(IncreaseBeeness());
        }

        private IEnumerator IncreaseBeeness()
        {
            while (true)
            {
                UpdateTrait("Beeness", 1f);

                yield return new WaitForSeconds(1);
                Debug.Log("Getting more bee-like");

                if (beeness > beenessThresh)
                {
                    sensor.BecomeEgg();
                }

                else
                {

                }
            }
        }
    }
}
