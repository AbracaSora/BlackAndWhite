using UnityEngine;

public class UIManager : MonoBehaviour
{
    public DialogueController dialogue;

    public bool IsDialogueOpen => dialogue.gameObject.activeSelf;

    public void ShowDialogue(string text)
    {
        dialogue.Show(text);
    }
}

