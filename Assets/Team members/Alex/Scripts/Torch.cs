// using Unity.Netcode;
using UnityEngine;

namespace Alex
{
/*    public class Torch : NetworkBehaviour
    {
        public bool isOn = false;
        public GameObject lightSorce;
        public AudioSource clickSound;
  

        // Update is called once per frame
        void Update()
        {
            if(IsLocalPlayer)
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    TurnServerRpc();
                }
        }

        [ServerRpc]
        void TurnServerRpc()
        {
            isOn = !isOn;
        
            TurnClientRpc(isOn);
        }

        [ClientRpc]
        void TurnClientRpc(bool currentTorchState)
        {
            if (currentTorchState)
            {
                lightSorce.SetActive(true);
                clickSound.Play();
                isOn = true;
            }
            else
            {
                lightSorce.SetActive(false);
                clickSound.Play();
                isOn = false;
            }
        }
    }*/
}
