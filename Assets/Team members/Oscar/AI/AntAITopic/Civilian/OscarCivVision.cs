using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using Oscar;
using UnityEditor.SceneManagement;
using UnityEngine;

public class OscarCivVision : MonoBehaviour
{
    public List<GameObject> beesInSight;

    public List<GameObject> honeyInSight;

    public MemoryManger memoryManger;
    public Memory memory;

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.GetComponent<LittleGuy>() != null)
            {
                if (other.GetComponent<LittleGuy>().isBee == true)
                {
                    GameObject beeStuff = other.gameObject;
                
                    beesInSight.Add(beeStuff);
                    
                    memory.position = Vector2Int.RoundToInt(beeStuff.transform.position);
                    memory.description = "Saw a bee";
                    memory.timeStamp = Time.time;
                    memory.theThing = beeStuff.GetComponent<DynamicObject>();
                    
                    memoryManger.memories.Add(memory);
                }
            }
            
            if (other.GetComponent<Honey>() != null)
            {
                GameObject honeyStuff = other.gameObject;

                if (!honeyInSight.Contains(honeyStuff))
                {
                    honeyInSight.Add(honeyStuff);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Honey>() != null)
        {
            GameObject honeyStuff = other.gameObject;
        
            honeyInSight.Remove(honeyStuff);
        }
        if (other.GetComponent<BeeTemp>())
        {
            GameObject beeStuff = other.gameObject;
            
            beesInSight.Remove(beeStuff);
        }
    }
}
