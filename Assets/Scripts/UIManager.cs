using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    public DialogueController dialogue;

    public bool IsDialogueOpen => dialogue.gameObject.activeSelf;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowDialogue(string text)
    {
        dialogue.Show(text);
    }
}

