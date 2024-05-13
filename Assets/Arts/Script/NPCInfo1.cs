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
        public float lineDelay; // Add a new variable for line delay
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
            // Enable the overlay to darken the background
            if (overlayImage != null)
            {
                overlayImage.gameObject.SetActive(true);
            }

            dialoguePanel.SetActive(true);
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
                yield return new WaitForSeconds(wordSpeed);
            }

            isTyping = false;

            // Add a delay before displaying the next line
            yield return new WaitForSeconds(lineDelay);

            // Check if it's the last line of dialogue
            if (index == dialogue.Length - 1)
            {
                yield return new WaitForSeconds(1.5f); // Add a delay before hiding the dialogue panel
                EndDialogue();
            }
            else
            {
                index++;
                StartCoroutine(Typing());
            }
        }

        void EndDialogue()
        {
            dialogueText.text = "";
            index = 0;
            dialoguePanel.SetActive(false); // Disable the entire dialogue panel
            dialogueStarted = false;

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











