using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lance
{
    public class DoorInteraction : MonoBehaviour
    {
        public Button interactButton;
        public Collider2D triggerCollider;
        public Collider2D barrierCollider;
        public Animator doorAnimator;

        private bool doorOpened = false;

        void Start()
        {
            interactButton.gameObject.SetActive(false);
            interactButton.onClick.AddListener(OnButtonClick);


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
            if (!doorOpened && other.CompareTag("Player"))
            {
                Debug.Log("Player entered the trigger area.");
                interactButton.gameObject.SetActive(true);
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (!doorOpened && other.CompareTag("Player"))
            {
                Debug.Log("Player exited the trigger area.");
                interactButton.gameObject.SetActive(false);
            }
        }

        public void OnButtonClick()
        {
            if (!doorOpened)
            {
                Debug.Log("Button clicked. Door opening.");
                barrierCollider.enabled = false;
                interactButton.gameObject.SetActive(false);
                doorOpened = true;

                // Disable the trigger collider when the door is opened
                triggerCollider.enabled = false;

                doorAnimator.SetTrigger("Open");
            }
            else
            {
                // If the door is already opened, toggle the trigger collider state
                triggerCollider.enabled = !triggerCollider.enabled;
            }
        }


    }
}