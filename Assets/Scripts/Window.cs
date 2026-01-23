using UnityEngine;

public class Window : Interactable
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
                    "阳光明媚。",
                });
                interactCount++;
            }
            else
            {
                UIManager.Instance.ShowDialogue(new string[]
                {
                    "时候不早了，",
                    "该起程了。",
                });
            }
        }
    }
}