using UnityEngine;

public class Shelf : Interactable
{
    protected override void OnInteract()
    {
        UIManager.Instance.ShowDialogue(new string[]
        {
            "你打开了柜子，发现里面空无一物。",
            "也许可以去别的地方看看。"
        });
    }
}
