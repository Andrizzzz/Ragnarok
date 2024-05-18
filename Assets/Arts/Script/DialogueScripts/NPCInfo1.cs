using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Lance
{
    public class NPCInfo1 : MonoBehaviour
    {
        public GameObject dialoguePanel;
        public Text dialogueText;
        public string[] dialogue;
        private int index;

        public float wordSpeed;
        private bool playerIsClose;
        private bool dialogueStarted;

        private bool isTyping;
        private bool skipCurrentSentence;

        // Reference to the UI overlay image
        public Image overlayImage;

        // Collider triggering the dialogue
        public Collider2D dialogueCollider;

        // Reference to the currently active dialogue panel
        private GameObject currentDialoguePanel;

        // Flag to track if the dialogue has finished
        private bool dialogueFinished;

        void Start()
        {
            dialogueStarted = false;
            dialogueFinished = false;
            // Ensure the overlay is initially disabled
            if (overlayImage != null)
            {
                overlayImage.gameObject.SetActive(false);
            }
        }

        void Update()
        {
            // Check if the player is close and dialogue hasn't started yet
            if (playerIsClose && !dialogueStarted && currentDialoguePanel == null && !dialogueFinished)
            {
                StartDialogue();
            }

            // Check for input to proceed to the next dialogue
            if (dialogueStarted && (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0)))
            {
                if (isTyping)
                {
                    // If currently typing, skip to the end of the sentence
                    skipCurrentSentence = true;
                }
                else
                {
                    // If not typing, proceed to the next dialogue
                    NextDialogue();
                }
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
            currentDialoguePanel = dialoguePanel;
            StartCoroutine(Typing());
            dialogueStarted = true;
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
                if (skipCurrentSentence)
                {
                    // If skipping, display the full sentence immediately
                    dialogueText.text = currentDialogue;
                    break;
                }

                dialogueText.text += currentDialogue[i];
                yield return new WaitForSecondsRealtime(wordSpeed); // Use WaitForSecondsRealtime to respect the paused timescale
            }

            isTyping = false;
            skipCurrentSentence = false; // Reset the skip flag

            // Check if the dialogue has finished typing
            if (!skipCurrentSentence && index == dialogue.Length - 1)
            {
                dialogueFinished = true;
            }
        }

        void NextDialogue()
        {
            if (index < dialogue.Length - 1)
            {
                index++;
                StartCoroutine(Typing());
            }
            else
            {
                EndDialogue();
            }
        }

        void EndDialogue()
        {
            // Resume the game
            Time.timeScale = 1;

            dialogueText.text = "";
            index = 0;
            dialoguePanel.SetActive(false); // Disable the entire dialogue panel
            dialogueStarted = false;
            currentDialoguePanel = null;
            dialogueFinished = false; // Reset dialogue finished flag

            // Disable the overlay when ending the dialogue
            if (overlayImage != null)
            {
                overlayImage.gameObject.SetActive(false);
            }
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
