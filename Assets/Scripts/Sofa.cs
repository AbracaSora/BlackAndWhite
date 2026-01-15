using UnityEngine;

public class Sofa : Interactable
{
    private int _interactCount = 0;
    protected override void OnInteract()
    {
        int playthroughCount = PlayerPrefs.GetInt("Playthrough", 1);
        if (playthroughCount == 1)
        {
            if (_interactCount == 0)
            {
                UIManager.Instance.ShowDialogue(new string[]
                {
                    "柔软的沙发。",
                    "每天放学后都会坐在这里。",
                });
                _interactCount++;
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
