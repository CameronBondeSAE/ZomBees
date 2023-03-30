using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Oscar
{
    public class PromptResult : MonoBehaviour
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
        public PromptStim _promptResult;

        private MemoryManger memoryManger;
        
        [TextArea(5, 40)] public string finalPrompt;
        
        [Button]
        void CreatePrompt()
        {
            OscarCivController civConditions = GetComponent<OscarCivController>();
            memoryManger = GetComponent<MemoryManger>();
            
            Emotions emotions = new Emotions
            {
                attitude = 1f
            };


            finalPrompt = "Using this JSON as a template. attitude is a 0 to 1 float value. ";
            finalPrompt += "\nreplace ExamplePersonality in personality to: " + _promptResult.personality;
            finalPrompt += "\nReplace ExampleAction with an action like: " + _promptResult.enumValue;
            finalPrompt += "\nReplace ExampleSpeech in the outputSpeech with the characters response.";
            finalPrompt += "\nAnnouncement is a bool and is true if they aren’t replying to a specific character.";
             finalPrompt += "";
            // Example JSON to teach GPT what to return
            finalPrompt += @"
		    {
                ""personality"": ExamplePersonality,
                ""outputSpeech"": ExampleSpeech,
                ""announcement"": true,
                ""action"": ExampleAction

		    }";
            finalPrompt += "\nThe following list of bools are the characters conditions in the world:";
            finalPrompt += "\nCan I see a bee: " + civConditions.SeeBeeBool();
            finalPrompt += "\nAm I scared: " + civConditions.IsScaredBool();
            finalPrompt += "\nAm I Alive: " + civConditions.StayAliveBool();
            finalPrompt += "\nHave I killed a bee: " + civConditions.KilledBeeBool();
            finalPrompt += "\nThis is the memory of what the civilian has already seen: ";
            foreach (var memory in memoryManger.memories)
            {
                finalPrompt += "\nMemory time: " + memory.timeStamp;
                finalPrompt += "\nRemembered Object " + memory.description;
                finalPrompt += "\nRemembered dynamicObject position: " + memory.position;
                finalPrompt += "\nRemembered DynamicObject: " + memory.theThing;
            }
            
            finalPrompt += "\nUse the character condition information and its memories to influence a response from the characters perspective. Place the response in the JSON template";
            
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