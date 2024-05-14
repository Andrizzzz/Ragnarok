using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lance
{
    public class OneWayPlatform : MonoBehaviour
    {
        public PlatformEffector2D effector;
        public float timeToStayOpen = 0.5f; // Adjust this value as needed
        private bool isOpen = false; // Start with platform closed

        void Start()
        {
            effector = GetComponent<PlatformEffector2D>();
        }

        void Update()
        {
            // Check if S key is pressed or joystick is moved down and the player is colliding with the platform
            if ((Input.GetKeyDown(KeyCode.S) || Input.GetAxis("Vertical") < 0) && IsPlayerColliding())
            {
                OpenPlatform(); // Open the platform
            }

            // If the platform is open, start countdown
            if (isOpen)
            {
                timeToStayOpen -= Time.deltaTime;

                if (timeToStayOpen <= 0)
                {
                    ClosePlatform(); // Close the platform after countdown
                }
            }
        }

        private bool IsPlayerColliding()
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(effector.transform.position, effector.GetComponent<BoxCollider2D>().size, 0f);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    return true; // Player is colliding with the platform
                }
            }
            return false; // Player is not colliding with the platform
        }

        private void OpenPlatform()
        {
            isOpen = true;
            effector.rotationalOffset = 180f; // Rotate the platform
            timeToStayOpen = 0.5f; // Reset the countdown timer
        }

        private void ClosePlatform()
        {
            isOpen = false;
            effector.rotationalOffset = 0f; // Reset the platform rotation
        }
    }
}