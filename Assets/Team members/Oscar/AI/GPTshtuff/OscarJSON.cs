using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Oscar
{
    public class PromptResult
    {
        public enum Actions
        {
            huntBees,
            lookForBees,
            attackBee,
            tacticalRetreat
        }
        
        public string personality;
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

        private PromptResult _promptResult;

        [Button]
        void CreatePrompt()
        {
            OscarCivController civConditions = GetComponent<OscarCivController>();

            Emotions emotions = new Emotions
            {
                attitude = 1f
            };


            finalPrompt =
                string.Format("Using this JSON as a template. attitude is a 0 to 1 float value. " +
                              "replace Example in personality to {_promptResult.personality}. " +
                              "Replace ExampleAction with one of the actions in {_promptResult.action}. " +
                              "Announcement is a bool and is true if they aren’t replying to a specific character", _promptResult.personality, _promptResult.action);
             finalPrompt += "";
            // Example JSON to teach GPT what to return
            finalPrompt += @"
		    {
                ""personality"": ""Example"",
                ""outputSpeech"": ""Example"",
                ""announcement"": true,
                ""action"": ExampleAction
		    }";

            finalPrompt += "\nCan I see a bee: " + civConditions.SeeBeeBool();
            finalPrompt += "\nAm I near the Bee: " + civConditions.NearBeeBool();
            finalPrompt += "\nAm I scared: " + civConditions.IsScaredBool();
            finalPrompt += "\nAm I Alive: " + civConditions.StayAliveBool();
            finalPrompt += "\nHave I killed a bee: " + civConditions.KilledBeeBool();

            finalPrompt += "\nUse this information to generate a spontaneous outburst from the character";
            
            Debug.Log(finalPrompt);
        }

        [Button]
        void createJSON()
        {
            PromptResult promptResults = new PromptResult();

            promptResults = JsonUtility.FromJson<PromptResult>(finalPrompt);
        }

    }
}