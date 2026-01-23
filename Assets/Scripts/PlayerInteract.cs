using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class PlayerInteract : MonoBehaviour
{
    private Interactable currentInteractable;
    private HashSet<Interactable> interactables = new();
    
    private void Update()
    {
        // 每帧更新当前交互物体
        currentInteractable = GetHighestPriorityInteractable();
        if (currentInteractable != null && Input.GetKeyDown(KeyCode.Z))
        {
            currentInteractable.Interact();
        }
    }

    float FaceAt(Interactable interactable)
    {
        if (interactable.IsFaceless())
        {
            return 1f;
        }
        Vector2 playerFacing = PlayerMove.Instance.FacingVector();
        Vector2 toTarget = interactable.GetInteractionPoint().position - transform.position;
        toTarget.Normalize();
        float dot = Vector2.Dot(playerFacing, toTarget.normalized);
        return dot;
    }
    // 获取当前交互范围内优先级最高的物体
    private Interactable GetHighestPriorityInteractable()
    {
        var interactable = interactables
            .OrderBy(obj => obj.priority)  // 按优先级排序，优先级小的排在前面
            .ThenByDescending(FaceAt)  // 按面向角度排序，面向的排在前面
            .ThenBy(obj => Vector3.Distance(transform.position, obj.GetInteractionPoint().position))  // 如果角度相同，则按距离排序
            .FirstOrDefault();  // 获取优先级最高的物体
        if (interactable == null || FaceAt(interactable) < 0.1f)
        {
            return null;
        }
        return interactable;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Mine>() != null)
        {
            Mine mine = other.GetComponent<Mine>(); 
            mine.Interact();
            return;
        }
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null)
        {
            interactables.Add(interactable);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null)
        {
            interactables.Remove(interactable);
        }
    }
}