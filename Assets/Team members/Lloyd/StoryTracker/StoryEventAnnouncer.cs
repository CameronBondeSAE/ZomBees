using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEventAnnouncer : MonoBehaviour
{
    public Action<MonoBehaviour, bool> ChangeQueenState;

    public void OnChangeQueenState(MonoBehaviour state, bool activated)
    {
        ChangeQueenState?.Invoke(state, activated);
    }   
}
