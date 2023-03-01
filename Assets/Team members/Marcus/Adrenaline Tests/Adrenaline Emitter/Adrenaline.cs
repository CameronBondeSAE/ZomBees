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
        
        /// <summary>
        /// Multipler to determine the range of adrenaline sphere
        /// Higher potency means larger range for bees to smell
        /// </summary>
        public float adrenalinePotency;

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
            float adrenalineRange = adrenaline * adrenalinePotency;
            
            foreach (Collider item in Physics.OverlapSphere(transform.position, adrenalineRange))
            {
                IAdrenalineSensitive bee = item.GetComponent<IAdrenalineSensitive>();
                if (bee != null)
                {
                    bee.PathfindToSource(transform.position + 
                                         new Vector3(Random.Range(-adrenalineRange,adrenalineRange), 0, Random.Range(-adrenalineRange, adrenalineRange)));
                }
            }
        }
    }
}
