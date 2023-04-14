using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Lloyd;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : AntAIState
{
    // Spawner instantiates GameObject at random 360 direction
    // for amount numSwarmers every second waitTime
    // ChatGPT was here
    
    private int defaultSwarmers;
    public GameObject swarmer;

    public float waitTime;

    public List<GameObject> FollowerList;

    private GameObject parent;

    public QueenEvent queenEvent;

    public QueenScenarioManager queenScene;

    public Rigidbody rb;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        queenScene = aGameObject.GetComponent<QueenScenarioManager>();
        
        rb = queenScene.GetComponent<Rigidbody>();

        parent = new GameObject();
        parent.name = "Swarmer Parent";
        parent.transform.position = queenScene.transform.position;
        parent.transform.rotation = queenScene.transform.rotation;

        queenEvent = GetComponentInParent<QueenEvent>();

        defaultSwarmers = queenScene.numSwarmers;
    }

    public override void Enter()
    {
        RefillFollowers();
    }
    
    public void StartSpawner(int numFollowers)
    {
        defaultSwarmers = numFollowers;
        StartCoroutine(SpawnFollower(numFollowers));
    }

    private IEnumerator SpawnFollower(int numberOfFollowersToSpawn)
    {
        for (int i = 0; i < numberOfFollowersToSpawn; i++)
        {
            yield return new WaitForSeconds(waitTime);

            Vector3 position = transform.position;
            int randomAngle = Random.Range(0, 360);
            randomAngle = Mathf.RoundToInt(randomAngle);
            Quaternion rotation = Quaternion.Euler(transform.rotation.x + randomAngle,
                transform.rotation.y + randomAngle, transform.rotation.z + randomAngle);
            GameObject swarmerObj = Instantiate(swarmer, position, rotation) as GameObject;

            Follower follower;
            follower = swarmerObj.GetComponent<Follower>();
            follower.SetRotationPoint(transform);

            queenScene.AddFollower(follower);

            queenEvent.ChangeSwarmTransform += follower.SetRotationPoint;
            queenEvent.ChangeSwarmCircleSize += follower.SetCircleSize;

            queenEvent.OnChangeSwarmPoint(swarmer.transform);

            follower.Begin();

            swarmerObj.transform.SetParent(parent.transform);

            FollowerList.Add(swarmerObj);
        }
        queenScene.fullFollowers = true;
        queenScene.hasArrived = false;
    }

    public void RefillFollowers()
    {
        queenScene.fullFollowers = false;
        int newAmount = defaultSwarmers - FollowerList.Count;
        StartCoroutine(SpawnFollower(newAmount));
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void FixedUpdate()
    {
        parent.transform.position = rb.position;
        parent.transform.rotation = rb.rotation;
    }
}