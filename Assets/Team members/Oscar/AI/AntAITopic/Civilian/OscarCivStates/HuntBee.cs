using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;

public class HuntBee : OscarsLittleGuyMovement
{
    //or navmesh to the Hive patrol points and look around.
    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        BasicMovement(1f);
        Wondering();
    }
}
