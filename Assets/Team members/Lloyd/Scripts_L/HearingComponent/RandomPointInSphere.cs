using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomPointInSphere : MonoBehaviour
{
    public Vector3 GetRandomSpherePointWithNoise(Vector3 position, float maxRadius, float noiseScale)
    {
        Vector3 randomPoint = Random.insideUnitSphere;

        randomPoint *= maxRadius;
        randomPoint += position;

        float noiseValue = Mathf.PerlinNoise(randomPoint.x * noiseScale, randomPoint.y * noiseScale);

        float minNoise = 0f;
        float maxNoise = 1f;
        float scaledNoise = Mathf.Lerp(minNoise, maxNoise, noiseValue);

        float sphereMax = maxRadius;
        float randomRadius = Random.Range(0f, sphereMax);
        Vector3 noiseOffset = randomPoint.normalized * scaledNoise * randomRadius;
        Vector3 finalPoint = randomPoint + noiseOffset;

        return finalPoint;
    }
}