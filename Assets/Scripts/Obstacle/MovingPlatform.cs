using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField] private float moveSpeed = 2f;        // 移動速度
    [SerializeField] private float moveDistance = 5f;     // 移動距離
    [SerializeField] private Color gizmosColor = Color.yellow;  // Gizmos 顏色
    [SerializeField] private Vector3 moveDirection = Vector3.right;  // 新增：移動方向
    
    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool movingForward = true;

    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + moveDirection.normalized * moveDistance;  // 修改：使用自定義方向
    }

    void Update()
    {
        Vector3 targetPosition = movingForward ? endPosition : startPosition;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            movingForward = !movingForward;
        }
    }

    // 在編輯器中繪製移動路徑
    private void OnDrawGizmos()
    {
        // 如果遊戲還沒開始運行，使用當前位置作為起始點
        Vector3 start = Application.isPlaying ? startPosition : transform.position;
        Vector3 end = start + moveDirection.normalized * moveDistance;  // 修改：使用自定義方向

        // 設置 Gizmos 顏色
        Gizmos.color = gizmosColor;

        // 繪製起點和終點的球體
        Gizmos.DrawWireSphere(start, 0.3f);
        Gizmos.DrawWireSphere(end, 0.3f);

        // 繪製連接線
        Gizmos.DrawLine(start, end);
    }
}