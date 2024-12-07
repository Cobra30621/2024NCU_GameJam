using UnityEngine;


    public class DrawCollider : MonoBehaviour
    {
        [SerializeField] private Color fillColor = new Color(0f, 1f, 0f, 0.3f); // 填充顏色
        [SerializeField] private Color outlineColor = Color.green; // 線框顏色
        
        private void OnDrawGizmos()
        {
            BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
            if (boxCollider != null)
            {
                // 設置 Gizmos 填充顏色
                Gizmos.color = fillColor;
            
                // 計算 box collider 的世界空間位置和大小
                Vector3 pos = transform.position + (Vector3)boxCollider.offset;
                Vector3 size = new Vector3(
                    boxCollider.size.x * transform.localScale.x,
                    boxCollider.size.y * transform.localScale.y,
                    0.1f
                );
            
                // 繪製實心方塊
                Gizmos.DrawCube(pos, size);
            
                // 設置線框顏色
                Gizmos.color = outlineColor;
                // 繪製線框
                Gizmos.DrawWireCube(pos, size);
            }
        }
    }
