using UnityEngine;
using System.Collections;

public class BedShelf : Interactable
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
                    "床头柜里摆放着一些书籍。",
                });
                interactCount++;
            }
            else if (interactCount == 1)
            {
                UIManager.Instance.ShowDialogue(new string[]
                {
                    "柜上的花是何时间插上的？",
                    "你不记得了。",
                });
                interactCount++;
            }
            else
            {
                UIManager.Instance.ShowDialogueWithOptions(new string[]
                {
                    "取走花朵吗？",
                }, new string[]
                {
                    "取走",
                    "不取",
                },ResolveDialogueOption);
            }
        }
    }
    
    private void ResolveDialogueOption(int optionIndex)
    {
        if (optionIndex == 0) // 取走
        {
            UIManager.Instance.ShowDialogue(new string[]
            {
                "你小心翼翼地取下花朵，",
                "放进了口袋。",
            });
            // 这里可以添加将花朵添加到玩家物品栏的逻辑
        }
        else // 不取
        {
            UIManager.Instance.ShowDialogue(new string[]
            {
                "你决定不取走花朵，",
                "它静静地摆在那里。",
            });
        }
    }
}
