using System;
using System.Collections.Generic;
using UnityEngine;

namespace Team_members.Lloyd.Scripts_L.StatsComp
{
    public class StatsView : MonoBehaviour
    {
        private StatsModelView modelView = new StatsModelView();

        private Dictionary<string, float> floats;

        private void OnEnable()
        {
            modelView.ChangeFloat += ChangeFloat;
            modelView.SetDictionary += SetDictionary;
        }

        private void ChangeFloat(string key, float amount)
        {
            floats[key] += amount;
        }

        private void SetDictionary(Dictionary<string, float> dict)
        {
            floats = dict;
        }

        private void OnDisable()
        {
            modelView.ChangeFloat -= ChangeFloat;
            modelView.SetDictionary -= SetDictionary;;
        }
    }
}
