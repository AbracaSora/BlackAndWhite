using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ShadowMaterialController : MonoBehaviour
{
    private SpriteRenderer sr;
    private Material mat;

    [Header("Shadow Parameters")]
    [Range(0f, 1f)] public float alpha = 0f;
    [Range(0f, 1f)] public float grayAmount = 0.7f;
    [Range(0f, 0.3f)] public float whiteBias = 0.1f;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

        // 关键：实例化材质，避免影响其他对象
        mat = Instantiate(sr.material);
        sr.material = mat;
    }

    void Update()
    {
        mat.SetFloat("_Alpha", alpha);
        mat.SetFloat("_GrayAmount", grayAmount);
        mat.SetFloat("_WhiteBias", whiteBias);
    }
    
    public IEnumerator FadeInGhost(float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            alpha = Mathf.Lerp(0f, 0.6f, t / duration);
            yield return null;
        }
    }
    
    public IEnumerator SuddenOutGhost()
    {
        alpha = 0f;
        yield return null;
    }
}