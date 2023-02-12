using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenEvent : MonoBehaviour
{
    //event for declaring transform
    public event Action<Transform> MyTransform;

    public void OnMyTransform(Transform x)
    {
        MyTransform?.Invoke(x);
    }

    public event Action<bool> Attack;

    public void OnAttack(bool x)
    {
        Attack?.Invoke(x);
    }
}