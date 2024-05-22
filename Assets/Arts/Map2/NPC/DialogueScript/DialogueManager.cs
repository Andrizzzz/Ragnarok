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
        public Button nextButton; // Add a reference to the next button

        // List of other UI elements to hide/show
        public List<GameObject> otherUIElements = new List<GameObject>();

        public GameObject darkPanel; // Dark panel behind the dialogue UI

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

            // Disable the dialogue UI, dark panel, and next button initially
            HideDialogueUI();
            HideDarkPanel();
            HideNextButton();
        }

        public void StartDialogue(Dialogue dialogue)
        {
            isDialogueActive = true;
            ShowDialogueUI();
            ShowDarkPanel(); // Show dark panel
            HideOtherUI(); // Hide other UI elements

            lines.Clear();

            foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
            {
                lines.Enqueue(dialogueLine);
            }

            DisplayNextDialogueLine();
        }

        public void DisplayNextDialogueLine()
        {
            // Hide the next button at the start of displaying a new line
            HideNextButton();

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

            // Show the next button after the line is fully typed
            ShowNextButton();
        }

        void EndDialogue()
        {
            isDialogueActive = false;
            HideDialogueUI();
            HideDarkPanel(); // Hide dark panel
            ShowOtherUI(); // Show other UI elements
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

        // Method to show the dark panel
        private void ShowDarkPanel()
        {
            if (darkPanel != null)
            {
                darkPanel.SetActive(true);
            }
        }

        // Method to hide the dark panel
        private void HideDarkPanel()
        {
            if (darkPanel != null)
            {
                darkPanel.SetActive(false);
            }
        }

        // Method to hide other UI elements
        private void HideOtherUI()
        {
            foreach (GameObject element in otherUIElements)
            {
                if (element != null)
                {
                    element.SetActive(false);
                }
            }
        }

        // Method to show other UI elements
        private void ShowOtherUI()
        {
            foreach (GameObject element in otherUIElements)
            {
                if (element != null)
                {
                    element.SetActive(true);
                }
            }
        }

        // Method to hide the next button
        private void HideNextButton()
        {
            if (nextButton != null)
            {
                nextButton.gameObject.SetActive(false);
            }
        }

        // Method to show the next button
        private void ShowNextButton()
        {
            if (nextButton != null)
            {
                nextButton.gameObject.SetActive(true);
            }
        }

        // Method to check if dialogue is active
        public bool IsDialogueActive()
        {
            return isDialogueActive;
        }
    }
}
