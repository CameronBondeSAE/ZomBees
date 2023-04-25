using Marcus;
using UnityEngine;

namespace Lloyd
{

    public class PerlinBob : MonoBehaviour
    {
        //bobs in a gentle perlin pattern
        // perlin picks random points within a sphere

        public QueenScenarioManager queenScene;
        public BeeWingsManager beeWings;

        public float forceMagnitude = 1f;
        public float frequency = 1f;
        public float scale = 1f;
        public float sphereRadius = 1f;

        public Rigidbody rigidbody;

        private float previousYPosition;

        public float upFlapSpeed;
        public float downFlapSpeed;

        private Vector3 randomOffset;

        private void OnEnable()
        {
            //queenScene = GetComponent<QueenScenarioManager>();
            //beeWings = GetComponentInChildren<BeeWingsManager>();
            rigidbody = GetComponent<Rigidbody>();

            //beeWings.SpawnWings();

            //beeWings.ChangeBeeWingStats(-90, 15, true);
        }

        private void FixedUpdate()
        {
            Vector3 randomDirection = Random.insideUnitSphere.normalized;
            float perlinValue = Mathf.PerlinNoise(Time.time * frequency, 0f) * 2f - 1f;
            Vector3 forceDirection = randomDirection * perlinValue * forceMagnitude;
            rigidbody.AddForce(forceDirection, ForceMode.Acceleration);

            // Calculate random offset
            randomOffset = new Vector3(
                Mathf.PerlinNoise(Time.time * frequency, 1f) * 2f - 1f,
                Mathf.PerlinNoise(Time.time * frequency, 2f) * 2f - 1f,
                Mathf.PerlinNoise(Time.time * frequency, 3f) * 2f - 1f
            ) * scale;
        }

        private void Update()
        {
            float currentYPosition = transform.position.y;
            if (currentYPosition > previousYPosition)
            {
//            beeWings.OnChangeStatEvent(-90, upFlapSpeed, true);
                //Debug.Log("going up");
            }
            else if (currentYPosition < previousYPosition)
            {
                //          beeWings.OnChangeStatEvent(-90, downFlapSpeed, true);
                //Debug.Log("going down");
            }

            previousYPosition = currentYPosition;
        }

        private void LateUpdate()
        {
            // Move rigidbody using random offset
            rigidbody.MovePosition(transform.position + randomOffset + sphereRadius * new Vector3(
                Mathf.Cos(Time.time),
                0f,
                Mathf.Sin(Time.time)
            ));
        }
    }
}