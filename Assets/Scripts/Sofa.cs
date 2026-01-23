using UnityEngine;

public class Sofa : Interactable
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
                    "柔软的沙发。",
                    "每天放学后都会坐在这里。",
                });
                interactCount++;
            }
            else
            {
                UIManager.Instance.ShowDialogue(new string[]
                {
                    "你已经坐过无数次了，",
                    "它还是老样子。",
                });
            }
        }
    }
}
