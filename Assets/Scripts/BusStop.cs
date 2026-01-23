using UnityEngine;

public class BusStop : Interactable
{
    protected DoorTrigger _doorTrigger;
    
    [TextArea(3,10)]
    public string promptText;
    
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
                promptText,
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