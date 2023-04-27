using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Oscar;

namespace Lloyd
{
    public class BeeStingerBrain : MonoBehaviour
    {
        private CivVision vision;

        public Transform nearestCiv;

        public bool seesCiv;

        private void Start()
        {
            vision = GetComponent<CivVision>();
        }

        private void Update()
        {
            if (vision.civObjects.Any())
            {
                seesCiv = true;
                nearestCiv = vision.ReturnNearestCiv();
            }
            else seesCiv = false;
        }
        
        public Transform ReturnNearestCiv()
        {
            if (vision.civObjects.Any())
            {
                List<(float, Transform)> distanceAndTransformList = new List<(float, Transform)>();
                foreach (GameObject civ in vision.civObjects)
                {
                    float distance = Vector3.Distance(transform.position, civ.transform.position);
                    distanceAndTransformList.Add((distance, civ.transform));
                }

                distanceAndTransformList.Sort((a, b) => a.Item1.CompareTo(b.Item1));

                return distanceAndTransformList[0].Item2;
            }
            return null;
        }
    }
}
