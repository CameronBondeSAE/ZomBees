using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Oscar
{
    class PromptResult
    {
        public enum Actions
        {
            attackCiv,
            ignoreThem
        }
        
        public string characterName;
        public string outputSpeech;
        public bool announcement;
        public Actions action;
    }

    public class Emotions
    {
        public float attitude;
    }

    public class OscarJSON : MonoBehaviour
    {
        [TextArea(5, 40)] public string finalPrompt;

        [Button]
        void CreatePrompt()
        {
            Emotions emotions = new Emotions
            {
                attitude = 1f
            };

            finalPrompt = @"Using this JSON as a template. attitude is a 0 to 1 float value.
		    Replace ""ExampleAction"" with either attackCiv, ignoreThem.
		    Announcement is a bool and is true if they aren’t replying to a specific character
		    ";
            finalPrompt += "";
            // Example JSON to teach GPT what to return
            finalPrompt += @"
		    {
				""characterName"": characterName,
				""outputSpeech"": ""Example"",
				""announcement"": true,
				""action"": ExampleAction
		    }";

            
        }

    }
}