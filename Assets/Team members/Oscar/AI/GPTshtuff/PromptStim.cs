using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oscar
{
    public class PromptStim : MonoBehaviour
    {
        public OscarCivSensor sensor;
        public MemoryManger theMemories;

        public string personality;
        public string outputSpeech;
        public bool announcement;
        public OscarCivSensor.OscarCivilian enumValue;

        private void Awake()
        {
            enumValue = GetComponent<OscarCivSensor.OscarCivilian>();
        }
    }
}

