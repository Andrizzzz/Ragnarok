using UnityEngine;
using UnityEngine.UI;

namespace Lance
{
    public class ColliderToggle : MonoBehaviour
    {
        public GameObject targetObject;       // The object whose collider you want to disable
        public Collider2D doorCollider;       // Collider attached to your door
        public Button yourButton;             // Reference to your UI button
        public Animator buttonAnimator;       // Reference to the Animator component attached to your button
        public Animator targetAnimator;       // Reference to the Animator component attached to your target object

        private bool buttonUsed = false;      // Flag to track if the button has been clicked
        private bool isCloseToTarget = false; // Flag to track if the player is close to the target object

        void Start()
        {
            if (yourButton != null)
            {
                yourButton.gameObject.SetActive(false); // Initially hide the button
                yourButton.onClick.AddListener(OnButtonClick);
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !buttonUsed) // Ensure your player is tagged correctly and button not already used
            {
                yourButton.gameObject.SetActive(true); // Show the button when the player enters the trigger
            }
            else if (other.CompareTag("Player"))
            {
                // Player is close to the target object
                isCloseToTarget = true;
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player")) // Ensure your player is tagged correctly
            {
                yourButton.gameObject.SetActive(false); // Hide the button when the player exits the trigger
                isCloseToTarget = false; // Player is no longer close to the target object
            }
        }

        void OnButtonClick()
        {
            Collider2D collider = targetObject.GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false; // Disable the collider
            }

            // Trigger the animations using Animator's SetTrigger method
            buttonAnimator.SetTrigger("Open");
            targetAnimator.SetTrigger("OpenDoor"); // Trigger animation for the target object

            // Optionally hide the button after clicking
            yourButton.gameObject.SetActive(false);

            buttonUsed = true; // Set the flag to true indicating the button has been used
        }
    }
}
