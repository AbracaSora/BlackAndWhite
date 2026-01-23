using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ChengDisappear : Mine
{
    public Object chengObject;
    ShadowMaterialController shadowMaterialController;

    void Start()
    {
        shadowMaterialController = chengObject.GetComponent<ShadowMaterialController>();
    }
    
    IEnumerator GhostAppearThenDialogue()
    {
        // 1. 禁用玩家操作
        PlayerMove.Instance.ForbidMovement();

        // 2. 幻影淡入
        yield return StartCoroutine(shadowMaterialController.SuddenOutGhost());

        // 3. 可选：给玩家 0.3 秒“看清楚”
        yield return new WaitForSeconds(0.3f);

        // 4. 显示对话
        UIManager.Instance.ShowDialogue(dialogueLines);
    }
    protected override void OnInteract()
    {
        if (PlayerPrefs.GetInt("WriteDiary", 0) == 1)
        {
            return;
        }
        StartCoroutine(GhostAppearThenDialogue());
    }
}