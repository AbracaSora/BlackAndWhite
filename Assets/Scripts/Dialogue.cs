using UnityEngine;

public class Dialogue : Interactable
{
    public string[] dialogueLines;

    protected override void OnInteract()
    {
        UIManager.Instance.ShowDialogue(dialogueLines);
    }
}
