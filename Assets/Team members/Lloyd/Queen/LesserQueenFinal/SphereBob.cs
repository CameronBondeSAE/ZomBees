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
            Vector3 prevPosition = transform.localPosition;

            while (bobbing)
            {
                Vector3 randomPoint = Random.onUnitSphere * moveDist;

                float elapsedTime = 0f;
                while (elapsedTime < moveTime)
                {
                    transform.Translate((randomPoint - prevPosition) * Time.deltaTime / moveTime);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                yield return new WaitForSeconds(hangTime);

                elapsedTime = 0f;
                while (elapsedTime < moveTime)
                {
                    // Use Transform.Translate to move locally
                    transform.Translate((origPos - randomPoint) * Time.deltaTime / moveTime);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                yield return new WaitForSeconds(hangTime);

                prevPosition = transform.localPosition;
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
