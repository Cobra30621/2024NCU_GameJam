using UnityEngine;

public static class SaveManager
{
    private static  Vector3 lastSavedPosition; // 記錄最後的存檔位置
    private static  bool hasSavedPosition = false; // 是否已經有存檔點
    

    public static  void Initial()
    {
        lastSavedPosition = Vector3.zero;
        hasSavedPosition = false;
    }

    // 保存角色位置
    public static  void SavePosition(Vector3 position)
    {
        lastSavedPosition = position;
        hasSavedPosition = true;
    }

    // 獲取角色存檔位置
    public static  Vector3 GetSavedPosition()
    {
        return lastSavedPosition;
    }

    // 檢查是否有有效的存檔點
    public static  bool HasSavedPosition()
    {
        return hasSavedPosition;
    }
}