using System.Collections.Generic;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Lloyd
{
    public class CivilianTraits : MonoBehaviour, ICivilian
    {
        // [ShowInInspector] public Dictionary<TraitScriptableObject, TraitStats> traitsDictionary;
        [ShowInInspector] public List<TraitStats> traits;

        void Awake()
        {
            foreach (TraitStats trait in traits)
            {
                // Triggers setting thresholdHit
                UpdateThresholds(trait, trait.value);
            }

            StartCoroutine(UpdateChangingTraits());
        }

        IEnumerator UpdateChangingTraits()
        {
            while (true)
            {
                foreach (TraitStats trait in traits)
                {
                    if (!Mathf.Approximately(0, trait.changeValueOverTime))
                    {
                        UpdateTrait(trait.traitScriptableObject, trait.value+trait.changeValueOverTime/10f);
                    }
                }

                yield return new WaitForSeconds(1f);
            }

            yield return null;
        }

        public event Action<TraitStats> HitThresholdEvent;

        public TraitStats GetTrait(TraitScriptableObject traitScriptableObject)
        {

            foreach (TraitStats civilianTraitsTrait in traits)
            {
                if (civilianTraitsTrait.traitScriptableObject == traitScriptableObject)
                {
                    // Found it
                    return civilianTraitsTrait;
                }
            }

            return null;
        }

        // TODO
        // Shift traits over time
        
        
        public void UpdateTrait(TraitScriptableObject key, float newValue)
        {
            foreach (TraitStats civilianTraitsTrait in traits)
            {
                if (civilianTraitsTrait.traitScriptableObject == key)
                {
                    if (newValue > 1.0f)
                    {
                        newValue = 1.0f;
                    }
                    else if (newValue < 0.0f)
                    {
                        newValue = 0.0f;
                    }

                    UpdateThresholds(civilianTraitsTrait, newValue);

                    civilianTraitsTrait.value = newValue;
                }
            }
        }

        void UpdateThresholds(TraitStats civilianTraitsTrait, float newValue)
        {
            if (newValue >= civilianTraitsTrait.threshold)
            {
                civilianTraitsTrait.thresholdHit = true;
                HitThresholdEvent?.Invoke(civilianTraitsTrait);
            }
            else if (newValue < civilianTraitsTrait.threshold)
            {
                civilianTraitsTrait.thresholdHit = false;
            }
        }
    }

    [Serializable]
    public class TraitStats
    {
        public TraitScriptableObject traitScriptableObject;

        [Header("Don't update in play mode")]
        // TODO tell ODIN to do ReadOnlyInPlay/Edit [ReadOnly]
        [Range(0f, 1f)]
        public float value = 0.5f;

        [Range(0f, 1f)] public float threshold = 1.0f;

        [Range(-1f, 1f)]
        public float changeValueOverTime = 0f;

        [ReadOnly] public bool thresholdHit;
    }
}