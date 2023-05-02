using System.Collections.Generic;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

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
                UpdateThresholds(trait.value, trait);
            }
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

                    UpdateThresholds(newValue, civilianTraitsTrait);

                    civilianTraitsTrait.value = newValue;
                }
            }
        }

        void UpdateThresholds(float newValue, TraitStats civilianTraitsTrait)
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

        [ReadOnly] public bool thresholdHit;
    }
}