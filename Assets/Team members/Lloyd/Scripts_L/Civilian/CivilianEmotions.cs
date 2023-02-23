using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianEmotions : MonoBehaviour
{
        public int speakingVolume;

        public int panicLevel;
        public int panicThreshold;
        public bool isPanicked;

        public float beeness;
        public float beeThreshold;

        public enum CivType
        {
                Neutral,
                Fighter,
                Scientist,
                HalfBee
        }
        public CivType myType;
}
