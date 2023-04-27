using UnityEngine;

namespace Lloyd
{
    public class LesserQueenLookAt : MonoBehaviour
    {
        public Transform target;
        public float speed = 5f;

        public Vector3 targetVector;

        private void Update()
        {
            if(target)
                transform.LookAt(target);
            
            else
                transform.LookAt(targetVector);
        }
    }
}