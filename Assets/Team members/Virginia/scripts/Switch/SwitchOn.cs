using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Virginia;
namespace Virginia
{
    public class SwitchOn : VStateBase
    {
        public Switch Switch;
    
        void OnEnable()
        {
            
            //TurnOnEvent?.Invoke();
            Switch.ThingToSwitch?.TurnOn();
           transform.DOLocalMove(new Vector3(-2, 1, -7), 1f);
            GetComponent<Renderer>().material.color = Color.green;
        }

      
        void OnDisable()
        {
            Debug.Log("light is off");
        }
    }
}
