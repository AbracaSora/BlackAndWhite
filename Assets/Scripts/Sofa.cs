using UnityEngine;

public class Sofa : Interactable
{
    protected override void OnInteract()
    {
        UIManager.Instance.ShowDialogue(new string[]
        {
            "你坐在沙发上，感到一阵放松。",
            "或许可以休息一会儿。"
        });
    }
}
