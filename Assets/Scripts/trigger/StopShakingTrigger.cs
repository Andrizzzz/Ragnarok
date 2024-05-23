using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using FirstGearGames.SmoothCameraShaker;

namespace Lance
{
    public class StopShakingTrigger : MonoBehaviour
    {
        public CameraShaker cameraShaker; 
        public GameObject stopButton; 
        public GameObject dialoguePanel;
        public TMP_Text dialogueText;
        public GameObject objectToDestroy; 

        private void Start()
        {
            if (stopButton != null)
            {
                stopButton.SetActive(false);

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
            if (other.CompareTag("Player")) 
            {
                Debug.Log("Player entered the stop shaking trigger.");

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

            // Disable the button to prevent further clicks
            if (stopButton != null)
            {
                stopButton.GetComponent<Button>().interactable = false;
            }

            // Destroy the object containing this script
            if (objectToDestroy != null)
            {
                Destroy(objectToDestroy);
            }
            else
            {
                Debug.LogWarning("Object to destroy is not assigned.");
            }
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
