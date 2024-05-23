using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Lance
{
    public class NPCTutorial : MonoBehaviour
    {
        public GameObject dialoguePanel;
        public Text dialogueText;
        public DialogueText[] dialogueTexts;
        private int dialogueIndex;
        private int lineIndex;

        public float wordSpeed;
        private bool playerIsClose;
        private bool dialogueStarted;
        private bool dialogueFinished;

        private bool isTyping;
        private bool skipCurrentSentence;

        public Image overlayImage;
        public Collider2D dialogueCollider;

        [System.Serializable]
        public class DialogueText
        {
            public string[] dialogueLines;
            public GameObject[] uiToShow;
            public GameObject[] uiToHide;
        }

        private void Start()
        {
            dialogueStarted = false;
            dialogueFinished = false;
            if (overlayImage != null)
            {
                overlayImage.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (playerIsClose && !dialogueStarted && !dialogueFinished)
            {
                StartDialogue();
            }

            if (dialogueStarted && (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0)))
            {
                if (isTyping)
                {
                    skipCurrentSentence = true;
                }
                else
                {
                    NextDialogue();
                }
            }
        }

        private void StartDialogue()
        {
            Time.timeScale = 0;
            if (overlayImage != null)
            {
                overlayImage.gameObject.SetActive(true);
            }
            dialoguePanel.SetActive(true);
            dialogueStarted = true;
            dialogueIndex = 0;
            lineIndex = 0;
            StartCoroutine(Typing());
            if (dialogueCollider != null)
            {
                dialogueCollider.enabled = false;
            }

            // Show UI elements specified for the current dialogue text
            ShowUIForCurrentDialogue();
        }

        private void ShowUIForCurrentDialogue()
        {
            if (dialogueIndex < dialogueTexts.Length)
            {
                foreach (GameObject uiElement in dialogueTexts[dialogueIndex].uiToShow)
                {
                    uiElement.SetActive(true);
                }
                // Hide UI elements not specified for the current dialogue text
                foreach (GameObject uiElement in dialogueTexts[dialogueIndex].uiToHide)
                {
                    uiElement.SetActive(false);
                }
            }
        }
        private IEnumerator Typing()
        {
            if (isTyping) yield break;

            isTyping = true;
            dialogueText.text = "";

            string[] currentDialogue = dialogueTexts[dialogueIndex].dialogueLines;
            int dialogueLength = currentDialogue.Length;

            for (int i = lineIndex; i < dialogueLength; i++)
            {
                if (skipCurrentSentence)
                {
                    dialogueText.text = currentDialogue[i];
                    break;
                }

                dialogueText.text = currentDialogue[i];
                yield return new WaitForSecondsRealtime(wordSpeed);

                // Move lineIndex increment here to properly display all lines
                lineIndex++;

                // Check if it's the last dialogue line
                if (lineIndex == dialogueLength - 1)
                {
                    NextDialogue();
                }
            }

            isTyping = false;
            skipCurrentSentence = false;
        }



        private void NextDialogue()
        {
            if (dialogueIndex < dialogueTexts.Length - 1)
            {
                dialogueIndex++;
                lineIndex = 0;
                StartCoroutine(Typing());
                ShowUIForCurrentDialogue();
            }
            else
            {
                EndDialogue();
                dialogueFinished = true;
            }
        }

        private void EndDialogue()
        {
            Time.timeScale = 1;
            dialoguePanel.SetActive(false);
            dialogueStarted = false;
            StopCoroutine(Typing()); // Ensure the typing coroutine is stopped when ending the dialogue
            if (overlayImage != null)
            {
                overlayImage.gameObject.SetActive(false);
            }

            // Show all UI elements that were hidden during dialogue
            ShowAllUI();
        }

        private void ShowAllUI()
        {
            foreach (DialogueText dialogueText in dialogueTexts)
            {
                foreach (GameObject uiElement in dialogueText.uiToHide)
                {
                    uiElement.SetActive(true);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                playerIsClose = true;
                dialogueText.text = "";
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                playerIsClose = false;
            }
        }
    }
}
