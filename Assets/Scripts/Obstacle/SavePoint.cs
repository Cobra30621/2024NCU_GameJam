using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer; // 保存 SpriteRenderer 引用
    private bool isActivated = false;      // 標記是否已經激活

    public string savePointSFX = "savePoint";
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 檢測是否是角色進入存檔點
        if (!isActivated && other.CompareTag("Player"))
        {
            // 設置為激活狀態
            isActivated = true;

            spriteRenderer.gameObject.SetActive(false);

            // 保存角色位置到存檔管理器
            SaveManager.Instance.SavePosition(transform.position);
            SFXManager.Instance.PlaySound(savePointSFX);

            Debug.Log("存檔點已激活：" + transform.position);
        }
    }
}