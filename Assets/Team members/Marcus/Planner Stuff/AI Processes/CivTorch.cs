using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace Marcus
{
    public class CivTorch : MonoBehaviour
    {
        public bool torchOn = true;
        public Light torch;

        // Update is called once per frame
        void Update()
        {
            if (torchOn)
            {
                torch.enabled = true;
            }
            else
            {
                torch.enabled = false;
            }

            if (torch.enabled)
            {
                torchOn = true;
            }
            else
            {
                torchOn = false;
            }
        }
    }
}
