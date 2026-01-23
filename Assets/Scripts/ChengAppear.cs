using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ChengAppear : Mine
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

        // 2. 幻影淡入
        yield return StartCoroutine(shadowMaterialController.FadeInGhost(0.5f));

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
