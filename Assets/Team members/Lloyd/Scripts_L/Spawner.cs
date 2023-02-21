using System;
using System.Collections;
using System.Collections.Generic;
using Lloyd;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    // Spawner instantiates GameObject at random 360 direction
    // for amount numSwarmers every second waitTime
    // 
    public int numSwarmers;
    public GameObject swarmer;

    public float waitTime;

    public List<GameObject> FollowerList;

    private GameObject parent;

    public QueenEvent queenEvent;

    private void Start()
    {
        parent = new GameObject();

        queenEvent = GetComponent<QueenEvent>();
        
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
            Quaternion rotation = Quaternion.Euler(transform.rotation.x + randomAngle,transform.rotation.y + randomAngle,transform.rotation.z + randomAngle);
            GameObject swarmerObj = Instantiate(swarmer, position, rotation) as GameObject;

            Follower follower;
            follower = swarmerObj.GetComponent<Follower>();
            follower.SetRotationPoint(transform);

            queenEvent.ChangeSwarmTransform += swarmerObj.GetComponent<Follower>().SetRotationPoint;

            follower.Begin();

            FollowerLookTowards turn;
            turn = swarmerObj.GetComponent<FollowerLookTowards>();
            turn.SetTarget(transform);

            swarmerObj.transform.SetParent(parent.transform);

            FollowerList.Add(swarmerObj);
        }
    }
}