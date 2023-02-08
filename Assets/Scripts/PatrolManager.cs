using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZachFrench
{
    
    public class PatrolManager : MonoBehaviour
    {
        public List<PatrolPoint> paths;
        public List<PatrolPoint> pathsWithIndoors;
        public List<PatrolPoint> indoors;
        public List<PatrolPoint> sneaky;
        public List<PatrolPoint> waterTargets;
    }
}
