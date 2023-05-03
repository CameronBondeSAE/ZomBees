using System.Collections;
using UnityEngine;

namespace Lloyd
{
    public class GibPartsTimer : MonoBehaviour
    {
        public float waitTime;
    
        public void OnEnable()
        {
            StartCoroutine(Timer());
        }

        public IEnumerator Timer()
        {
            yield return new WaitForSeconds(waitTime);
            DestroyImmediate(gameObject);
        }
    }
}
