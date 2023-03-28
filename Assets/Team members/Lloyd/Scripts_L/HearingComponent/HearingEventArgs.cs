using System;
using UnityEngine;

public class HearingEventArgs : EventArgs
{
    public GameObject Source { get; set; }
    public float Volume { get; set; }
    public float Fear { get; set; }
    public float Beeness { get; set; }
    public Team Team { get; set; }
}