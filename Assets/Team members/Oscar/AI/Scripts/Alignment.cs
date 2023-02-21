using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oscar
{
    public class Alignment : MonoBehaviour
    {
        public Oscar.Neighbours neighbours;

        public float force;

        void FixedUpdate()
        {
            Vector3 targetDir = neighbours.friendsList[1].transform.position - neighbours.friendsList[0].transform.position;
            Vector3 newUp = Vector3.Cross(transform.forward, targetDir);
            Quaternion targetRotation = Quaternion.LookRotation(targetDir * force, newUp);
    
            for (int i = 0; i < neighbours.friendsList.Count; i++)
            {
                neighbours.friendsList[i].transform.rotation = targetRotation;
            }
        }

       
    }
}
