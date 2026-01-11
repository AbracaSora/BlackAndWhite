using System.Collections;
using Art.Characters.Gen;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    public TMP_Text dialogueText;
    public float charInterval = 0.04f;

    private string[] dialogueLines;  // 存储多句对话的数组
    private int currentLineIndex = 0;  // 当前对话句子索引
    private Coroutine typingCoroutine;
    private bool isTyping;
    private PlayerMove _playerMove;
    private PlayerInteract _playerInteract;

    void Awake()
    {
        dialogueText.text = "";
        gameObject.SetActive(false);
        _playerMove = FindFirstObjectByType<PlayerMove>();
        _playerInteract = FindFirstObjectByType<PlayerInteract>();
    }

    void Update()
    {
        if (!gameObject.activeSelf) return;
        

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (isTyping)
            {
                // 如果还在打字，按Z则显示当前句子的完整文本
                StopCoroutine(typingCoroutine);
                dialogueText.text = dialogueLines[currentLineIndex];
                isTyping = false;
            }
            else
            {
                // 当前句子已完成，继续显示下一句
                currentLineIndex++;

                if (currentLineIndex < dialogueLines.Length)
                {
                    // 显示下一句对话
                    typingCoroutine = StartCoroutine(TypeText(dialogueLines[currentLineIndex]));
                }
                else
                {
                    // 所有对话已结束，关闭对话框
                    Close();
                }
            }
        }
    }

    // 显示多句对话
    public void Show(string[] lines)
    {
        dialogueLines = lines;
        currentLineIndex = 0;
        gameObject.SetActive(true);
        dialogueText.text = "";
        
        if (_playerMove != null)
        {
            _playerMove.enabled = false; // 禁止玩家移动
        }
        
        if (_playerInteract != null)
        {
            _playerInteract.enabled = false; // 禁止玩家交互
        }

        // 开始显示第一句对话
        typingCoroutine = StartCoroutine(TypeText(dialogueLines[currentLineIndex]));
    }

    // 用于逐字显示对话的协程
    IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogueText.text = "";  // 清空文本框

        foreach (char c in text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(charInterval);
        }

        isTyping = false;  // 打字完毕
    }

    // 关闭对话框
    void Close()
    {
        dialogueText.text = "";
        gameObject.SetActive(false);
        
        if (_playerMove != null)
        {
            _playerMove.enabled = true; // 允许玩家移动
        }

        if (_playerInteract != null)
        {
            _playerInteract.enabled = true; // 允许玩家交互
        }
    }
}
