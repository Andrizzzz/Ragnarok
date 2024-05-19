using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lance
{
    public class DoorInteraction2 : MonoBehaviour
    {
        public Button interactButton; // Assign the button in the Inspector
        public Collider2D triggerCollider; // Assign the trigger collider in the Inspector
        public Collider2D barrierCollider; // Assign the barrier collider in the Inspector
        public Animator doorAnimator; // Reference to the door animator

        private bool doorOpened = false; // Track whether the door has been opened

        void Start()
        {
            interactButton.gameObject.SetActive(false); // Hide button initially
            interactButton.onClick.AddListener(OnButtonClick); // Add listener to button

            // Ensure doorAnimator is assigned
            if (doorAnimator == null)
            {
                Debug.LogError("Door Animator not assigned!");
            }
            else
            {
                Debug.Log("Door Animator assigned successfully.");
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (!doorOpened && other.CompareTag("Player")) // Ensure it's the player and door isn't opened
            {
                Debug.Log("Player entered the trigger area.");
                interactButton.gameObject.SetActive(true); // Show button
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (!doorOpened && other.CompareTag("Player")) // Ensure it's the player and door isn't opened
            {
                Debug.Log("Player exited the trigger area.");
                interactButton.gameObject.SetActive(false); // Hide button
            }
        }

        public void OnButtonClick()
        {
            if (!doorOpened) // Only proceed if the door hasn't been opened
            {
                Debug.Log("Button clicked. Door opening.");
                barrierCollider.enabled = false; // Disable the physical collider
                interactButton.gameObject.SetActive(false); // Hide button after click
                doorOpened = true; // Set flag to true

                // Trigger "Open" animation for this specific door
                doorAnimator.SetTrigger("Open");
            }
        }
    }
}
 

