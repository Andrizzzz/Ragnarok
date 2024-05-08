using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{
    public string[] dialogue; // Array of dialogue lines
    private bool playerInRange; // Flag to track if player is in range
    public Text dialogueText; // Reference to the UI Text component

    // Called when another collider enters the NPC's collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Check if the collider is the player
        {
            playerInRange = true;
            // You can add more functionality here like displaying a prompt
        }
    }

    // Called when another collider exits the NPC's collider
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Check if the collider is the player
        {
            playerInRange = false;
            // You can remove the prompt or any other UI element here
        }
    }

    // Method to trigger dialogue
    public void TriggerDialogue()
    {
        // Clear previous dialogue
        dialogueText.text = "";

        // You can implement your dialogue system here, e.g., using Unity UI or a custom dialogue system
        foreach (string line in dialogue)
        {
            dialogueText.text += line + "\n"; // Append each line of dialogue
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.Space)) // Check if player is in range and presses space
        {
            TriggerDialogue(); // Trigger dialogue when conditions are met
        }
    }
}
