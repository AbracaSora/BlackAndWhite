using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;

public class OptionPanelController : MonoBehaviour
{
    [Header("References")]
    public RectTransform container;
    public VerticalLayoutGroup layoutGroup;
    public GameObject buttonPrefab;

    [Header("Selection Visual")]
    public Color normalColor = Color.white;
    public Color selectedColor = Color.yellow;

    private List<GameObject> buttons = new List<GameObject>();
    private int currentIndex = 0;
    private bool isActive = false;

    private Action<int, string> onOptionSelected;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (!isActive) return;

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            MoveSelection(-1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            MoveSelection(1);
        }
        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            ConfirmSelection();
        }
    }

    /* =======================
     * 对外暴露的核心接口
     * ======================= */

    /// <summary>
    /// </summary>
    public void ShowOptions(
        List<string> options,
        Action<int, string> callback
    )
    {
        if (container == null || layoutGroup == null || buttonPrefab == null)
        {
            Debug.LogError("OptionPanelController: 引用未绑定");
            return;
        }
        gameObject.SetActive(true);
        StartCoroutine(ShowOptionsNextFrame(options, callback));
    }
    
    public IEnumerator ShowOptionsNextFrame(
        List<string> options,
        Action<int, string> callback
    )
    {
        ClearOptions();
        yield return null;
        Debug.Log("ChildCount After Clean: " + container.childCount);
        onOptionSelected = callback;
        isActive = true;
        currentIndex = 0;

        foreach (string text in options)
        {
            GameObject btn = Instantiate(buttonPrefab, container);
            buttons.Add(btn);

            TMP_Text label = btn.GetComponentInChildren<TMP_Text>();
            if (label != null)
                label.text = text;
        }

        UpdateContainerHeight();
        UpdateVisual();
    }

    /// <summary>
    /// 强制关闭选项框
    /// </summary>
    public void Hide()
    {
        isActive = false;
        gameObject.SetActive(false);
    }

    /* =======================
     * 内部逻辑
     * ======================= */

    void MoveSelection(int delta)
    {
        currentIndex += delta;

        if (currentIndex < 0)
            currentIndex = buttons.Count - 1;
        else if (currentIndex >= buttons.Count)
            currentIndex = 0;

        UpdateVisual();
    }

    void ConfirmSelection()
    {
        isActive = false;

        string result = buttons[currentIndex]
            .GetComponentInChildren<TMP_Text>().text;

        onOptionSelected?.Invoke(currentIndex, result);
        gameObject.SetActive(false);
    }

    void UpdateVisual()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            Image img = buttons[i].GetComponent<Image>();
            if (img != null)
                img.color = (i == currentIndex) ? selectedColor : normalColor;
        }
    }

    void ClearOptions()
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
        buttons.Clear();
    }


    void UpdateContainerHeight()
    {
        float height = layoutGroup.padding.top + layoutGroup.padding.bottom;
        
        Debug.Log("ChildCount: " + container.childCount);
        for (int i = 0; i < container.childCount; i++)
        {
            RectTransform child = container.GetChild(i) as RectTransform;
            LayoutElement le = child.GetComponent<LayoutElement>();

            float h = le != null && le.preferredHeight > 0
                ? le.preferredHeight
                : child.sizeDelta.y;

            height += h;
            
            Debug.Log("Name: " + child.gameObject.name + ", Height: " + h);

            if (i < container.childCount - 1)
                height += layoutGroup.spacing;
        }
        
        Debug.Log("OptionPanelController: 更新容器高度为 " + height);

        container.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Vertical,
            height
        );
    }
}
