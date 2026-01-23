using UnityEngine;
using System.Collections;

public class BedroomDesk : Interactable
{
    protected override void OnInteract()
    {
        int playthroughCount = PlayerPrefs.GetInt("Playthrough", 1);
        bool writeDiary = PlayerPrefs.GetInt("WriteDiary", 0) == 1;
        bool diaryTaken = PlayerPrefs.GetInt("DiaryTaken", 0) == 1;
        if (playthroughCount == 1 && !writeDiary)
        {
            UIManager.Instance.ShowDialogue(new string[]
            {
                "书桌上整齐地摆放着学习用品。",
            });
        } else if (playthroughCount == 1 && writeDiary)
        {
            if (!diaryTaken)
            {
                UIManager.Instance.ShowDialogue(new string[]
                {
                    "先去床头柜取走日记本吧。",
                });
                return;
            }
            UIManager.Instance.ShowDialogue(new string[]
            {
                "打开了日记本，翻到最后一页。",
                "你开始提笔写下——",
                "「最近写下的东西，越来越不像是日记了。」",
                "「有些内容，我甚至不记得是在什么时候写的。」",

                "「明明发生了不少奇怪的事。」",
                "「可每次落笔的时候，还是会下意识地把它们略过去。」",
                "「就像是……不去写下来，就能当作没有发生过一样。」",

                "……？",
                "一张照片，从日记里滑了出来。",

                "照片上，是两个年纪相仿的女孩。",
                "站得很近，像是习惯了彼此的存在。",

                "她盯着看了一会儿，才意识到——",
                "那两张脸，几乎一模一样。",

                "只有头发和眼睛的颜色不同。",

                "「……原来是这样。」",

                "你翻到日记的最后一页。",
                "「我想起来了。」"
            });
        }
    }
}