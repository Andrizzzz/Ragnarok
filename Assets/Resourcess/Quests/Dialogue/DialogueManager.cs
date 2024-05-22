using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Lance;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public Button nextButton;
    public Button acceptButton; // Accept button
    public Button noButton; // No button
    public Button yesButton; // Yes button
    public Button declineButton; // Decline button

    private string[] dialogueLines;
    private int currentLineIndex;
    private Coroutine typingCoroutine;
    private bool isProgressedDialogue;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        dialoguePanel.SetActive(false);

        if (nextButton != null)
        {
            nextButton.onClick.AddListener(DisplayNextLine);
        }

        if (acceptButton != null)
        {
            acceptButton.onClick.AddListener(OnAccept);
            acceptButton.gameObject.SetActive(false);
        }

        if (noButton != null)
        {
            noButton.onClick.AddListener(OnNo);
            noButton.gameObject.SetActive(false);
        }

        if (yesButton != null)
        {
            yesButton.onClick.AddListener(OnYes);
            yesButton.gameObject.SetActive(false);
        }

        if (declineButton != null)
        {
            declineButton.onClick.AddListener(OnDecline);
            declineButton.gameObject.SetActive(false);
        }
    }

    public void StartDialogue(DialogueSO dialogue, bool isProgressed = false)
    {
        dialogueLines = dialogue.dialogueLines;
        currentLineIndex = 0;
        dialoguePanel.SetActive(true);
        isProgressedDialogue = isProgressed;
        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if (currentLineIndex < dialogueLines.Length)
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            typingCoroutine = StartCoroutine(TypeText(dialogueLines[currentLineIndex]));
            currentLineIndex++;
        }
        else
        {
            if (isProgressedDialogue)
            {
                acceptButton.gameObject.SetActive(true);
                noButton.gameObject.SetActive(true);
                yesButton.gameObject.SetActive(true);
                declineButton.gameObject.SetActive(true);
            }
            else
            {
                EndDialogue();
            }
        }
    }

    private IEnumerator TypeText(string text)
    {
        dialogueText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f); // Adjust the speed as needed
        }
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        dialogueLines = null;
    }

    private void OnAccept()
    {
        acceptButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
        yesButton.gameObject.SetActive(false);
        declineButton.gameObject.SetActive(false);
        EndDialogue();
        NpcQuest.instance.OnDialogueAccepted();
    }

    private void OnNo()
    {
        acceptButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
        yesButton.gameObject.SetActive(false);
        declineButton.gameObject.SetActive(false);
        EndDialogue();
        NpcQuest.instance.OnDialogueRejected();
    }

    private void OnYes()
    {
        acceptButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
        yesButton.gameObject.SetActive(false);
        declineButton.gameObject.SetActive(false);
        EndDialogue();
        NpcQuest.instance.OnDialogueConfirmed();
    }

    private void OnDecline()
    {
        acceptButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
        yesButton.gameObject.SetActive(false);
        declineButton.gameObject.SetActive(false);
        EndDialogue();
        NpcQuest.instance.OnDialogueDeclined();
    }
}
