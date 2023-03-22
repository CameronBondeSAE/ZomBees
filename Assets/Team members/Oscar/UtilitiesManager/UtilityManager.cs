using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oscar
{
    public class UtilityManager : MonoBehaviour
    {
        public static UtilityManager instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        public static void DeleteAfterDelay(GameObject obj)
        {
            obj.transform.position = new Vector3(40, 30, 30);

            instance.StartCoroutine(deleteCoroutine(obj));
        }

        private static IEnumerator deleteCoroutine(GameObject gObj)
        {
            yield return new WaitForSeconds(3f);
            
            Destroy(gObj);
        }
    }

}