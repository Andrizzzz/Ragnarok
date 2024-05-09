using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{
    public Transform npcTransform;
    public Button interactButton;
    
    public float interactionDistance = 2f;

    private void Update()
    {
        // Calculate the distance between player and NPC
        float distance = Vector2.Distance(transform.position, npcTransform.position);

        // Enable or disable the button and panel based on the distance
        if (distance <= interactionDistance)
        {
            interactButton.gameObject.SetActive(true);
            
        }
        else
        {
            interactButton.gameObject.SetActive(false);
             // Hide the panel if the player is far from the NPC
        }
    }

    public void InteractWithNPC()
    {
        // Implement your interaction logic here
        Debug.Log("Interacting with NPC...");
    }
}





