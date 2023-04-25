using UnityEngine;
    public class LesserQueenLookAt : MonoBehaviour
    {
        public Transform target;
        public float speed = 5f;

        private void Update()
        {
            if(target!=null)
            transform.LookAt(target);
        }
    }