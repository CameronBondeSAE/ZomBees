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

        public bool heardSound;

        public Hearing hearing;

        public List<SoundProperties> sounds;

        private void Start()
        {
            hearing = GetComponent<Hearing>();
            hearing.SoundHeardEvent += HeardSound;
            vision = GetComponent<CivVision>();
        }

        public void HeardSound(SoundProperties sound)
        {
            sounds.Add(sound);
            StartCoroutine(CountdownCoroutine(sound));
        }
        
        private IEnumerator CountdownCoroutine(SoundProperties sound)
        {
            float countdownTime = 10.0f;
            float elapsedTime = 0.0f;

            while (elapsedTime < countdownTime)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            RemoveSound(sound);
        }
        
        public void RemoveSound(SoundProperties sound)
        {
            sounds.Remove(sound);
        }

        private void Update()
        {
            if (vision.civObjects.Any())
            {
                seesCiv = true;
                nearestCiv = vision.ReturnNearestCiv();
            }
            else seesCiv = false;

            if (sounds.Any())
            {
                heardSound = true;
            }
            else heardSound = false;
        }
        
        public Transform ReturnNearestSound()
        {
            if (sounds.Any())
            {
                List<(float, Transform)> distanceAndTransformList = new List<(float, Transform)>();
                foreach (SoundProperties newSound in sounds)
                {
                    float distance = Vector3.Distance(transform.position, newSound.Source.transform.position);
                    distanceAndTransformList.Add((distance, newSound.Source.transform));
                }

                distanceAndTransformList.Sort((a, b) => a.Item1.CompareTo(b.Item1));

                return distanceAndTransformList[0].Item2;
            }

            return null;
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

        private void OnDisable()
        {
            hearing.SoundHeardEvent -= HeardSound;
        }
    }
}