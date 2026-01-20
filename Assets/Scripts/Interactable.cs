using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [Header("Interactable Base")]
    [SerializeField] protected bool canInteract = true;
    [SerializeField] protected bool interactOnce = false;
    [SerializeField] protected int interactCount = 0;
 
    private bool hasInteracted = false;
    
    public int priority = 0;
    
    /// <summary>
    /// 对外唯一入口（Player 只能调用这个）
    /// </summary>
    public void Interact() {
        if (!CanInteract())
            return;

        OnInteract();

        if (interactOnce)
            hasInteracted = true;
    }

    /// <summary>
    /// 是否允许交互（子类可扩展条件）
    /// </summary>
    protected virtual bool CanInteract()
    {
        if (!canInteract)
            return false;

        if (interactOnce && hasInteracted)
            return false;

        return true;
    }

    /// <summary>
    /// 具体交互逻辑（子类必须实现）
    /// </summary>
    protected abstract void OnInteract();
}