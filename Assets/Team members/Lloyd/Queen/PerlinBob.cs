using Marcus;
using UnityEngine;

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

    private void Start()
    {
        queenScene = GetComponent<QueenScenarioManager>();
        beeWings = queenScene.beeWings.GetComponent<BeeWingsManager>();
        rigidbody = queenScene.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 randomDirection = Random.insideUnitSphere.normalized;
        float perlinValue = Mathf.PerlinNoise(Time.time * frequency, 0f) * 2f - 1f;
        Vector3 forceDirection = randomDirection * perlinValue * forceMagnitude;
        rigidbody.AddForce(forceDirection, ForceMode.Force);
    }

    private void Update()
    {
        Vector3 randomOffset = new Vector3(
            Mathf.PerlinNoise(Time.time * frequency, 1f) * 2f - 1f,
            Mathf.PerlinNoise(Time.time * frequency, 2f) * 2f - 1f,
            Mathf.PerlinNoise(Time.time * frequency, 3f) * 2f - 1f
        ) * scale;
        transform.position = transform.position + randomOffset + sphereRadius * new Vector3(
            Mathf.Cos(Time.time),
            0f,
            Mathf.Sin(Time.time)
        );

        float currentYPosition = transform.position.y;
        if (currentYPosition > previousYPosition)
        {
            beeWings.OnChangeStatEvent(-90, upFlapSpeed, true);
            //Debug.Log("going up");
        }
        else if (currentYPosition < previousYPosition)
        {
            beeWings.OnChangeStatEvent(-90, downFlapSpeed, true);
            //Debug.Log("going down");
        }

    }
}