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

        public static void DisableAfterDelay(GameObject obj, GameObject caller)
        {
            obj.transform.position = new Vector3(100000000000, 100000000000, 100000000000);

            instance.StartCoroutine(DisableCoroutine(obj, caller));
        }

        private static IEnumerator DisableCoroutine(GameObject obj, GameObject caller)
        {
            yield return new WaitForSeconds(.1f);

            obj.GetComponent<Collider>().enabled = false;

            yield return new WaitForSeconds(.1f);

            obj.transform.position = caller.transform.position;
        }

        public static void EnableAfterDelay(GameObject obj)
        {
            instance.StartCoroutine(EnableCoroutine(obj));
        }

        private static IEnumerator EnableCoroutine(GameObject obj)
        {
            yield return new WaitForSeconds(.1f);

            obj.GetComponent<Collider>().enabled = true;
        }

        public static void DeleteAfterDelay(GameObject obj)
        {
            obj.transform.position = new Vector3(100000000000, 100000000000, 100000000000);

            instance.StartCoroutine(DeleteCoroutine(obj));
        }

        private static IEnumerator DeleteCoroutine(GameObject obj)
        {
            yield return new WaitForSeconds(.1f);

            Destroy(obj);
        }
    }
}