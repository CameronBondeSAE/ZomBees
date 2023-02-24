using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class QueenSight : AntAIState
{
    // "sight"
    public float radius;

    private SphereCollider sphereCollider;

    private GameObject playerObj;
    
    public override void Enter()
    {
        void SightRadius(Vector3 center, float radius)
        {
            int maxColliders = 10;
            Collider[] hitColliders = new Collider[maxColliders];
            int numColliders = Physics.OverlapSphereNonAlloc(center, radius, hitColliders);
            for (int i = 0; i < numColliders; i++)
            {
                hitColliders[i].SendMessage("AddDamage");
            }
        }
    }
}
