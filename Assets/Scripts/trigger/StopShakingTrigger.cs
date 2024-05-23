using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FirstGearGames.SmoothCameraShaker;
using System.Collections;

namespace Lance
{
    public class StopShakingTrigger : MonoBehaviour
    {
        public CameraShaker cameraShaker;
        public GameObject stopButton;
        public GameObject continueButton; // Add reference to the continue button
        public GameObject dialoguePanel;
        public TMP_Text dialogueText;
        public GameObject objectToDestroy;

        private string[] dialogueLines; // Array to store dialogue lines
        private int currentLineIndex = 0; // Index to keep track of current dialogue line
        private Coroutine typingCoroutine;

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

            if (continueButton != null)
            {
                continueButton.SetActive(false); // Hide continue button initially
                Button continueButtonComponent = continueButton.GetComponent<Button>();
                if (continueButtonComponent != null)
                {
                    continueButtonComponent.onClick.AddListener(OnContinueButtonClick);
                }
                else
                {
                    Debug.LogError("Button component is missing on continueButton GameObject.");
                }
            }
            else
            {
                Debug.LogError("ContinueButton reference is missing.");
            }

            if (dialoguePanel != null)
            {
                dialoguePanel.SetActive(false);
            }
            else
            {
                Debug.LogError("DialoguePanel reference is missing.");
            }

            // Initialize dialogue lines
            dialogueLines = new string[]
            {
                "Thank you, brave soul! By absorbing these seismic waves, you've averted a catastrophe.",
                "The waves were emanating from this very core, threatening to destabilize the entire region.",
                "Seismic waves? Can you tell me more about them?",
                "Of course. Seismic waves are energy waves caused by sudden movements in the Earth's crust. There are two main types: body waves and surface waves.",
                "Body waves travel through the Earth's interior and are divided into P-waves and S-waves. P-waves are faster and can move through both solid and liquid layers of the Earth, while S-waves are slower and only travel through solids.",
                "Surface waves travel along the Earth's surface and are usually the cause of the most damage during an earthquake. They move the ground up and down or side to side.",
                "So the waves here were causing instability in the core?",
                "Exactly. If left unchecked, these waves would have grown more violent, causing massive earthquakes. The tremors would have propagated to the surface, potentially causing catastrophic destruction.",
                "I'm glad I could help. What would have happened if I hadn't absorbed the waves?",
                "The quakes could have torn the land apart, destroying cities, infrastructure, and taking countless lives. The entire region could have been rendered uninhabitable.",
                "I'm relieved we avoided that fate. The core is now stable, thanks to you.",
                "Your courage and quick action have saved us all. We owe you our deepest gratitude. Understanding the nature of seismic waves and acting quickly has made all the difference."
            };
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
            ShowDialogue(dialogueLines[currentLineIndex]);

            // Disable the button to prevent further clicks
            if (stopButton != null)
            {
                stopButton.GetComponent<Button>().interactable = false;
            }

            // Show continue button after displaying the dialogue line
            if (continueButton != null)
            {
                continueButton.SetActive(true);
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
                if (typingCoroutine != null)
                {
                    StopCoroutine(typingCoroutine);
                }
                typingCoroutine = StartCoroutine(TypeText(message));
            }
            else
            {
                Debug.LogError("DialoguePanel or DialogueText reference is missing.");
            }
        }

        private IEnumerator TypeText(string message)
        {
            dialogueText.text = "";
            foreach (char letter in message.ToCharArray())
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(0.01f); // Adjust typing speed here
            }
        }

        private void OnContinueButtonClick()
        {
            // Increment index to display the next dialogue line
            currentLineIndex++;

            // Check if there are more dialogue lines to display
            if (currentLineIndex < dialogueLines.Length)
            {
                ShowDialogue(dialogueLines[currentLineIndex]);
            }
            else
            {
                // Hide dialogue panel and continue button when all lines are displayed
                if (dialoguePanel != null)
                {
                    dialoguePanel.SetActive(false);
                }
                if (continueButton != null)
                {
                    continueButton.SetActive(false);
                }
            }
        }
    }
}