using System;
using System.Collections;
using System.Collections.Generic;
using Lloyd;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public int numSwarmers;
    public GameObject swarmer;

    public float waitTime;

    public List<GameObject> FollowerList;

    private GameObject parent;

    private void Start()
    {
        parent = new GameObject();
        
        StartCoroutine(SpawnSwarmer());
    }
    private IEnumerator SpawnSwarmer()
    {
        for (int i = 0; i < numSwarmers; i++)
        {
            yield return new WaitForSeconds(waitTime);
            
            Vector3 position = transform.position;
            int randomAngle = Random.Range(0, 360);
            randomAngle = Mathf.RoundToInt(randomAngle);
            Quaternion rotation = Quaternion.Euler(0, randomAngle, 0);
            GameObject swarmerObj = Instantiate(swarmer, position, rotation) as GameObject;

            Follower follower;
            follower = swarmerObj.GetComponent<Follower>();
            follower.SetRotationPoint(transform);
            follower.Begin();

            FollowerLookTowards turn;
            turn = swarmerObj.GetComponent<FollowerLookTowards>();
            turn.SetTarget(transform);

            swarmerObj.transform.SetParent(parent.transform);

            FollowerList.Add(swarmerObj);
        }
    }
}