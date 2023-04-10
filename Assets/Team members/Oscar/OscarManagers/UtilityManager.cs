using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oscar
{
    public class UtilityManager : MonoBehaviour
    {
        public static UtilityManager instance;

        public static Hive myBase;
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
            
            myBase = FindObjectOfType<Hive>();
        }

        public static void DisableAfterDelay(GameObject obj)
        {
            obj.transform.position = new Vector3(myBase.transform.position.x + 13,20f,myBase.transform.position.z + 13)  /*new Vector3(40, 30, 30)*/;

            instance.StartCoroutine(DisableCoroutine(obj));
        }

        private static IEnumerator DisableCoroutine(GameObject obj)
        {
            yield return new WaitForSeconds(1f);

            obj.SetActive(false);
        }

        public static void DeleteAfterDelay(GameObject obj)
        {
            obj.transform.position = new Vector3(40, 30, 30);

            instance.StartCoroutine(DeleteCoroutine(obj));
        }

        private static IEnumerator DeleteCoroutine(GameObject obj)
        {
            yield return new WaitForSeconds(3f);
            
            Destroy(obj);
        }

        public static void EnableAfterDelay(GameObject obj)
        {
            instance.StartCoroutine(EnableCoroutine(obj));
        }
        
        private static IEnumerator EnableCoroutine(GameObject obj)
        {
            yield return new WaitForSeconds(1f);

            obj.SetActive(true);
        }
    }

}