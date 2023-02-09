using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.Serialization;

namespace ZachFrench
{
    
    public class PatrolManager : MonoBehaviour
    {
        public List<PatrolPoint> paths;
        public List<PatrolPoint> pathsWithIndoors;
        public List<PatrolPoint> indoors;
        public List<PatrolPoint> sneaky;
        public List<PatrolPoint> waterTargets;

        public static PatrolManager singleton;


        [FormerlySerializedAs("networkTransport")]
        public UnityTransport unityTransport;
        
        void Awake()
        {
            singleton = this;

            // NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = ipString;
        }
    }
}
