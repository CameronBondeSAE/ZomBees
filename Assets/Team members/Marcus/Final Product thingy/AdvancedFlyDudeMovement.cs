using System.Collections;
using System.Collections.Generic;
using UnityEditor.Searcher;
using UnityEngine;

namespace Marcus
{
    public class AdvancedFlyDudeMovement : MonoBehaviour
    {
        private float moveSpeed;
        private Rigidbody rb;

        private AttackPlayer sense;
        private FlyDudeAlignment alignment;
        private FlyDudeCohesion cohesion;
        private FlyDudeWandering wandering;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            moveSpeed = GetComponent<FlyDudeStats>().speed;
            
            sense = GetComponent<AttackPlayer>();
            alignment = GetComponent<FlyDudeAlignment>();
            cohesion = GetComponent<FlyDudeCohesion>();
            wandering = GetComponent<FlyDudeWandering>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (sense.tracking && !sense.attacking)
            {
                rb.AddRelativeForce(Vector3.forward * (moveSpeed/10f));
                
                alignment.enabled = false;
                wandering.enabled = false;
            }
            else if (sense.attacking)
            {
                rb.AddRelativeForce(Vector3.forward * (moveSpeed * 2f));

                cohesion.enabled = false;
            }
            else
            {
                rb.AddRelativeForce(Vector3.forward * moveSpeed);

                cohesion.enabled = true;
                alignment.enabled = true;
                wandering.enabled = true;
            }
        }
    }
}
