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
        public GameObject backButton; // Back button reference
        public float wordSpeed;
        private bool playerIsClose;
        private bool dialogueStarted;

        private bool isTyping;

        // Reference to the UI overlay image
        public Image overlayImage;

        // Collider triggering the dialogue
        public Collider2D dialogueCollider;

        // Flag to track if the collider has triggered the dialogue
        private bool colliderTriggered;

        void Start()
        {
            dialogueStarted = false;
            colliderTriggered = false; // Initialize the flag
            // Ensure the overlay is initially disabled
            if (overlayImage != null)
            {
                overlayImage.gameObject.SetActive(false);
            }

            // Ensure buttons are initially disabled
            contButton.SetActive(false);
            backButton.SetActive(false);
        }

        void Update()
        {
            // Check if the player is close and dialogue hasn't started yet
            if (playerIsClose && !dialogueStarted && !colliderTriggered)
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
            // Pause the game
            Time.timeScale = 0;

            // Enable the overlay to darken the background
            if (overlayImage != null)
            {
                overlayImage.gameObject.SetActive(true);
            }

            dialoguePanel.SetActive(true);
            // Enable the skip button
            contButton.SetActive(true);
            StartCoroutine(Typing());
            dialogueStarted = true;
            colliderTriggered = true; // Set the collider-triggered flag
                                      // Disable the collider triggering the dialogue
            if (dialogueCollider != null)
            {
                dialogueCollider.enabled = false;
            }
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
                yield return new WaitForSecondsRealtime(wordSpeed); // Use WaitForSecondsRealtime to respect the paused timescale
            }

            isTyping = false;

            // Enable the continue button
            contButton.SetActive(true);
            // Enable the back button if not on the first dialogue
            backButton.SetActive(index > 0);
            Debug.Log("Typing complete. Index: " + index + ". Back button active: " + (index > 0));
        }

        public void NextLine()
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = dialogue[index]; // Immediately display the full sentence
                isTyping = false;
                contButton.SetActive(true); // Enable the continue button to proceed to the next dialogue
                backButton.SetActive(index > 0); // Enable the back button if not on the first dialogue
            }
            else
            {
                if (index < dialogue.Length - 1)
                {
                    index++;
                    dialogueText.text = "";
                    StartCoroutine(Typing());
                }
                else
                {
                    // If it's the last dialogue, end the dialogue
                    EndDialogue();
                }
            }
        }

        public void PreviousLine()
        {
            if (isTyping)
            {
                StopAllCoroutines();
                isTyping = false;
            }

            if (index > 0)
            {
                index--; // Move to the previous dialogue line
                dialogueText.text = dialogue[index]; // Directly set the previous dialogue line
                Debug.Log("Moved to previous line. Index: " + index);
            }

            // Ensure the back button is properly updated
            backButton.SetActive(index > 0);
        }

        void EndDialogue()
        {
            // Resume the game
            Time.timeScale = 1;

            dialogueText.text = "";
            index = 0;
            dialoguePanel.SetActive(false); // Disable the entire dialogue panel
            contButton.SetActive(false); // Disable the continue button
            backButton.SetActive(false); // Disable the back button
            dialogueStarted = false;

            // Disable the overlay when ending the dialogue
            if (overlayImage != null)
            {
                overlayImage.gameObject.SetActive(false);
            }
            Debug.Log("Dialogue ended.");
        }

        public void ContinueButtonClick()
        {
            NextLine();
        }

        public void BackButtonClick()
        {
            PreviousLine();
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