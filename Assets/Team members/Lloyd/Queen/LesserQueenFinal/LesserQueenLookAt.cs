using UnityEngine;

namespace Team_members.Lloyd.Queen.QueenFinal
{
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
}