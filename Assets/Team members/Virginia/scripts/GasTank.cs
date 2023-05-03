using System;
using System.Collections;
using System.Collections.Generic;
using Johns;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


namespace Virginia
{

    public class GasTank : MonoBehaviour, IItem
    {
        public int fuelAmount;
        public GameObject fuelcan;
        public void Awake()
        {
            fuelAmount = Random.Range(1, 51);
           
        }
       

        public void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<GeneratorModel>() != null)
            {
               // Debug.Log("fuel taken");
               Destroy(fuelcan);
            }
        }

        public void Consume()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public string Description()
        {
            return ("fuel");
        }

        public void Pickup(GameObject whoPickedMeUp)
        {
            throw new NotImplementedException();
        }
    }

      
    }
