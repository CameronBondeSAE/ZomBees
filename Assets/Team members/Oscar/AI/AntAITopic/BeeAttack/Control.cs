using System.Collections;
using System.Collections.Generic;
using Oscar;
using UnityEngine;

namespace Oscar
{
    public class Control : MonoBehaviour
    {
        
        public Vision vision;

        public bool CanSeeCivilGuy()
        {
            print(vision.civilGuyInSight.Count > 0);
            return vision.civilGuyInSight.Count > 0;
        }
    }
}
