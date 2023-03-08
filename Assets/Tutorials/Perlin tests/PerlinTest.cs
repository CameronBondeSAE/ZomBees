using System;
using System.Collections;
using System.Collections.Generic;
using Tanks;
using UnityEngine;

public class Audience : MonoBehaviour
{
	public Goals goals;
	
	private void Awake()
	{
		goals.GoalGotEvent += ScreamsLikeAFootballFan;
	}

	private void ScreamsLikeAFootballFan()
	{
		Debug.Log("I GOT A GOAL EVEN THOUGH I HAD TO PART IN IT. I FEEL FULLFILLED!");
	}
}
public class ScoreBoard : MonoBehaviour
{
	public GameObject gasdfasd;
	public Goals goals;
	public Rigidbody rb;
	public int health;
	
	private void Awake()
	{
		goals.GoalGotEvent += IncreaseScore;
	}

	private void IncreaseScore()
	{
		Debug.Log("GOOOOOOOAAALLLL");
	}
}

public class Goals : MonoBehaviour
{
	// FAKE: What the event system in C# is really doing. Just a bunch of memory addresses pointing to functions
	public List<int> memoryAddresses;

	
	public event Action GoalGotEvent; // This is actually just a list of memory addresses

	private int[] arrayOfValues;
	public List<int> listOfValues;

	public void OnTriggerEnter(Collider other)
	{
		GoalGotEvent?.Invoke(); // for each memory address, go to them

		// FAKE: What is really happening in the event 'system' it's pretty simple in the end
		// foreach (int memoryAddress in memoryAddresses)
		// {
		// 	memoryAddress();
		// }
	}
}
































public class PerlinTest : MonoBehaviour
{
	public GameObject currentTarget;
	public GameObject Monster1;
	public GameObject Monster2;
	public GameObject rocketPrefab;

	public List<GameObject> things;

	public event Action ThingHappenedEvent;
	
	
	
    // Update is called once per frame
    void Update()
    {
	    ThingHappenedEvent?.Invoke();
	    
	    
	    float y;
	    y = Mathf.Sin(Time.time*20f);
	    float x;
	    x = Mathf.Cos(Time.time*13f);
	    
	    transform.localPosition = new Vector3(x, y, 0);

	    currentTarget = Monster2;
	    Destroy(currentTarget);

	    GameObject newGO = Instantiate(rocketPrefab);
	    newGO.transform.localScale = new Vector3(2f, 2f, 2f);

	    
	    Instantiate(rocketPrefab).transform.localScale = new Vector3(2f,2f,2f);
	    
	    
    }

    
    
    
    
    
    
    
    
    public GameObject ReturnSomething()
    {
	    return FindObjectOfType<Rigidbody>().gameObject;
    }
}
