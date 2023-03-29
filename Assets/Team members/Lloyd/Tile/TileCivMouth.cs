using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCivMouth : MonoBehaviour
{
    private bool fear;

    private string prompt;

    public void Prompt()
    {
        if (fear)
            prompt = "I'm scared!";

        else
        {
            prompt = "I'm chill!";
        }
    }
}
