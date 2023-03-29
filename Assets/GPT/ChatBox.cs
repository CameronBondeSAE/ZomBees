using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class ChatBox : MonoBehaviour
{
    public TMP_InputField inputField;
    public TestGPT        testGpt;
    
    // Start is called before the first frame update
    void Start()
    {
        testGpt.StartChatConversation();
        inputField.onSubmit.AddListener(NewInput);
    }

    void NewInput(string input)
    {
        testGpt.AppendUserInput(input);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
