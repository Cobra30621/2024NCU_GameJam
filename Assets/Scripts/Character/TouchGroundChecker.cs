using UnityEngine;

namespace DefaultNamespace
{
    public class TouchGroundChecker : MonoBehaviour
    {
        [SerializeField] private float checkRadius = 0.2f;  // 檢查半徑
        [SerializeField] private LayerMask groundLayer;     // 地面層級
        [SerializeField] private Transform checkPoint;       // 檢查點

        private bool isGrounded;

        private void Start()
        {
            // 如果沒有指定檢查點，就使用當前物件的位置
            if (checkPoint == null)
            {
                checkPoint = transform;
            }
        }

        private void Update()
        {
            // 使用 Physics2D.OverlapCircle 檢查是否接觸地面
            isGrounded = Physics2D.OverlapCircle(checkPoint.position, checkRadius, groundLayer);
        }

        // 提供一個公開方法來獲取接地狀態
        public bool IsGrounded()
        {
            return isGrounded;
        }

        // 在編輯器中繪製檢查範圍（方便調試）
        private void OnDrawGizmos()
        {
            if (checkPoint == null) return;
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(checkPoint.position, checkRadius);
        }
    }
    
}