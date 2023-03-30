using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class ChatBox : MonoBehaviour
{
    public TestGPT        testGpt;
    
    // Start is called before the first frame update
    void Start()
    {
        testGpt.StartChatConversation();
        // inputField.onSubmit.AddListener(NewInput);
    }

    void NewInput(string input)
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(new Ray(transform.position, transform.forward), out hitInfo, 3f, Int32.MaxValue, QueryTriggerInteraction.Ignore))
        {
            testGpt.AppendUserInput(input, hitInfo.transform.GetComponent<FakeCivilian>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
