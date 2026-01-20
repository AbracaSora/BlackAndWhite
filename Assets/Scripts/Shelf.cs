using UnityEngine;

public class Shelf : Interactable
{
    protected override void OnInteract()
    {
        int playthroughCount = PlayerPrefs.GetInt("Playthrough", 1);
        if (playthroughCount == 1)
        {
            if (interactCount == 0)
            {
                UIManager.Instance.ShowDialogue(new string[]
                {
                    "你走到衣柜前。",
                    "里面挂满了你儿时的衣服。",
                    "你已经穿不下了。",
                });
                interactCount++;
            }
            else
            {
                UIManager.Instance.ShowDialogue(new string[]
                {
                    "你再次走到衣柜前。",
                    "这些衣服依然静静地挂在那里，",
                    "仿佛在诉说着过去的故事。",
                });
            }
        }
    }
}
