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
        private List<Collider2D> collidersToIgnore = new List<Collider2D>();

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
                // Ignore collision between the trigger collider and the player
                Physics2D.IgnoreCollision(triggerCollider, other, true);
                // Add the collider to the list of colliders to ignore
                collidersToIgnore.Add(other);
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (!doorOpened && other.CompareTag("Player"))
            {
                Debug.Log("Player exited the trigger area.");
                interactButton.gameObject.SetActive(false);
                // Re-enable collision between the trigger collider and the player
                collidersToIgnore.Remove(other);
                foreach (Collider2D collider in collidersToIgnore)
                {
                    Physics2D.IgnoreCollision(triggerCollider, collider, false);
                }
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

                // Disable collision between the player and barrier collider
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    Physics2D.IgnoreCollision(barrierCollider, player.GetComponent<Collider2D>());
                }

                triggerCollider.enabled = false;

                doorAnimator.SetTrigger("Open");
            }
            else
            {
                triggerCollider.enabled = !triggerCollider.enabled;
            }
        }
    }
}
