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

    public QueenScenarioManager queenScene;

    private void Start()
    {
        parent = new GameObject();
        parent.name = "Swarmer Parent";

        queenEvent = GetComponent<QueenEvent>();

        queenScene = GetComponent<QueenScenarioManager>();

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
            
            queenScene.AddFollower(swarmerObj);

            queenEvent.ChangeSwarmTransform += follower.SetRotationPoint;
            queenEvent.ChangeSwarmCircleSize += follower.SetCircleSize;
            
            queenEvent.OnChangeSwarmPoint(swarmer.transform);

            follower.Begin();

            swarmerObj.transform.SetParent(parent.transform);

            FollowerList.Add(swarmerObj);
        }
    }
}