using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEvent : StoryEventMustSucceed
{
    private float timeStamp;

    public List<string> GPTstrings = new List<string>();
        
    string gptDescription;

    private Transform positionMarker;

    public List<StoryEvent> mustSucceedList = new List<StoryEvent>();
    
    private List<StoryEventActionBase> actionBaseList = new List<StoryEventActionBase>();

    
   // ????? gameObject.AddComponent<StoryEventActionBase>

}
