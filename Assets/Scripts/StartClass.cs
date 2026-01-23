using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartClass : Interactable
{
    public SceneFade sceneFade; // 连接到SceneFade脚本
    protected override void OnInteract()
    {
        StartCoroutine(Class());
        PlayerPrefs.SetInt("WriteDiary", 1);
    }

    private IEnumerator Class()
    {
        // 进入黑屏
        yield return StartCoroutine(sceneFade.FadeOut());
        
        UIManager.Instance.ShowDialogue(new []
        {
            "（课堂还是是那么无聊。）",
            "（黑板上写了什么，我已经不太记得了。）",
            "（老师的声音，一直在说话。）",
            "（但那些句子，没有留下来。）",

            "（我记得自己坐在那里。）",
            "（记得翻过书页。）",
            "（记得下课铃响了。）",

            "（除此之外，什么都没有。）",
            "（……）",
            "（回去把今天记录在日记本上吧。）",
        });
        yield return new WaitForSeconds(10f);
        yield return StartCoroutine(sceneFade.FadeIn());
    }
}