using UnityEngine;

public class BaseDoor : Interactable
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
            _doorTrigger.EnterDoor();
        }
    }
}
