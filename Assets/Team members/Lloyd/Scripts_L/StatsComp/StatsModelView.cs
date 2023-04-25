using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lloyd
{
    public class StatsModelView : MonoBehaviour
    {
        public event Action<string, float> ChangeFloat;

        public void OnChangeFloat(string floatName, float amountChanged)
        {
            ChangeFloat?.Invoke(floatName, amountChanged);
        }
        
        public event Action<Dictionary<string, float>> SetDictionary;

        public void OnSetDictionary(Dictionary<string, float> statValues)
        {
            SetDictionary?.Invoke(statValues);
        }

    }
}
