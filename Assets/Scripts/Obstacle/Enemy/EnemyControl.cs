using Unity.VisualScripting;
using UnityEngine;

public class EnemyEvent : MonoBehaviour
{
    [Header("移動設定")]
    public float moveSpeed = 2f;        // 移動速度
    public float walkDistance = 5f;     // 來回移動的距離

    [Header("偵測地面和牆壁")]
    public Transform groundCheck;      // 用於檢查地面
    public LayerMask groundLayer;      // 用於標記地面圖層

    private float startPositionX;       // 怪物的初始位置
    private bool movingRight = true;    // 控制移動方向
    private Rigidbody2D rb;             // 2D 物理剛體
    [SerializeField] int coolDown = 5000;
    [SerializeField] int time;
    [SerializeField] bool shouldRurn = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // 獲取 Rigidbody2D 組件
        startPositionX = transform.position.x;  // 記錄初始位置
        time = coolDown;
    }

    void Update()
    {
        // 移動怪物
        Move();

        // 確認是否需要轉身
        if ((IsAtEdge() )&&shouldRurn || IsHittingWall())
        {
            Flip();
            shouldRurn = false;
        }
        if (!shouldRurn)
        {
            time--;
            if(time < 0)
            {
                time = coolDown;
                shouldRurn=true;
            }
        }
    }

    // 讓怪物左右來回移動
    void Move()
    {
        float moveDirection = movingRight ? 1 : -1;
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
    }

    // 偵測是否到達邊界 (地面消失)
    bool IsAtEdge()
    {
        if (groundCheck == null) return false;
        // 增加 Raycast 的距離，讓地面檢測更準確
        float rayDistance = 1f; // 增加到 1，比腳的位置低一點
        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, rayDistance, groundLayer);
        Debug.DrawRay(groundCheck.position, Vector2.down * rayDistance, Color.red); // Debug 可視化檢查
        return groundInfo.collider == null; // 如果地面未被檢測到，則返回 true
    }


    // 偵測是否撞到牆壁
    bool IsHittingWall()
    {
        float direction = movingRight ? 1 : -1;
        float rayDistance = 1f;
        RaycastHit2D wallInfo = Physics2D.Raycast(transform.position, Vector2.right * direction, rayDistance, groundLayer);
        Debug.DrawRay(transform.position, Vector2.right * direction* rayDistance, Color.red); // Debug 可視化檢查
        return wallInfo.collider != null; // 如果前方有牆壁，則返回 true
    }

    // 翻轉怪物
    void Flip()
    {
        movingRight = !movingRight;  // 改變方向
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;  // 翻轉 X 軸
        transform.localScale = localScale;
    }
}
