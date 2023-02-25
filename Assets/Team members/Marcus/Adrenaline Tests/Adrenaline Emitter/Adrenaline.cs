using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Marcus
{
    public class Adrenaline : MonoBehaviour
    {
        [ReadOnly] [SerializeField] 
        private float adrenaline;

        public Neighbours neighbours;
        public CivilianStats myStats;

        private List<Transform> neighbourList;

        private float timeAlone;
        private int aloneRoundRobin;
        private int groupedRoundRobin;

        // Start is called before the first frame update
        void Start()
        {
            adrenaline = myStats.baseAdrenaline;
            neighbourList = neighbours.neighbours;
        }

        // Update is called once per frame
        void Update()
        {
            if (neighbourList.Count <= 0 && aloneRoundRobin >= 10)
            {
                timeAlone += Time.deltaTime;
                adrenaline += timeAlone;

                aloneRoundRobin = 0;
            }
            else
            {
                aloneRoundRobin++;
            }
            
            foreach (Transform civs in neighbourList)
            {
                timeAlone = 0f;

                if (groupedRoundRobin >= 10 * neighbourList.Count)
                {
                    adrenaline -= 0.3f;

                    groupedRoundRobin = 0;
                }
                else
                {
                    groupedRoundRobin++;
                }
            }
            
            adrenaline = Mathf.Clamp(adrenaline, 0, 10);
            Collider[] smellers = Physics.OverlapSphere(transform.position, adrenaline * 2);
            
            foreach (Collider item in smellers)
            {
               GiveTarget(item);
            }
        }

        void GiveTarget(Collider item)
        {
            Vector3 randomness = new Vector3(Random.Range(10f, 20f), 0, Random.Range(10f, 20f));
                
            if (item.gameObject.GetComponent<FearSense>() != null)
            {
                //Pass my position with some randomness
                item.gameObject.GetComponent<FearSense>().targetPos = transform.position + randomness;
            }
        }
    }
}
