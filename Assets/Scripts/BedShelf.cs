using UnityEngine;
using System.Collections;

public class BedShelf : Interactable
{
    protected override void OnInteract()
    {
        int playthroughCount = PlayerPrefs.GetInt("Playthrough", 1);
        bool writeDiary = PlayerPrefs.GetInt("WriteDiary", 0) == 1;
        if (playthroughCount == 1 && !writeDiary)
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
        } else if (playthroughCount == 1 && writeDiary)
        {
            UIManager.Instance.ShowDialogueWithOptions(new string[]
            {
                "要取走日记本吗？",
            }, new string[]
            {
                "取走",
                "放下",
            }, (optionIndex) =>
            {
                if (optionIndex == 0)
                {
                    UIManager.Instance.ShowDialogue(new string[]
                    {
                        "你取走了日记本。",
                    });
                    PlayerPrefs.SetInt("DiaryTaken", 1);
                }
                else
                {
                    UIManager.Instance.ShowDialogue(new string[]
                    {
                        "你决定把日记本留在原处。",
                    });
                }
            });
        }
    }
}
