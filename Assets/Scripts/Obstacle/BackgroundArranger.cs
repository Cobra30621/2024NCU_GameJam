using Sirenix.OdinInspector;
using UnityEngine;

public class BackgroundArranger : MonoBehaviour
{
    [System.Serializable]
    public struct BackgroundRepeat
    {
        public Sprite sprite;      // 直接指定要使用的 Sprite
        public int repeatCount;    // 重複次數
    }
    
    public BackgroundRepeat[] backgroundSequence;  // 背景序列設置
    public float spacing = 0f;        // 背景之間的間距（如果不需要間距，設為 0）

    [Button("生成地圖")]
    void ArrangeBackgrounds()
    {
        // 刪除所有現有的背景
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        float currentXPosition = 0f;

        float parentScale = transform.lossyScale.x;  // 獲取父物件的全局縮放值

        // 使用新的背景序列生成背景
        foreach (var sequence in backgroundSequence)
        {
            for (int j = 0; j < sequence.repeatCount; j++)
            {
                GameObject bg = new GameObject($"Background_{sequence.sprite.name}_{j}");
                bg.transform.parent = transform;
                bg.transform.localScale = Vector3.one;  // 保持本地縮放為 1

                SpriteRenderer sr = bg.AddComponent<SpriteRenderer>();
                sr.sprite = sequence.sprite;
                sr.sortingOrder = -1;

                float bgWidth = sequence.sprite.bounds.size.x * parentScale;  // 考慮父物件縮放的實際寬度
                bg.transform.position = new Vector3(currentXPosition, 0, 0);
                currentXPosition += bgWidth + (spacing * parentScale);  // spacing 也需要考慮縮放
            }
        }
    }
}