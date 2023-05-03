using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class BeeGib : MonoBehaviour
{
    //quick hack
    //BeeGib has references to antannae, mandibles, beelegs, wings
    
    public GameObject antannae;
    public GameObject mandibles;
    public GameObject beeLegs;
    public GameObject beeWings;
    //beewings

    public List<GameObject> beeParts;
    public List<GameObject> objectsToSpawn;

    public float gibVerticalForce;
    public float gibSpinForce;

    public void OnEnable()
    {
        beeParts.Add(antannae);
        beeParts.Add(mandibles);
        beeParts.Add(beeWings);
        beeParts.Add(beeLegs);
    }

    public enum BeeType
    {
        Small,
        Medium,
        Large
    };

    public BeeType myType;

    [Button]
    public void DetermineGib(BeeType type)
    {
        objectsToSpawn.Clear();

        if (type == BeeType.Small)
        {
            objectsToSpawn.Add(beeParts[Random.Range(0, beeParts.Count)]);
            GibBee(objectsToSpawn);
        }

        else if (type == BeeType.Medium)
        {
            GameObject randObj = beeParts[Random.Range(0, beeParts.Count)];
            objectsToSpawn.Add(randObj);
            
            GameObject secondRandObj;
            do
            {
                secondRandObj = beeParts[Random.Range(0, beeParts.Count)];
            }
            while (secondRandObj == randObj);

            objectsToSpawn.Add(secondRandObj);
            GibBee(objectsToSpawn);
        }

        else
        {
            GibBee(beeParts);
            GibBee(beeParts);
        }
    }

    public void GibBee(List<GameObject> gibObjs)
    {
        foreach (GameObject beePart in gibObjs)
        {
            GameObject newObj = Instantiate (beePart, transform.position, Quaternion.identity) as GameObject;
            Rigidbody newRb = newObj.GetComponent<Rigidbody>();
            if (newRb != null)
            {
                newRb.AddForce(Vector3.up*gibVerticalForce, ForceMode.Impulse);
                
                float random = Random.Range(0f, 1f);
                float x = (random < 0.5f) ? gibSpinForce : -gibSpinForce;
                random = Random.Range(0f, 1f);
                float y = (random < 0.5f) ? gibSpinForce : -gibSpinForce;
                random = Random.Range(0f, 1f);
                float z = (random < 0.5f) ? gibSpinForce : -gibSpinForce;

                Vector3 torque = new Vector3(x, y, z);
                newRb.AddTorque(torque);
            }
        }
    }
}