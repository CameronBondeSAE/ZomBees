using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oscar
{
    public class Vision : MonoBehaviour
    {
        public LittleGuy guy;
        private float distance = 5f;
        public float feelerAmount;

        public float spacing;
        
        public List<Transform> civilGuyInSight;
        
        private void FixedUpdate()
        {
            for (int i = 0; i < feelerAmount; i++)
            {
                Vector3 direction = Quaternion.Euler(0f, i * spacing, 0f) * guy.transform.forward;
                Physics.Raycast(guy.rb.transform.localPosition, direction, out RaycastHit hitInfo, distance, 255,
                    QueryTriggerInteraction.Ignore);
                if (hitInfo.collider == null)
                {
                    continue;
                }
                if (hitInfo.collider.GetComponent<CivilGuy>() != null)
                {
                    Transform CivGuy = hitInfo.transform;
                    
                    if (!civilGuyInSight.Contains(CivGuy))
                    {
                        civilGuyInSight.Add(CivGuy);
                    }
                }
            }

            if (civilGuyInSight.Count > 0)
            {
                if (civilGuyInSight[0] == null)
                {
                    civilGuyInSight.Remove(civilGuyInSight[0]);
                }
            }
            
        }
    }
}
