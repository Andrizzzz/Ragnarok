using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogue;
    private int index;

    public GameObject contButton;
    public float wordSpeed;
    public bool playerIsClose;

    public PlayerController playerController;

    
    void Update()
    {
        if (playerIsClose)
        {
            if(!dialoguePanel.activeInHierarchy)
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
                if(playerController != null)
                    playerController.enabled = false;
            }
            if (dialogueText.text == dialogue[index])
            {
                contButton.SetActive(true);
            }
        }
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index= 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {

        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            contButton.SetActive(false); // Hide the continue button
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
        }
    }
}
