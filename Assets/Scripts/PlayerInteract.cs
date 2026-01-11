using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class PlayerInteract : MonoBehaviour
{
    private Interactable currentInteractable;
    private List<Interactable> interactables = new List<Interactable>();
    
    private void Update()
    {
        // 每帧更新当前交互物体
        currentInteractable = GetHighestPriorityInteractable();
        if (currentInteractable != null && Input.GetKeyDown(KeyCode.Z))
        {
            currentInteractable.Interact();
        }
    }
    
    // 获取当前交互范围内优先级最高的物体
    private Interactable GetHighestPriorityInteractable()
    {
        var interactable = interactables
            .OrderBy(obj => obj.priority)  // 按优先级排序，优先级小的排在前面
            .ThenBy(obj => Vector3.Distance(transform.position, obj.transform.position))  // 如果优先级相同，则按距离排序
            .FirstOrDefault();  // 获取优先级最高的物体

        return interactable;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        Debug.Log("Player entered interactable: " + interactable);
        if (interactable != null)
        {
            interactables.Add(interactable);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null && interactable == currentInteractable)
        {
            interactables.Remove(interactable);
        }
    }
}