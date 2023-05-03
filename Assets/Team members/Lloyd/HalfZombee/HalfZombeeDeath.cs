using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Lloyd;
using UnityEngine;

public class HalfZombeeDeath : AntAIState
{
    private BeeGib beeGib;

    public HalfZombeeSensor sensor;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        sensor = aGameObject.GetComponent<HalfZombeeSensor>();
    }

    public override void Enter()
    {
        base.Enter();
        beeGib = GetComponentInChildren<BeeGib>();
        beeGib.DetermineGib(BeeGib.BeeType.Medium);
        DestroyImmediate(sensor.gameObject);
    }
}
