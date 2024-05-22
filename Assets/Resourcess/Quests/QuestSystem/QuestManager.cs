using System.Collections.Generic;
using UnityEngine;

namespace Lance
{
    public class QuestManager : MonoBehaviour
    {
        public static QuestManager instance;

        public List<QuestInfoSO> activeQuests = new List<QuestInfoSO>();

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void StartQuest(QuestInfoSO quest)
        {
            activeQuests.Add(quest);
            Debug.Log("Started quest: " + quest.displayName);
        }

        public void FinishQuestStep(QuestStep questStep)
        {
            foreach (QuestInfoSO quest in activeQuests)
            {
                foreach (GameObject stepPrefab in quest.questStepPrefabs)
                {
                    QuestStep step = stepPrefab.GetComponent<QuestStep>();
                    if (step == questStep)
                    {
                        Debug.Log("Finished quest step: " + stepPrefab.name);
                        return;
                    }
                }
            }
        }

        public bool IsQuestCompleted(QuestInfoSO quest)
        {
           
            foreach (QuestInfoSO activeQuest in activeQuests)
            {
                if (activeQuest == quest)
                {
                    foreach (GameObject stepPrefab in quest.questStepPrefabs)
                    {
                        QuestStep step = stepPrefab.GetComponent<QuestStep>();
                        if (step != null && !step.IsFinished)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
