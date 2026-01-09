using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFade : MonoBehaviour
{
    public Image fadeImage; // 连接到FadeImage
    public float fadeDuration = 1f; // 淡入淡出持续时间

    private void Start()
    {
        // 在场景开始时执行淡入效果
        StartCoroutine(FadeIn());
    }

    // 淡入效果（场景加载时从黑色逐渐变透明）
    public IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color startColor = fadeImage.color;
        startColor.a = 1f; // 初始为黑色
        fadeImage.color = startColor;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            startColor.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration); // 从黑色到透明
            fadeImage.color = startColor;
            yield return null;
        }
        fadeImage.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
    }

    // 淡出效果（切换场景时）
    public IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color startColor = fadeImage.color;
        startColor.a = 0f; // 初始为透明
        fadeImage.color = startColor;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            startColor.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration); // 从透明到黑色
            fadeImage.color = startColor;
            yield return null;
        }
        fadeImage.color = new Color(startColor.r, startColor.g, startColor.b, 1f);
    }

    // 在场景切换时调用
    public void TriggerFadeOut()
    {
        StartCoroutine(FadeOut());
    }
}