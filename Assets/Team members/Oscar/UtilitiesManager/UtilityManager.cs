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
                gameObject.SetActive(false);
            }
        }

        public static void DisableAfterDelay(GameObject obj)
        {
            obj.transform.position = new Vector3(40, 30, 30);

            instance.StartCoroutine(DisableCoroutine(obj));
        }

        private static IEnumerator DisableCoroutine(GameObject gObj)
        {
            yield return new WaitForSeconds(1f);

            gObj.SetActive(false);
        }

        public static void DeleteAfterDelay(GameObject obj)
        {
            obj.transform.position = new Vector3(40, 30, 30);

            instance.StartCoroutine(DeleteCoroutine(obj));
        }

        private static IEnumerator DeleteCoroutine(GameObject gObj)
        {
            yield return new WaitForSeconds(3f);
            
            Destroy(gObj);
        }
    }

}