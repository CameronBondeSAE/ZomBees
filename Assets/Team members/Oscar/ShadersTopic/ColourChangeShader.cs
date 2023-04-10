using System;
using System.Collections;
using System.Collections.Generic;
using Oscar;
using UnityEngine;

public class ColourChangeShader : MonoBehaviour
{
    public float newCutOffHeight;

    public Material mat;

    private BasicBeeEventsManager _basicBeeEventsManager;

    private void Update()
    {
        mat.SetFloat("_CutOffHeight", newCutOffHeight);
    }

    // private void Awake()
    // {
    //     _basicBeeEventsManager = GetComponent<BasicBeeEventsManager>();
    // }
    //
    // private void OnEnable()
    // {
    //     _basicBeeEventsManager.attackThing += Attack;
    //     _basicBeeEventsManager.searchThing += Search;
    // }
    //
    // private void OnDisable()
    // {
    //     _basicBeeEventsManager.attackThing -= Attack;
    //     _basicBeeEventsManager.searchThing -= Search;
    // }
    //
    // private void Attack()
    // {
    //     newCutOffHeight += Time.deltaTime * 2;
    //
    //     if (newCutOffHeight >= 1f)
    //     {
    //         newCutOffHeight = 1f;
    //     }
    //     
    //     mat.SetFloat("_CutOffHeight", newCutOffHeight);
    // }
    //
    // public void Search()
    // {
    //     newCutOffHeight -= Time.deltaTime * 2;
    //
    //     if (newCutOffHeight <= -10f)
    //     {
    //         newCutOffHeight = -10f;
    //     }
    //     
    //     mat.SetFloat("_CutOffHeight", newCutOffHeight);
    // }
}
