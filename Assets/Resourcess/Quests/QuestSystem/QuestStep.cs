using UnityEngine;

namespace Lance
{
    public abstract class QuestStep : MonoBehaviour
    {
        protected bool isFinished = false;

        // Public property to check if the step is finished
        public bool IsFinished => isFinished;

        protected void FinishQuestStep()
        {
            if (!isFinished)
            {
                isFinished = true;

                // Notify the quest manager that this step is finished
                QuestManager.instance.FinishQuestStep(this);

                Destroy(gameObject);
            }
        }
    }
}
