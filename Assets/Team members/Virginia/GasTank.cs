using System;
using System.Collections;
using System.Collections.Generic;
using Johns;
using UnityEngine;
using Random = System.Random;

namespace Virginia
{

    public class GasTank : MonoBehaviour, IItem
    {
        public int FuelAmount = 0;

        public void Awake()
        {
           // FuelAmount = Random.Range(0, 51);
            
        }
       

        public void OnTriggerEnter(Collider other)
        {
            
                Debug.Log("fuel taken");
          
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
