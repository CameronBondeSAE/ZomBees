using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

class CamsEventArgs : EventArgs
{
    public float  stuff;
    public string me;
}

public class CoRoutineTest : MonoBehaviour
{
    public delegate void EventHandler(object sender, EventArgs args);

    public event EventHandler myEvent;


    public UnityEvent testUnityEvent;
    
    
    // Start is called before the first frame update
    void Start()
    {
        testUnityEvent?.Invoke();
        
        
        myEvent?.Invoke(this, new CamsEventArgs());
        
        DoAnother();
    }

    IEnumerator DoSequence()
    {
        Debug.Log("Started!");
        yield return new WaitForSeconds(2);
        Debug.Log("Doing thing");
        yield return new WaitForSeconds(2);
        Debug.Log("Ended!");

        Bounds b = new Bounds(Vector3.zero, Vector3.one * 10f);

        b.max = Vector3.one;
    }

    void OnTriggerEnter(Collider other)
    {
        myEvent += OnmyEvent;
    }

    void OnmyEvent(object sender, EventArgs args)
    {
        CamsEventArgs camsEventArgs = args as CamsEventArgs;
        

        Debug.Log("Woo! : "+camsEventArgs.stuff);
    }

    public async void DoAnother()
    {
        Debug.Log("Started!");
        await Task.Delay(2000);
        Debug.Log("Doing thing");
        await Task.Delay(2000);
        Debug.Log("Ended!");
    }
}
