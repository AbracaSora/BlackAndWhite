using UnityEngine;

public class Bed : Interactable
{
    protected override void OnInteract()
    {
        UIManager.Instance.ShowDialogue("你躺在床上，感到一阵疲惫。");
    }
}
