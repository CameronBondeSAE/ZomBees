using UnityEngine;

namespace Team_members.Lloyd.Civilian_L
{
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
                        Coward,
                        Fighter,
                        Scientist,
                
                        HalfBee,

                        Rescued,
                
                        TurnedIntoBee
                }
                public CivType myType;
        }
}
