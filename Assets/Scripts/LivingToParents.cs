using UnityEngine;

public class LivingToParents : BaseDoor
{
    protected override void OnInteract()
    {
        int currentPlaythrough = PlayerPrefs.GetInt("Playthrough", 1);
        if (currentPlaythrough >= 3)
        {
            UIManager.Instance.ShowDialogue(new string[]
            {
                "你走向父母的房间，准备面对他们。",
                "也许这次你能找到答案。"
            });
            _doorTrigger.EnterDoor();
        }
        else
        {
            UIManager.Instance.ShowDialogue(new string[]
            {
                "你试图进入父母的房间。",
                "锁住了。",
            });
        }
    }
}
