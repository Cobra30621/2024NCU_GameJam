using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            GameManager.Instance.WinGame();
        }
    }

    private void OnDrawGizmos()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider != null)
        {
            // 設置 Gizmos 顏色為半透明綠色
            Gizmos.color = new Color(0f, 1f, 0f, 0.3f);
            
            // 計算 box collider 的世界空間位置和大小
            Vector3 pos = transform.position + (Vector3)boxCollider.offset;
            Vector3 size = new Vector3(
                boxCollider.size.x * transform.localScale.x,
                boxCollider.size.y * transform.localScale.y,
                0.1f
            );
            
            // 繪製實心方塊
            Gizmos.DrawCube(pos, size);
            
            // 設置線框顏色為純綠色
            Gizmos.color = Color.green;
            // 繪製線框
            Gizmos.DrawWireCube(pos, size);
        }
    }
}
