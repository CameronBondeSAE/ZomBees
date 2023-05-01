using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Lloyd
{
    public class BeePartsManager : MonoBehaviour
    {
        //hack script that turns 3D model parts on / off
        //SpawnRandomPart chooses one randomly from the list and turns it on when called
        //if bee eyes are chosen, human eyes are turned off
        //Cure returns to default
        
        public GameObject antannae;
        public GameObject mandibles;
        public GameObject beeEyes;
        public GameObject beeLegs;
        public GameObject humanEyes;

        public List<GameObject> beeParts;

        public void OnEnable()
        {
            beeParts.Add(antannae);
            beeParts.Add(mandibles);
            beeParts.Add(beeLegs);
        }

        public void SpawnRandomPart()
        {
            if (beeParts.Count > 0)
            {
                int randomIndex = Random.Range(0, beeParts.Count);

                GameObject randomGameObject = beeParts[randomIndex];
               
                if (randomGameObject == beeEyes)
                    BeeEyes(); 
                
                randomGameObject.SetActive(true);

                beeParts.RemoveAt(randomIndex);
            }
        }

        public void Cure()
        {
            beeParts.Clear();
            beeParts.Add(mandibles);
            beeParts.Add(antannae);
            beeParts.Add(beeLegs);
            
            HumanEyes();
            LoseAntannae();
            LoseMandibles();
            LoseLegs();
        }
        
        //test buttons

        [Button]
        public void BeeEyes()
        {
            humanEyes.SetActive(false);
            beeEyes.SetActive(true);
        }

        [Button]
        public void HumanEyes()
        {
            humanEyes.SetActive(true);
            beeEyes.SetActive(false);
        }

        [Button]
        public void GrowAntannae()
        {
            antannae.SetActive(true);
        }

        [Button]
        public void LoseAntannae()
        {
            antannae.SetActive(false);
        }

        [Button]
        public void GrowMandibles()
        {
            mandibles.SetActive(true);
        }

        [Button]
        public void LoseMandibles()
        {
            mandibles.SetActive(false);
        }
        
        [Button]
        public void GrowLegs()
        {
            beeLegs.SetActive(true);
        }

        [Button]
        public void LoseLegs()
        {
            beeLegs.SetActive(false);
        }
    }
}
