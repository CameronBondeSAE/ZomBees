using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Lloyd
{


    public class LesserQueenSpawnFollowers : AntAIState
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

        public LesserQueenSensor queenSensor;

        public Rigidbody rb;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            queenSensor = aGameObject.GetComponent<LesserQueenSensor>();
            queenEvent = aGameObject.GetComponent<QueenEvent>();

            rb = aGameObject.GetComponent<Rigidbody>();

            parent = new GameObject();
            parent.name = "Swarmer Parent";
        }

        public override void Enter()
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            
            parent.transform.position = queenSensor.transform.position;
            parent.transform.rotation = queenSensor.transform.rotation;

            defaultSwarmers = queenSensor.numSwarmers;

            RefillFollowers();
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
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

                queenSensor.AddFollower(follower);

                queenEvent.ChangeSwarmTransform += follower.SetRotationPoint;
                queenEvent.ChangeSwarmCircleSize += follower.SetCircleSize;
                queenEvent.ChangeQueenStateEvent += follower.ChangeQueenState;


                follower.Begin();

                swarmerObj.transform.SetParent(parent.transform);

                FollowerList.Add(swarmerObj);
            }
            queenEvent.OnChangeSwarmPoint(swarmer.transform);

            queenSensor.spawnFollowers = false;
            queenSensor.patrol = true;
            Finish();
        }

        public void RefillFollowers()
        {
            int newAmount = defaultSwarmers - FollowerList.Count;
            StartCoroutine(SpawnFollower(newAmount));
        }

        public override void Exit()
        {
            rb.constraints = RigidbodyConstraints.None;
            queenSensor.spawnFollowers = true;
        }

        private void FixedUpdate()
        {
            parent.transform.position = rb.position;
            parent.transform.rotation = rb.rotation;
        }
    }
}