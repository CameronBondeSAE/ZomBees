using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Lloyd
{

    public class BeeWingsManager : MonoBehaviour
    {
        //ChatGPT cowritten

        //BeeWings Manager spawns a custom number of wings at a custom xydistance apart
        //number is set by whoever is using it. for each numWings, Add to List, which is then Spawned/Deleted with SetWings and DeleteWings
        //pairs are tracked, so that each second wing is flipped horizontally when Instantiated
        //when spawn, attached to an "Anchor" position, which must be set in the prefab
        //there is also a spawned Parent Object which is set as the parent, and then told to follow the current transform pos+rotation
        //that last part pissed me off, hacked solution to BeeWings' local rotation messing with the parent
        //gameObject wing is set by myWing which corresponds to a different model
        //ie Ethan's BeeWingRegular, BeeWingHoles, BeeWingDistorted
        //beeWings are subscribed to a "FlapEvent" which can be called to change the speed, angle, and if they should flap at all thru the Instantiated BeeWing.cs

        public int numberOfWings;

        public GameObject wing;

        public GameObject wingParent;
        private int numWings;
        public float xDistance;
        public float yDistance;
        public float zDistance;

        public GameObject anchorPos;

        public List<GameObject> myWings;

        private List<GameObject> wingObjects;

        private GameObject spawnedWing;

        public GameObject BeeWingRegular;
        public GameObject BeeWingHoles;
        public GameObject BeeWingHolesLarge;
        public GameObject BeeWingDistorted;

        public enum MyWingType
        {
            Random,
            Regular,
            Holes,
            HolesLarge,
            Distorted
        }

        public MyWingType wingType;

        public bool spawned = false;

        public void Update()
        {
            if (wingParent != null)
            {
                wingParent.transform.position = anchorPos.transform.position;
                wingParent.transform.rotation = anchorPos.transform.rotation;
            }
        }

        public void SetWings()
        {
            myWings = new List<GameObject>();
            for (int i = 0; i < numberOfWings; i++)
            {
                myWings.Add(wing);
            }

            SpawnWings();
            OnChangeStatEvent(-90, 15, true);
        }

        [Button("CHANGE ANGLE / SPEED / ISALIVE")]
        public void ChangeBeeWingStats(float newAngle, float newSpeed, bool isAlive)
        {
            OnChangeStatEvent(newAngle, newSpeed, isAlive);
        }

        [Button]
        public void SpawnWings()
        {
            wingObjects = new List<GameObject>();
            wingObjects.Add(BeeWingRegular);
            wingObjects.Add(BeeWingHoles);
            wingObjects.Add(BeeWingDistorted);

            wingParent = new GameObject("BeeWings Parent Anchor") as GameObject;
            wingParent.transform.rotation = anchorPos.transform.rotation;
            wingParent.transform.position = anchorPos.transform.position;

            numWings = myWings.Count;
            int currentPair = -1;
            Vector3 startPosition = transform.position -
                                    new Vector3(((numWings / 2) - 1) * xDistance, 0, ((numWings / 2) - 1) * zDistance);

            float offset = RandomOffset();

            for (int i = 0; i < numWings; i++)
            {
                GameObject newSpawnWing = PickWings();

                int pairIndex = i / 2;

                GameObject newWing = Instantiate(newSpawnWing,
                    startPosition + new Vector3((i % 2 == 0 ? 1 : -1) * xDistance, pairIndex * yDistance,
                        pairIndex * zDistance), Quaternion.Euler(0, 0, 0));
                BeeWing wingScript = newWing.GetComponent<BeeWing>();
                wingScript.randomOffset = offset;
                ChangeStatEvent += wingScript.ChangeWingStats;

                wingScript.pivotTransform = wingParent.transform;

                if (i % 2 == 0)
                {
                    wingScript.SetAsSecondWing();
                    currentPair++;
                }

                wingScript.StartFlapping();

                newWing.transform.SetParent(wingParent.transform, false);
            }

            spawned = true;
        }

        [Button]
        public GameObject PickWings()
        {
            GameObject wingsObj;

            if (wingType == MyWingType.Random)
            {
                int randomIndex = Random.Range(0, wingObjects.Count);
                GameObject randomGameObject = wingObjects[randomIndex];
                wingsObj = randomGameObject;
            }

            else if (wingType == MyWingType.Regular)
            {
                wingsObj = BeeWingRegular;
            }

            else if (wingType == MyWingType.Holes)
            {
                wingsObj = BeeWingHoles;
            }

            else if (wingType == MyWingType.HolesLarge)
            {
                wingsObj = BeeWingHolesLarge;
            }

            else
                wingsObj = BeeWingDistorted;

            return wingsObj;
        }

        public float RandomOffset()
        {
            float newRand = Random.Range(-1f, 1f);
            return newRand;
        }

        [Button]
        public void DeleteWings()
        {
            if (myWings.Any())
            {
                foreach (GameObject deletedWing in myWings)
                {
                    BeeWing wingScript = deletedWing.GetComponent<BeeWing>();
                    ChangeStatEvent -= wingScript.ChangeWingStats;
                }

                if (wingObjects.Any())
                    wingObjects.Clear();

                if (myWings.Any())
                    myWings.Clear();

                if (wingParent != null)
                {
                    DestroyImmediate(wingParent);
                }
            }
        }

        private void OnDisable()
        {
            //DeleteWings();
        }

        public event Action<float, float, bool> ChangeStatEvent;

        public void OnChangeStatEvent(float newAngle, float newSpeed, bool isAlive)
        {
            ChangeStatEvent?.Invoke(newAngle, newSpeed, isAlive);
            //Debug.Log("wingEvent");
        }
    }
}