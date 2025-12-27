using UnityEngine;

public class Shelf : Interactable
{
    protected override void OnInteract()
    {
        UIManager.Instance.ShowDialogue("柜子里空无一物");
    }
}
