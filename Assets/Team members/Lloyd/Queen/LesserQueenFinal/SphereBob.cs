using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Lloyd
{
    public class SphereBob : MonoBehaviour
    {
        private Vector3 origPos;
        public float moveDist;
        public float moveTime;
        public float hangTime;

        public bool movingUp;

        public BeeWingsManager beeWings;

        public Rigidbody rb;

        public bool bobbing;

        private Vector3 previousPosition;

        private void OnEnable()
        {
            origPos = transform.position;
            beeWings = GetComponent<BeeWingsManager>();
            rb = GetComponent<Rigidbody>();
            bobbing = true;
            StartCoroutine(Bob());
        }

        private IEnumerator Bob()
        {
            Vector3 prevPosition = transform.position;

            while (bobbing)
            {
                Vector3 randomPoint = Random.onUnitSphere * moveDist + origPos;

                float elapsedTime = 0f;
                while (elapsedTime < moveTime)
                {

                    transform.position = Vector3.Slerp(transform.position, randomPoint, elapsedTime / moveTime);
                    elapsedTime += Time.deltaTime;
                    
                    prevPosition = transform.position;
                    yield return null;
                }

                yield return new WaitForSeconds(hangTime);

                elapsedTime = 0f;
                while (elapsedTime < moveTime)
                {

                    transform.position = Vector3.Slerp(transform.position, origPos, elapsedTime / moveTime);
                    elapsedTime += Time.deltaTime;

                    prevPosition = transform.position;
                    yield return null;
                }

                yield return new WaitForSeconds(hangTime);
            }
        }

        private void OnDisable()
        {
            transform.position = origPos;
            rb.velocity = Vector3.zero;
            bobbing = false;
            StopCoroutine(Bob());
        }
    }
}
