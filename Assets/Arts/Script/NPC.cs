using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

        void Start()
        {
            dialogueStarted = false;
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
                yield return new WaitForSeconds(2.5f); // Add a delay before hiding the dialogue panel
                dialoguePanel.SetActive(false); // Disable the entire dialogue panel after the last line
                dialogueText.text = ""; // Clear the dialogue text
                index = 0; // Reset the dialogue index
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









