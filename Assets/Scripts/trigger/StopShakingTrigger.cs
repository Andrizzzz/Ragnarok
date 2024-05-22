using UnityEngine;
using UnityEngine.UI; // Add this to access the UI components
using FirstGearGames.SmoothCameraShaker;

namespace Lance
{
    public class StopShakingTrigger : MonoBehaviour
    {
        public CameraShaker cameraShaker; // Reference to the CameraShaker component
        public GameObject stopButton; // Reference to the Button UI GameObject

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
                    buttonComponent.onClick.AddListener(StopShaking);
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
    }
}
