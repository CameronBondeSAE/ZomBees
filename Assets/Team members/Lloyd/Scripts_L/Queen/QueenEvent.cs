using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class QueenEvent : MonoBehaviour
{
    // QUEEN ATTACK
    //
    // swarm is projected to the new position
    // 
    // ie, the Queen detects a Player. The Queen attacks by changing the swarm focus point and size, "shooting" them at the Player
    // when the attack is over, swarm returns to the Queen's current pos
    public event Action<Transform> ChangeSwarmTransform;

    public void OnChangeSwarmPoint(Transform swarmTransform)
    {
        ChangeSwarmTransform?.Invoke(swarmTransform);
    }
    
    // changes the swarm's circle size
    // ie how far away they are from the centre point
    public event Action<float> ChangeSwarmCircleSize;

    public void OnChangeSwarmCircleSize(float circleSize)
    {
        ChangeSwarmCircleSize?.Invoke(circleSize);
    }

    public Action<MonoBehaviour, bool> ChangeQueenState;

    public void OnChangeQueenState(MonoBehaviour state, bool activated)
    {
        ChangeQueenState?.Invoke(state, activated);
    }
}