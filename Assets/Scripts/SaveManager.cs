using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance => GameManager.Instance.SaveManager;

    private Vector3 lastSavedPosition; // 記錄最後的存檔位置
    private bool hasSavedPosition = false; // 是否已經有存檔點

    public void Initial()
    {
        lastSavedPosition = Vector3.zero;
        hasSavedPosition = false;
    }

    // 保存角色位置
    public void SavePosition(Vector3 position)
    {
        lastSavedPosition = position;
        hasSavedPosition = true;
    }

    // 獲取角色存檔位置
    public Vector3 GetSavedPosition()
    {
        return lastSavedPosition;
    }

    // 檢查是否有有效的存檔點
    public bool HasSavedPosition()
    {
        return hasSavedPosition;
    }
}