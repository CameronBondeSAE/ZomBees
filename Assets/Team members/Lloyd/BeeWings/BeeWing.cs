using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class BeeWing : MonoBehaviour
{
    //finally hammered out with ChatGPT
    
    //BeeWing rotates around a point using Slerp Rotate on the Transform
    
    //it rotates to flapAngle as fast as flapSpeed. when it reaches within a min dist, it reverses to its original rotation indefinitely
    
    //the bot wants rotationSpeed to be seperate but I can't tell what it does. so just leave it at 1

    public Transform pivotTransform;

    public Transform beeWingTransform;
    public float flapToAngle;
    private float rotationSpeed=1;
    public float lerpFlapSpeed;
    public float flapWaitTime;

    public bool secondWing = false;

    private Quaternion targetRotation;
    private Quaternion initialRotation;
    private Quaternion prevTargetRotation;
    private float lerpValue;

    public Vector3 customAxis = new Vector3(0,0,1);

    public bool returning;

    public bool flapping;

    //perlin for angle

    [SerializeField] private float perlinScale = 5f;

    public float randomOffset;

    [Button]
    public void StartFlapping()
    {
        initialRotation = beeWingTransform.localRotation;

        targetRotation = Quaternion.Euler(0f, 0, flapToAngle);

        flapping = true;
        StartCoroutine(FlapWings());
    }

    public void SetAsSecondWing()
    {
        secondWing = true;
    }

    private void Update()
    {
        if (flapping)
        {
            float input = randomOffset + (Time.time * perlinScale);

            float perlinValue = Mathf.PerlinNoise(input, 0f);

            float offset = (randomOffset * perlinValue) * Time.deltaTime;

            //flapToAngle += offset;
            
            ChangeWingStats(flapToAngle, lerpFlapSpeed, true);
        }
    }

    private IEnumerator FlapWings()
    {
        while (flapping)
        {
            if (returning)
            {
                beeWingTransform.localRotation = Quaternion.Lerp(targetRotation, initialRotation, lerpValue);
                customAxis = -customAxis;
            }
            else
            {
                beeWingTransform.localRotation = Quaternion.Slerp(initialRotation, targetRotation, lerpValue);
                customAxis = new Vector3(Mathf.Abs(customAxis.x), Mathf.Abs(customAxis.y), Mathf.Abs(customAxis.z));
            }

            lerpValue += Time.deltaTime * lerpFlapSpeed;
            lerpValue = Mathf.Clamp01(lerpValue);

            if (lerpValue >= 1f)
            {
                lerpValue = 0f;
                returning = !returning;
            }
            
            Vector3 pivotOffset = pivotTransform.InverseTransformDirection(Vector3.down * beeWingTransform.localScale.y / 2f) *
                                  pivotTransform.localScale.y * 0.5f;

            Vector3 pivotPoint = pivotTransform.position + pivotTransform.TransformDirection(pivotOffset);
            
            beeWingTransform.RotateAround(pivotPoint, pivotTransform.TransformDirection(Vector3.down), Time.deltaTime * rotationSpeed * Mathf.Sign(transform.localScale.x));

            yield return new WaitForSeconds(flapWaitTime);

            yield return new WaitForSeconds(flapWaitTime);
        }
    }

    private IEnumerator Dying()
    {
        int time = 10;
        while (true)
        {
            time--;
            lerpFlapSpeed--;
            if (time > 0)
                yield return new WaitForSeconds(1);

            else
            {
                flapping = false;
                yield return new WaitForSeconds(1);
            }
        }
    }

    [Button]
    public void ChangeWingStats(float newAngle, float newSpeed, bool isAlive)
    {
        flapToAngle = newAngle;

        if (!secondWing)
            newAngle = -newAngle;

        lerpFlapSpeed = newSpeed;

        targetRotation = Quaternion.Euler(0,0,newAngle);
        if (isAlive == false)
        {
            StartCoroutine(Dying());
        }
    }
}