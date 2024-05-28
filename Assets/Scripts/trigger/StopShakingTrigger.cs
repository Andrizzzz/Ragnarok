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
        public GameObject continueButton;
        public GameObject dialoguePanel;
        public TMP_Text dialogueText;
        public TMP_Text characterNameText;
        public GameObject objectToDestroy;
        public Image characterImage;
        public GameObject backgroundPanel;
        public GameObject[] uiElementsToHide;

        [System.Serializable]
        public struct DialogueLine
        {
            public string characterName;
            public string text;
            public Sprite characterSprite;
        }

        public DialogueLine[] dialogueLines;
        private int currentLineIndex = 0;
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
                continueButton.SetActive(false);
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

            if (backgroundPanel != null)
            {
                backgroundPanel.SetActive(false);
            }
            else
            {
                Debug.LogError("BackgroundPanel reference is missing.");
            }

            if (characterImage == null)
            {
                Debug.LogError("CharacterImage reference is missing.");
            }

            if (characterNameText == null)
            {
                Debug.LogError("CharacterNameText reference is missing.");
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
            ShowDialogue(dialogueLines[currentLineIndex].characterName, dialogueLines[currentLineIndex].text, dialogueLines[currentLineIndex].characterSprite);

            if (stopButton != null)
            {
                stopButton.GetComponent<Button>().interactable = false;
            }

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
            if (cameraShaker != null)
            {
                cameraShaker.Stop();
                Debug.Log("Camera shaking stopped.");
            }
            else
            {
                Debug.LogWarning("CameraShaker reference is missing.");
            }

            if (stopButton != null)
            {
                stopButton.SetActive(false);
            }
        }

        private void ShowDialogue(string characterName, string message, Sprite characterSprite)
        {
            if (dialoguePanel != null && dialogueText != null && characterImage != null && backgroundPanel != null && characterNameText != null)
            {
                Time.timeScale = 0; // Pause the game
                dialoguePanel.SetActive(true);
                backgroundPanel.SetActive(true);
                characterImage.sprite = characterSprite;
                characterImage.gameObject.SetActive(true);
                characterNameText.text = characterName; // Set the character's name

                if (typingCoroutine != null)
                {
                    StopCoroutine(typingCoroutine);
                }
                typingCoroutine = StartCoroutine(TypeText(message));

                // Hide specified UI elements
                foreach (GameObject element in uiElementsToHide)
                {
                    if (element != null)
                    {
                        element.SetActive(false);
                    }
                }

                // Enable the continue button
                if (continueButton != null)
                {
                    continueButton.SetActive(true);
                }
            }
            else
            {
                Debug.LogError("DialoguePanel, DialogueText, CharacterImage, CharacterNameText, or BackgroundPanel reference is missing.");
            }
        }


        private IEnumerator TypeText(string message)
        {
            dialogueText.text = "";
            foreach (char letter in message.ToCharArray())
            {
                dialogueText.text += letter;
                yield return new WaitForSecondsRealtime(0.01f); // Use WaitForSecondsRealtime to respect the paused time scale
            }
        }

        private void OnContinueButtonClick()
        {
            currentLineIndex++;

            if (currentLineIndex < dialogueLines.Length)
            {
                ShowDialogue(dialogueLines[currentLineIndex].characterName, dialogueLines[currentLineIndex].text, dialogueLines[currentLineIndex].characterSprite);

                // Enable continue button again
                if (continueButton != null)
                {
                    continueButton.SetActive(true);
                }
            }
            else
            {
                if (dialoguePanel != null)
                {
                    dialoguePanel.SetActive(false);
                }
                if (backgroundPanel != null)
                {
                    backgroundPanel.SetActive(false);
                }
                if (continueButton != null)
                {
                    continueButton.SetActive(false);
                }
                Time.timeScale = 1; // Resume the game

                // Show specified UI elements
                foreach (GameObject element in uiElementsToHide)
                {
                    if (element != null)
                    {
                        element.SetActive(true);
                    }
                }
            }
        }
    }
}
