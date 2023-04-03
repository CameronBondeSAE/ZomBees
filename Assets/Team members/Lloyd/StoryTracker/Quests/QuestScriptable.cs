using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
[CreateAssetMenu(fileName = "New Quest Data", menuName = "Quest Data")]
[Serializable]
public class QuestScriptable : ScriptableObject
{  
        //written in tandem with chat GPT

        private AIModelView modelView;

        public string questPrompt;
        
        public string SendPrompt()
        {
                return questPrompt;
        }

        public void ChangeString(QuestStatus status)
        {
                
        }

        [System.Serializable]
         public class QuestRequirements
         {
                 public bool requirementFulfilled;
                 public string requirementName;
                 public Vector3 requirementlocation;
                 public float requirementStartTime;
                 public float requirementEndTime;
         }
         
         [SerializeField]
         public string questName;
         [SerializeField]
         public string questDescription;

        public float time;
        [SerializeField]
        public float startTime;
        [SerializeField]
        public float endTime;

        private int listLength;

        private QuestTracker questTracker;
        private WorldTime worldTime;

        public int minRequirements;
        public List<QuestRequirements> requirements = new List<QuestRequirements>();
        public QuestScriptable[] fulfilled;

        public string QuestPrompt;
        
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

        public void Begin(QuestTracker tracker, WorldTime time)
        {
                questTracker = tracker;
                worldTime = time;
                ChangeQuestStatus(QuestStatus.Idle);
        }

        public void Update()
        {
                time = worldTime.time;

                        foreach (QuestRequirements requirement in requirements)
                        {
                                if (!requirement.requirementFulfilled && time >= requirement.requirementEndTime)
                                {
                                        requirement.requirementFulfilled = false;
                                        Debug.Log($"{requirement.requirementName} has failed!");
                                }
                        }

                        if (time >= endTime)
                        {
                                int fulfilledCount = 0;
                                foreach (QuestScriptable quest in fulfilled)
                                {
                                        if (quest.questStatus == QuestStatus.Completed)
                                        {
                                                fulfilledCount++;
                                        }
                                }
                                if (fulfilledCount >= minRequirements)
                                {
                                        ChangeQuestStatus(QuestStatus.Completed);
                                }
                                else
                                {
                                        ChangeQuestStatus(QuestStatus.Failed);
                                }
                        }
                
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
        }

        [Button]
        public void ChangeQuestStatus(QuestStatus status)
        {
                if (status == QuestStatus.Idle)
                {
                        listLength = requirements.Count;
                }

                if (status == QuestStatus.Began)
                {
                        questTracker.MoveQuest(0, this, 1);
                }

                else if (status == QuestStatus.Completed)
                {
                        questTracker.MoveQuest(0, this, 2);
                }

                else if (status == QuestStatus.Failed)
                {
                        questTracker.MoveQuest(0, this, 3);
                }
                
                questStatus = status;
                
                OnQuestEvent(questName, questStatus);
        }
        
        public event Action<string, QuestScriptable.QuestStatus> QuestEvent;
        [Button]
        public void OnQuestEvent(string QuestName, QuestScriptable.QuestStatus status)
        {
                QuestEvent?.Invoke(QuestName, status);
        }
}
