using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorldTime_ViewModel : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    
    void Update()
    {
        // textMeshProUGUI.text = WorldTime.Instance.currentDayTracker + "\nTime = " + WorldTime.Instance.time;
        // textMeshProUGUI.text = "Time = " + WorldTime.Instance.GetFormattedTime();
        textMeshProUGUI.text = WorldTime.Instance.GetFormattedTime();
    }
}
