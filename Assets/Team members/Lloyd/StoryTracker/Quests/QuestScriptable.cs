using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
[CreateAssetMenu(fileName = "New Quest Data", menuName = "Quest Data")]
public class QuestScriptable : ScriptableObject
{  
        //written in tandem with chat GPT

        private AIModelView modelView;
        
        [System.Serializable]
         public class QuestRequirements
         {
                 public bool requirementFulfilled;
                 public string requirementName;
                 public Vector3 requirementlocation;
         }
         
        public string questName;
        public string questDescription;

        public float startTime;
        public float endTime;

        private int listLength;

        public int minRequirements;
        public List<QuestRequirements> requirements = new List<QuestRequirements>();
        public QuestScriptable[] fulfilled;

        public void RequirementFulfilled(int requirementIndex)
        {
                requirements[requirementIndex].requirementFulfilled = true;
        }

        public enum QuestStatus
        {
                Idle,
                Began,
                Completed,
                Failed
        }
        public QuestStatus questStatus;
        
        public void OnEnable()
        {
                if(QuestTracker.QuestInstance != null)
                QuestTracker.QuestInstance.FinishedInitializeEvent += Begin;
        }

        public void Begin()
        {
                ChangeQuestStatus(QuestStatus.Idle);
        }
                

        public void Update()
        {
                float time = WorldState.WorldInstance.time;
                
                if(time >= startTime)
                        ChangeQuestStatus(QuestStatus.Began);

                if (time >= endTime)
                {
                        if(fulfilled.Length >= minRequirements)
                                ChangeQuestStatus(QuestStatus.Completed);

                        else
                        {
                                ChangeQuestStatus(QuestStatus.Failed);
                        }
                }
                Debug.Log(time);
        }

        [Button]
        public void ChangeQuestStatus(QuestStatus status)
        {
                if (status == QuestStatus.Idle)
                {
                        listLength = requirements.Count;
                        QuestTracker.QuestInstance.AddQuest(this);
                }

                if (status == QuestStatus.Began)
                {
                        QuestTracker.QuestInstance.MoveQuest(0, this, 1);
                }

                else if (status == QuestStatus.Completed)
                {
                        QuestTracker.QuestInstance.MoveQuest(0, this, 2);
                }

                else if (status == QuestStatus.Failed)
                {
                        QuestTracker.QuestInstance.MoveQuest(0, this, 3);
                }
                
                questStatus = status;
        }

        private void OnDisable()
        {
                QuestTracker.QuestInstance.FinishedInitializeEvent -= Begin;
        }
}
