using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    public TMP_Text dialogueText;
    public float charInterval = 0.04f;

    private string fullText;
    private Coroutine typingCoroutine;
    private bool isTyping;

    void Awake()
    {
        dialogueText.text = "";
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (!gameObject.activeSelf) return;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (isTyping)
            {
                // 还在打字 → 直接显示全文
                StopCoroutine(typingCoroutine);
                dialogueText.text = fullText;
                isTyping = false;
            }
            else
            {
                // 已打完 → 关闭
                Close();
            }
        }
    }

    public void Show(string text)
    {
        gameObject.SetActive(true);
        fullText = text;
        dialogueText.text = "";

        typingCoroutine = StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        isTyping = true;

        foreach (char c in fullText)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(charInterval);
        }

        isTyping = false;
    }

    void Close()
    {
        dialogueText.text = "";
        gameObject.SetActive(false);
    }
}