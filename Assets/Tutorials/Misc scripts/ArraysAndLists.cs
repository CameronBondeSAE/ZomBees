using System;
using UnityEngine;

namespace CameronBonde
{

	[Serializable]
	public class FakeNode
	{
		public bool   isBlocked;
		public Color  colour;
		public string type;
	}

	public class ArraysAndLists : MonoBehaviour
	{
		public bool    boolTest;
		public bool[]  boolsTest;
		public bool[,] bool2DArrayTest;

		public FakeNode[,]    nodes2D;
		public GameObject thingToSpawn;

		// Start is called before the first frame update
		void Start()
		{
			nodes2D[0, 0]           = new FakeNode();
			nodes2D[0, 0].colour    = Color.red;
			nodes2D[0, 0].isBlocked = true;
			
			Debug.Log(boolsTest[0]);
			Debug.Log(bool2DArrayTest[1919, 1079]);
		}
	}	
}
