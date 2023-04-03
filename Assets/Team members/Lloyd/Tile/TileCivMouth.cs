    using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class TileCivMouth : MonoBehaviour
{
    public CivilianProfile profile;

    public CivEventArgs args;

    public LloydJSONParse jsonParse;

    public void StartGame(CivEventArgs newArgs)
    {
        args = newArgs;

        jsonParse = GetComponent<LloydJSONParse>();
    }

    [Button]
    public void OnTalk()
    {
        jsonParse.CreatePrompt(args);
    }
}
