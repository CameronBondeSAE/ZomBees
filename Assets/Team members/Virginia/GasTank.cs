using System;
using System.Collections;
using System.Collections.Generic;
using Johns;
using UnityEngine;

namespace Virginia
{

    public class GasTank : MonoBehaviour, IItem
    {
        

        public void OnTriggerEnter(Collider other)
        {
            if(GetComponent<GeneratorModel>()!= null)
            {
                Debug.Log("fuel taken");
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
            throw new NotImplementedException();
        }

        public void Pickup(GameObject whoPickedMeUp)
        {
            throw new NotImplementedException();
        }
    }

      
    }
