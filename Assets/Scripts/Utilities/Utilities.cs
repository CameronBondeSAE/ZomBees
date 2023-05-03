using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameronBonde
{
	public class Utilities : MonoBehaviour
	{
		public static string RemoveCarriageReturns(string input)
		{
			return input.Replace("\r", "");
		}
	}
}