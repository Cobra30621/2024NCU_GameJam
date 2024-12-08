using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Sirenix.OdinInspector;

public class ScreenEffectController : MonoBehaviour
{
    [SerializeField] private Image fadeImage; // 用於淡入淡出的黑色圖片
    [SerializeField] private Image flashImage; // 用於閃光效果的白色圖片
    
    public float defaultFadeDuration = 1f; // 預設淡入淡出時間
    public float defaultFlashDuration = 0.5f; // 預設閃光時間
    

    [Button]
    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }
    
    [Button]
    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }
    
    [Button]
    public void Flash()
    {
        StartCoroutine(FlashCoroutine());
    }
    

    // 淡入效果（從透明到黑色）
    public IEnumerator FadeInCoroutine(float duration = -1)
    {
        duration = duration < 0 ? defaultFadeDuration : duration;
        float elapsedTime = 0;
        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / duration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }

    // 淡出效果（從黑色到透明）
    public IEnumerator FadeOutCoroutine(float duration = -1)
    {
        duration = duration < 0 ? defaultFadeDuration : duration;
        float elapsedTime = 0;
        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = 1 - Mathf.Clamp01(elapsedTime / duration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }

    // 閃光效果
    public IEnumerator FlashCoroutine(float duration = -1)
    {
        duration = duration < 0 ? defaultFlashDuration : duration;
        float halfDuration = duration / 2;
        float elapsedTime = 0;
        
        // 閃光淡入
        while (elapsedTime < halfDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / halfDuration);
            flashImage.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
        
        elapsedTime = 0;
        
        // 閃光淡出
        while (elapsedTime < halfDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = 1 - Mathf.Clamp01(elapsedTime / halfDuration);
            flashImage.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
    }
}