using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CoRoutineTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
//        StartCoroutine(DoSequence());
        
        DoAnother();
    }

    IEnumerator DoSequence()
    {
        Debug.Log("Started!");
        yield return new WaitForSeconds(2);
        Debug.Log("Doing thing");
        yield return new WaitForSeconds(2);
        Debug.Log("Ended!");
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
