using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text dialogueText;
    public OptionPanelController optionPanel;

    [Header("Typing")]
    public float charInterval = 0.04f;

    private string[] dialogueLines;
    private int currentLineIndex;
    private Coroutine typingCoroutine;
    private bool isTyping;

    private PlayerMove _playerMove;
    private PlayerInteract _playerInteract;

    // ===== 新增：选项回调暂存 =====
    private Action<int> pendingOptionCallback;

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

        // 当选项存在时，对话输入失效
        if (optionPanel != null && optionPanel.gameObject.activeSelf)
            return;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                dialogueText.text = dialogueLines[currentLineIndex];
                isTyping = false;
            }
            else
            {
                currentLineIndex++;

                if (currentLineIndex < dialogueLines.Length)
                {
                    typingCoroutine = StartCoroutine(
                        TypeText(dialogueLines[currentLineIndex])
                    );
                }
                else
                {
                    Close();
                }
            }
        }
    }

    /* ======================
     * 普通对话
     * ====================== */
    public void Show(string[] lines)
    {
        dialogueLines = lines;
        currentLineIndex = 0;

        gameObject.SetActive(true);
        dialogueText.text = "";

        ForbidPlayer();

        typingCoroutine = StartCoroutine(
            TypeText(dialogueLines[currentLineIndex])
        );
    }

    /* ======================
     * 带选项的对话（核心）
     * ====================== */
    public void ShowWithOptions(
        string[] lines,
        List<string> options,
        Action<int> onOptionSelected
    )
    {
        dialogueLines = lines;
        currentLineIndex = 0;
        pendingOptionCallback = onOptionSelected;

        gameObject.SetActive(true);
        dialogueText.text = "";

        ForbidPlayer();

        typingCoroutine = StartCoroutine(
            TypeTextWithOptions(dialogueLines[currentLineIndex], options)
        );
    }

    IEnumerator TypeTextWithOptions(string text, List<string> options)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(charInterval);
        }

        isTyping = false;

        // 显示选项（但不执行回调）
        optionPanel.ShowOptions(options, OnOptionChosen);
    }

    /* ======================
     * 选项被确认（关键）
     * ====================== */
    void OnOptionChosen(int index, string optionText)
    {
        StartCoroutine(HandleOptionChosen(index));
    }

    IEnumerator HandleOptionChosen(int index)
    {
        // 1. 关闭选项 UI
        optionPanel.Hide();

        // 2. 关闭对话系统
        // Close();

        // 3. 等一帧，确保 UI & 输入状态刷新
        yield return null;

        // 4. 执行外部逻辑
        pendingOptionCallback?.Invoke(index);
        pendingOptionCallback = null;
    }

    /* ======================
     * 打字协程
     * ====================== */
    IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(charInterval);
        }

        isTyping = false;
    }

    /* ======================
     * Close / 玩家控制
     * ====================== */
    void Close()
    {
        dialogueText.text = "";
        gameObject.SetActive(false);

        AllowPlayer();
    }

    void ForbidPlayer()
    {
        if (_playerMove != null)
            _playerMove.ForbidMovement();

        if (_playerInteract != null)
            _playerInteract.enabled = false;
    }

    void AllowPlayer()
    {
        if (_playerMove != null)
            _playerMove.AllowMovement();

        if (_playerInteract != null)
            _playerInteract.enabled = true;
    }
}
