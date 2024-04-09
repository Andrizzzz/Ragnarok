using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace DialogueSystem
{
    public class DialogueBaseClass : MonoBehaviour
    {
        protected IEnumerator WriteText(string input, Text textHolder, Color textColor, Font textFont, float delay)
        {
            textHolder.color = textColor; // Set the text color
            textHolder.font = textFont; // Set the text font

            for (int i = 0; i < input.Length; i++)
            {
                textHolder.text += input[i];
                yield return new WaitForSeconds(delay);
            }
        }
    }
}

