using UnityEngine;

namespace Lance
{
    public class CollectMineralsStep : QuestStep
    {
        private int mineralsCollected = 0;
        private int mineralsToComplete = 5;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                CollectMineral();
            }
        }

        private void CollectMineral()
        {
            mineralsCollected++;

            // Check if the quest objective has been met
            if (mineralsCollected >= mineralsToComplete)
            {
                // Finish the quest step
                FinishQuestStep();
            }
        }
    }
}
