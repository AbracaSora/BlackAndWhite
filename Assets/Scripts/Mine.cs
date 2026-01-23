using System;
using UnityEngine;

public class Mine : Interactable
{
    [TextArea(3,10)]
    public string[] dialogueLines;


    protected override void OnInteract()
    {
        if (PlayerPrefs.GetInt("WriteDiary", 0) == 1)
        {
            return;
        }
        UIManager.Instance.ShowDialogue(dialogueLines);
    }
}
