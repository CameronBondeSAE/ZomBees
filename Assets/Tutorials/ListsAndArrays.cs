using CameronBonde;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListsAndArrays : MonoBehaviour
{
	public int            thing;
	public FakeNode[]     FakeNodesArray;
	public List<FakeNode> Open;
	public List<FakeNode> Closed;

	public Vector2 currentPosition;

	void Update()
	{
		Vector3 myVec3 = new Vector3();
		
		Debug.Log(myVec3);
		Debug.Log("HELLO");
	}
	
}