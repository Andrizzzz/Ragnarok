using UnityEngine;
using TMPro;
using System.Collections;

public class TypeWriterEffects : MonoBehaviour
{
    public TMP_Text textComponent;
    public float delay = 0.1f;

    private string originalText;
    private string currentText;

    private void Awake()
    {
        originalText = textComponent.text;
        textComponent.text = "";
    }

    public void StartTyping()
    {
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        for (int i = 0; i < originalText.Length; i++)
        {
            currentText = originalText.Substring(0, i + 1);
            textComponent.text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }

    public void ResetText()
    {
        textComponent.text = "";
    }
}
