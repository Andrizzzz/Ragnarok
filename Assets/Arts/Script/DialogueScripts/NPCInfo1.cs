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

            // Check for input to proceed to the next dialogue
            if (dialogueStarted && (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0)))
            {
                if (!isTyping)
                {
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
