using Lloyd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeenessIncreaserModel : MonoBehaviour
{
    public BeeWingsManager beeWings;

    public Transform viewTransform;
    
    private void Awake()
    {
        beeWings.SetWings();
    }

    public void ChangeWings(float angle, float speed, bool alive)
    {
        beeWings.OnChangeStatEvent(angle, speed, alive);
    }

}
