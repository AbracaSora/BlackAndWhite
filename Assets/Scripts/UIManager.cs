using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    public DialogueController dialogue;

    public bool IsDialogueOpen  { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void ShowDialogue(string[] text)
    {
        dialogue.Show(text);
    }
    
    public void ShowDialogueWithOptions(
        string[] text,
        string[] options,
        Action<int> onOptionSelected
    )
    {
        dialogue.ShowWithOptions(
            text,
            new System.Collections.Generic.List<string>(options),
            onOptionSelected
        );
    }
}

