using System.Collections;
using UnityEngine;

    public class SphereBobRB : MonoBehaviour
    {
        private Vector3 origPos;
        public float moveDist;
        public float moveTime;
        public float hangTime;

        public bool movingUp;
        
        public BeeWingsManager beeWings;

        public Rigidbody rb;

        private bool bobbing;

        private Vector3 previousPosition;

        private void OnEnable()
        {
            origPos = transform.position;
            beeWings = GetComponent<BeeWingsManager>();
            rb = GetComponent<Rigidbody>();
            bobbing = true;
            StartCoroutine(Bob());
        }
        private void Update()
        {
            if (bobbing)
            {
                Vector3 velocity = rb.velocity;

                if (velocity.y > 0)
                {
                    beeWings.OnChangeStatEvent(-125, 50, true);
                    movingUp = true;
                }
                else
                {
                    beeWings.OnChangeStatEvent(-145, 2, true);
                    movingUp = false;
                }
                
            }
        }
    
        private IEnumerator Bob()
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            Vector3 origPos = rb.position;
            Vector3 prevPosition = rb.position;

            while (true)
            {
                Vector3 randomPoint = Random.onUnitSphere * moveDist + origPos;

                float elapsedTime = 0f;
                while (elapsedTime < moveTime)
                {
                    Vector3 newPosition = Vector3.Slerp(rb.position, randomPoint, elapsedTime / moveTime);
                    Vector3 velocity = (newPosition - prevPosition) / Time.deltaTime;
                    rb.AddForce(velocity, ForceMode.VelocityChange);
                    elapsedTime += Time.deltaTime;
                    prevPosition = rb.position;
                    yield return null;
                }

                yield return new WaitForSeconds(hangTime);

                elapsedTime = 0f;
                while (elapsedTime < moveTime)
                {
                    Vector3 newPosition = Vector3.Slerp(rb.position, origPos, elapsedTime / moveTime);
                    Vector3 velocity = (newPosition - prevPosition) / Time.deltaTime;
                    rb.AddForce(velocity, ForceMode.VelocityChange);
                    elapsedTime += Time.deltaTime;
                    prevPosition = rb.position;
                    yield return null;
                }

                yield return new WaitForSeconds(hangTime);
            }
        }
    }
