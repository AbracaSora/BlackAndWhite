using UnityEngine;

public class BusStop : Interactable
{
    protected DoorTrigger _doorTrigger;
    
    void Awake()
    {
        _doorTrigger = GetComponent<DoorTrigger>();
    }
    protected override void OnInteract()
    {
        if (_doorTrigger != null)
        {
            UIManager.Instance.ShowDialogueWithOptions(new []
            {
                "是否坐公交车去学校？",
            }, new []
            {
                "是",
                "否",
            }, (choice) =>
            {
                if (choice == 0)
                {
                    _doorTrigger.EnterDoor();
                }
            });
        }
    }
}