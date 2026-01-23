using UnityEngine;
using UnityEngine.UI;

public class BedroomToLivingroom : BaseDoor
{
    protected override void OnInteract()
    {
        _doorTrigger.EnterDoor();
    }
}
