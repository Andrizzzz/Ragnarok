// DialogueManager.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Lance
{
    public class DialogueManager : MonoBehaviour
    {
        private static DialogueManager instance;
        public static DialogueManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<DialogueManager>();
                    if (instance == null)
                    {
                        Debug.LogError("DialogueManager instance is not found in the scene.");
                    }
                }
                return instance;
            }
        }

        public Image characterIcon;
        public TextMeshProUGUI characterName;
        public TextMeshProUGUI dialogueArea;

        private Queue<DialogueLine> lines;
        private bool isDialogueActive = false;

        public float typingSpeed = 0.2f;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;

            lines = new Queue<DialogueLine>();

            // Disable the dialogue UI initially
            HideDialogueUI();
        }

        public void StartDialogue(Dialogue dialogue)
        {
            isDialogueActive = true;
            ShowDialogueUI();

            lines.Clear();

            foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
            {
                lines.Enqueue(dialogueLine);
            }

            DisplayNextDialogueLine();
        }

        public void DisplayNextDialogueLine()
        {
            if (lines.Count == 0)
            {
                EndDialogue();
                return;
            }

            DialogueLine currentLine = lines.Dequeue();

            characterIcon.sprite = currentLine.character.icon;
            characterName.text = currentLine.character.name;

            StopAllCoroutines();

            StartCoroutine(TypeSentence(currentLine));
        }

        IEnumerator TypeSentence(DialogueLine dialogueLine)
        {
            dialogueArea.text = "";
            foreach (char letter in dialogueLine.line.ToCharArray())
            {
                dialogueArea.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        void EndDialogue()
        {
            isDialogueActive = false;
            HideDialogueUI();
            // Additional actions to take when dialogue ends
        }

        // Method to show the dialogue UI
        private void ShowDialogueUI()
        {
            gameObject.SetActive(true);
        }

        // Method to hide the dialogue UI
        private void HideDialogueUI()
        {
            gameObject.SetActive(false);
        }

        // Method to check if dialogue is active
        public bool IsDialogueActive()
        {
            return isDialogueActive;
        }
    }
}
