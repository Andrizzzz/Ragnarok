using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lance
{
    public class NpcQuest : MonoBehaviour
    {
        public static NpcQuest instance;

        public QuestInfoSO questToGive;
        public DialogueSO initialDialogue;
        public DialogueSO questAcceptedDialogue;
        public DialogueSO questProgressDialogue;
        public DialogueSO questCompletedDialogue;
        public DialogueSO questNotCompletedDialogue;
        public DialogueSO questDeclinedDialogue; // Dialogue for quest declined

        private bool questGiven;
        private bool questAccepted;
        private bool questCompleted;

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

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                HandleInteraction();
            }
        }

        private void HandleInteraction()
        {
            if (!questGiven)
            {
                StartDialogue(initialDialogue);
            }
            else if (questGiven && !questAccepted)
            {
                StartDialogue(questAcceptedDialogue, true);
            }
            else if (questAccepted && !questCompleted)
            {
                StartDialogue(questProgressDialogue, true);
            }
            else if (questCompleted)
            {
                StartDialogue(questCompletedDialogue);
            }
        }

        private void StartDialogue(DialogueSO dialogue, bool isProgressed = false)
        {
            DialogueManager.instance.StartDialogue(dialogue, isProgressed);
        }

        public void GiveQuestToPlayer()
        {
            QuestManager.instance.StartQuest(questToGive);
            Debug.Log("Quest given to player: " + questToGive.displayName);
            questAccepted = true; // Mark the quest as accepted
        }

        public void OnDialogueAccepted()
        {
            if (!questGiven)
            {
                questGiven = true; // Mark the quest as given
                GiveQuestToPlayer();
            }
        }

        public void OnDialogueRejected()
        {
            if (questGiven && !questCompleted)
            {
                StartDialogue(questNotCompletedDialogue);
            }
        }

        public void OnDialogueConfirmed()
        {
            if (questAccepted && !questCompleted)
            {
                // Handle quest completion check
                if (QuestManager.instance.IsQuestCompleted(questToGive))
                {
                    questCompleted = true;
                    StartDialogue(questCompletedDialogue);
                }
                else
                {
                    StartDialogue(questNotCompletedDialogue);
                }
            }
        }

        public void OnDialogueDeclined()
        {
            if (questGiven && !questAccepted)
            {
                Debug.Log("Quest declined by player.");
                StartDialogue(questDeclinedDialogue);
                questGiven = false; // Reset the quest given state
            }
        }

        public void CompleteQuest()
        {
            questCompleted = true;
        }
    }
}
