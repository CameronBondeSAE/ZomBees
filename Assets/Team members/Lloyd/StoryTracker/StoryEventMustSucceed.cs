using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEventMustSucceed : MonoBehaviour
{
    public int boolAmountToWin;
    public List<bool> dependancyBools;
    
    public void CreateMustSucceedList()
    {
        dependancyBools = new List<bool>();
        boolAmountToWin = dependancyBools.Count;
    }

    public void CreateMustSucceed(bool mustSucceed)
    {
        dependancyBools.Add(mustSucceed);
    }
}