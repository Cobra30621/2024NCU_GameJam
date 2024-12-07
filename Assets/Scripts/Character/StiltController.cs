using DefaultNamespace;
using UnityEngine;

public class StiltController : MonoBehaviour
{
    public Transform body;           // 角色身體
    public Transform leftStilt;      // 左高蹺
    public Transform rightStilt;     // 右高蹺

    public float stiltSpeed = 5f;    // 高蹺移動速度
    public float balanceForce = 10f; // 平衡力

    private Rigidbody2D bodyRb;
    private Rigidbody2D leftStiltRb;
    private Rigidbody2D rightStiltRb;

    [SerializeField] private TouchGroundChecker leftGroundChecker;   // 左腳地面檢查器
    [SerializeField] private TouchGroundChecker rightGroundChecker;  // 右腳地面檢查器

    void Start()
    {
        // 獲取 Rigidbody2D
        bodyRb = body.GetComponent<Rigidbody2D>();
        leftStiltRb = leftStilt.GetComponent<Rigidbody2D>();
        rightStiltRb = rightStilt.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 控制左高蹺（WASD 控制）
        Vector2 leftInput = new Vector2(
            Input.GetKey(KeyCode.A) ? -1 : Input.GetKey(KeyCode.D) ? 1 : 0,
            Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0
        );
        MoveStilt(leftStiltRb, leftInput);

        // 控制右高蹺（方向鍵控制）
        Vector2 rightInput = new Vector2(
            Input.GetKey(KeyCode.LeftArrow) ? -1 : Input.GetKey(KeyCode.RightArrow) ? 1 : 0,
            Input.GetKey(KeyCode.UpArrow) ? 1 : Input.GetKey(KeyCode.DownArrow) ? -1 : 0
        );
        MoveStilt(rightStiltRb, rightInput);
    }

    void FixedUpdate()
    {
        // 平衡角色身體
        BalanceBody();
    }

    private void MoveStilt(Rigidbody2D stiltRb, Vector2 input)
    {
        // 確定是哪一隻腳
        bool canMove = (stiltRb == leftStiltRb) ? 
            leftGroundChecker.IsGrounded() : 
            rightGroundChecker.IsGrounded();

        // 只有在接觸地面時才能移動
        if (input.magnitude > 0 && canMove)
        {
            // 根據輸入方向施加推力
            Vector2 force = input.normalized * stiltSpeed;
            stiltRb.AddForce(force);

            // 同時給高蹺底部添加扭矩以模擬旋轉
            float torque = -input.x * stiltSpeed;
            stiltRb.AddTorque(torque);
        }
        else
        {
            stiltRb.velocity = Vector2.zero;
        }
    }

    private void BalanceBody()
    {
        // 計算高蹺之間的中點
        Vector2 midPoint = (leftStilt.position + rightStilt.position) / 2;

        // 將角色身體拉向高蹺中點
        Vector2 forceDirection = midPoint - (Vector2)body.position;
        bodyRb.AddForce(forceDirection * balanceForce);
    }
}