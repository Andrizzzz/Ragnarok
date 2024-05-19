using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lance
{
    public class DoorInteraction : MonoBehaviour
    {
        public Button interactButton; // Assign the button in the Inspector
        public Collider2D triggerCollider; // Assign the trigger collider in the Inspector
        public Collider2D barrierCollider; // Assign the barrier collider in the Inspector
        public Sprite closedDoorSprite; // Assign the closed door sprite in the Inspector
        public Sprite openedDoorSprite; // Assign the opened door sprite in the Inspector
        public Vector3 closedPositionOffset; // Position offset for closed state
        public Vector3 openedPositionOffset; // Position offset for opened state
        public Material litMaterial; // Assign the lit material in the Inspector

        private SpriteRenderer spriteRenderer;
        private bool doorOpened = false; // Track whether the door has been opened

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
            spriteRenderer.sprite = closedDoorSprite; // Set the initial sprite to the closed door
            spriteRenderer.material = litMaterial; // Set the material to the lit material
            transform.localPosition = closedPositionOffset; // Set the initial position offset
            interactButton.gameObject.SetActive(false); // Hide button initially
            interactButton.onClick.AddListener(OnButtonClick); // Add listener to button

            Debug.Log($"Door {gameObject.name} initialized with closed sprite and lit material.");
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (!doorOpened && other.CompareTag("Player")) // Ensure it's the player and door isn't opened
            {
                Debug.Log($"Player entered the trigger area of door {gameObject.name}.");
                interactButton.gameObject.SetActive(true); // Show button
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (!doorOpened && other.CompareTag("Player")) // Ensure it's the player and door isn't opened
            {
                Debug.Log($"Player exited the trigger area of door {gameObject.name}.");
                interactButton.gameObject.SetActive(false); // Hide button
            }
        }

        public void OnButtonClick()
        {
            if (!doorOpened) // Only proceed if the door hasn't been opened
            {
                Debug.Log($"Button clicked. Door {gameObject.name} opening.");
                barrierCollider.enabled = false; // Disable the physical collider
                interactButton.gameObject.SetActive(false); // Hide button after click
                spriteRenderer.sprite = openedDoorSprite; // Change to the opened door sprite
                spriteRenderer.material = litMaterial; // Ensure the material remains set
                transform.localPosition = openedPositionOffset; // Adjust the position for the opened door
                doorOpened = true; // Set flag to true

                Debug.Log($"Door {gameObject.name} opened and sprite/material updated.");
            }
        }
    }
}
