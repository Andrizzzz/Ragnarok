using UnityEngine;
using UnityEngine.UI; // Add this to access the UI components
using TMPro; // Add this to access TextMeshPro components
using FirstGearGames.SmoothCameraShaker;

namespace Lance
{
    public class StopShakingTrigger : MonoBehaviour
    {
        public CameraShaker cameraShaker; // Reference to the CameraShaker component
        public GameObject stopButton; // Reference to the Button UI GameObject
        public GameObject dialoguePanel; // Reference to the dialogue panel GameObject
        public TMP_Text dialogueText; // Reference to the TMP_Text component

        private void Start()
        {
            if (stopButton != null)
            {
                // Ensure the button is initially inactive
                stopButton.SetActive(false);

                // Add the listener for the button click
                Button buttonComponent = stopButton.GetComponent<Button>();
                if (buttonComponent != null)
                {
                    buttonComponent.onClick.AddListener(OnStopButtonClick);
                }
                else
                {
                    Debug.LogError("Button component is missing on stopButton GameObject.");
                }
            }
            else
            {
                Debug.LogError("StopButton reference is missing.");
            }

            // Ensure the dialogue panel is initially inactive
            if (dialoguePanel != null)
            {
                dialoguePanel.SetActive(false);
            }
            else
            {
                Debug.LogError("DialoguePanel reference is missing.");
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player")) // Replace "Player" with the tag of the object that triggers the camera shaking stop
            {
                Debug.Log("Player entered the stop shaking trigger.");

                // Activate the stop button
                if (stopButton != null)
                {
                    stopButton.SetActive(true);
                }
            }
        }

        private void OnStopButtonClick()
        {
            StopShaking();
            ShowDialogue("Shaking stopped.");
        }

        private void StopShaking()
        {
            // Stop the camera shaking
            if (cameraShaker != null)
            {
                cameraShaker.Stop();
                Debug.Log("Camera shaking stopped.");
            }
            else
            {
                Debug.LogWarning("CameraShaker reference is missing.");
            }

            // Deactivate the button after stopping the shaking
            if (stopButton != null)
            {
                stopButton.SetActive(false);
            }
        }

        private void ShowDialogue(string message)
        {
            if (dialoguePanel != null && dialogueText != null)
            {
                dialoguePanel.SetActive(true);
                dialogueText.text = message;
            }
            else
            {
                Debug.LogError("DialoguePanel or DialogueText reference is missing.");
            }
        }
    }
}

