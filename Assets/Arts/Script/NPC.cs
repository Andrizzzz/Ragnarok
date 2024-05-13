using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Lance
{
    public class NPC : MonoBehaviour
    {
        public GameObject dialoguePanel;
        public Text dialogueText;
        public string[] dialogue;
        private int index;

        public GameObject contButton;
        public float wordSpeed;
        private bool playerIsClose;
        private bool dialogueStarted;

        private bool isTyping;

        // Reference to the UI overlay image
        public Image overlayImage;

        void Start()
        {
            dialogueStarted = false;
            // Ensure the overlay is initially disabled
            if (overlayImage != null)
            {
                overlayImage.gameObject.SetActive(false);
            }
        }

        void Update()
        {
            // Check if the player is close and dialogue hasn't started yet
            if (playerIsClose && !dialogueStarted)
            {
                StartDialogue();
            }

            // Check if the player has moved away and dialogue is active
            if (!playerIsClose && dialogueStarted)
            {
                EndDialogue();
            }
        }

        void StartDialogue()
        {
            // Enable the overlay to darken the background
            if (overlayImage != null)
            {
                overlayImage.gameObject.SetActive(true);
            }

            dialoguePanel.SetActive(true);
            StartCoroutine(Typing());
            dialogueStarted = true;
        }

        IEnumerator Typing()
        {
            if (isTyping) yield break; // If already typing, exit the coroutine

            isTyping = true;
            dialogueText.text = ""; // Clear the dialogue text before starting typing

            string currentDialogue = dialogue[index]; // Get the current dialogue line
            int dialogueLength = currentDialogue.Length;

            for (int i = 0; i < dialogueLength; i++)
            {
                dialogueText.text += currentDialogue[i];
                yield return new WaitForSeconds(wordSpeed);
            }

            isTyping = false;

            // Check if it's the last line of dialogue
            if (index == dialogue.Length - 1)
            {
                contButton.SetActive(false); // Disable the continue button
                yield return new WaitForSeconds(1.5f); // Add a delay before hiding the dialogue panel
                dialoguePanel.SetActive(false); // Disable the entire dialogue panel after the last line
                dialogueText.text = ""; // Clear the dialogue text
                index = 0; // Reset the dialogue index

                // Disable the overlay after hiding the dialogue panel
                if (overlayImage != null)
                {
                    overlayImage.gameObject.SetActive(false);
                }
            }
            else
            {
                contButton.SetActive(true); // Enable the continue button
            }
        }

        public void NextLine()
        {
            if (isTyping)
                return;

            if (index < dialogue.Length - 1)
            {
                index++;
                dialogueText.text = "";
                StartCoroutine(Typing());
            }
        }

        void EndDialogue()
        {
            dialogueText.text = "";
            index = 0;
            dialoguePanel.SetActive(false); // Disable the entire dialogue panel
            contButton.SetActive(false); // Disable the continue button
            dialogueStarted = false;

            // Disable the overlay when ending the dialogue
            if (overlayImage != null)
            {
                overlayImage.gameObject.SetActive(false);
            }
        }

        public void ContinueButtonClick()
        {
            NextLine();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                playerIsClose = true;
                // Clear the dialogue text when the player collides with the NPC to prevent jumbled letters
                dialogueText.text = "";
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                playerIsClose = false;
            }
        }
    }
}















