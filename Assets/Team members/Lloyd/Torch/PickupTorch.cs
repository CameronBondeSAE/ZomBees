using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Lloyd
{
    public class PickupTorch : MonoBehaviour
    {
        public bool shining;
        public Light light;

        public ItemPickup pickup;

        private void OnEnable()
        {
            light = GetComponent<Light>();
            pickup = GetComponent<ItemPickup>();

            pickup.AnnouncePickupStatus += FlipLight;
        }

        private void FlipLight(ItemPickup.PickupState currState)
        {
            if (currState == ItemPickup.PickupState.Active || currState == ItemPickup.PickupState.HeldActive)
                shining = true;

            else if (currState == ItemPickup.PickupState.Destroyed || currState == ItemPickup.PickupState.Idle ||
                     currState == ItemPickup.PickupState.Held)
                shining = false;

            if (shining)
                light.enabled = true;

            else
                light.enabled = false;
        }

        private void OnDisable()
        {
            pickup.AnnouncePickupStatus -= FlipLight;
        }
    }
}