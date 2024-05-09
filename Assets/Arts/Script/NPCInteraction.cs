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

        // Enable or disable the button based on the distance
        if (distance <= interactionDistance)
        {
            interactButton.gameObject.SetActive(true);
        }
        else
        {
            interactButton.gameObject.SetActive(false);
        }
    }

    public void InteractWithNPC()
    {
        // Implement your interaction logic here

        // Hide the interact button
        interactButton.gameObject.SetActive(false);

        Debug.Log("Interacting with NPC...");
    }
}






