using System.Collections;
using System.Collections.Generic;
using Lloyd;
using Oscar;
using UnityEngine;

namespace Virginia
{
    public class HealthSupplies : DynamicObject, IItem
    {
        GameObject   whoPickedMeUp;
        public float healthAmount = 50;
        public TraitScriptableObject scriptableObject;

        public void Pickup(GameObject _whoPickedMeUp)
        {
            whoPickedMeUp                         = _whoPickedMeUp;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Collider>().enabled      = false;
        }


        public void Consume()
        {
            whoPickedMeUp.GetComponent<Health>().Change(healthAmount);
            whoPickedMeUp.GetComponent<CivilianTraits>().UpdateTrait(scriptableObject, 0f);
            Destroy(gameObject);
        }

        public void Dispose()
        {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Collider>().enabled      = true;
        }

        public string Description()
        {
            return description;
        }
    }
}

