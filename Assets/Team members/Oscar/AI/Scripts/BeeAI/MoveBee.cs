using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBee : MonoBehaviour
{
    public BeeGuy guy;
    void Update()
    {
        guy.rbee.AddRelativeForce(Vector3.forward * guy.speed,ForceMode.Acceleration);
    }
}
