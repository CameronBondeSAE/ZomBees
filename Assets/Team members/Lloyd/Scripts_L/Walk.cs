using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lloyd;

public class Walk : MonoBehaviour
{
    private Look look;

    public bool wallSpotted;

    public float walkSpeed;

    public float turnSpeed;

    public float turnTime;

    private Rigidbody rb;

    private bool turning;

    public LayerMask NPCLayer;

    private void Start()
    {
        Physics.IgnoreLayerCollision(NPCLayer, NPCLayer, true);

        look = GetComponent<Look>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        /*wallSpotted = look.wallSpotted;
        if(!wallSpotted)
            Walking();

        else
        {
            if (!turning)
                StartCoroutine(Turn());
        }*/
        Walking();
    }

    private void Walking()
    {
        rb.angularVelocity = Vector3.zero;
        rb.AddRelativeForce(new Vector3(0,0,1f) * walkSpeed);
    }

    private IEnumerator Turn()
    {
        turning = true;
        rb.velocity = Vector3.zero;
        Vector3 rotationDirection = Random.Range(0, 2) == 0 ? Vector3.up : Vector3.down;
        rb.AddTorque(rotationDirection * turnSpeed, ForceMode.Acceleration);

        yield return new WaitForSeconds(turnTime);

        if (wallSpotted)
            StartCoroutine(Turn());
        else
        {
            turning = false;
        }
    }
}
