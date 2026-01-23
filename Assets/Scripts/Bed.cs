using UnityEngine;

public class Bed : Interactable
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
                    "对你来说有些宽大的床。",
                });
                interactCount++;
            }
            else
            {
                UIManager.Instance.ShowDialogue(new string[]
                {
                    "你刚刚从床上起来，",
                    "不需要再躺下去了。",
                });
            }
        }
    }
}
