using Anthill.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamModel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AntAIAgent antAIAgent = GetComponent<AntAIAgent>();
        antAIAgent.SetGoal("RetrieveItem");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
