using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "ScriptableObjects/DialogueSO", order = 1)]
public class DialogueSO : ScriptableObject
{
    [TextArea(3, 10)]
    public string[] dialogueLines;
}
