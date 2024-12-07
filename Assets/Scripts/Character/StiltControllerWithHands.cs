using UnityEngine;

public class StiltControllerWithHands : MonoBehaviour
{
    public Transform body;            // 角色身體
    public Rigidbody2D leftStiltRb;   // 左高蹺的剛體
    public Rigidbody2D rightStiltRb;  // 右高蹺的剛體

    public float angleStrength = 50f;  // 角度控制強度
    public float pushStrength = 10f;   // 推力強度
    public float balanceForce = 20f;   // 身體平衡力

    void Update()
    {
        // 控制左高蹺的角度
        float leftHandInput = Input.GetAxis("Vertical_Left"); // 上下鍵或左搖桿垂直方向控制
        ControlStiltAngle(leftStiltRb, leftHandInput);

        // 控制右高蹺的角度
        float rightHandInput = Input.GetAxis("Vertical_Right"); // 上下鍵或右搖桿垂直方向控制
        ControlStiltAngle(rightStiltRb, rightHandInput);
    }

    void FixedUpdate()
    {
        // 在物理更新中保持角色身體的平衡
        BalanceBody();
    }

    // 控制高蹺角度與推力
    private void ControlStiltAngle(Rigidbody2D stiltRb, float input)
    {
        if (input != 0)
        {
            // 根據玩家的輸入施加角度力（Torque）
            float torque = input * angleStrength;
            stiltRb.AddTorque(torque);

            // 根據玩家的輸入施加推力（Force）
            Vector2 force = new Vector2(input * pushStrength, 0);
            stiltRb.AddForce(force);
        }
        else
        {
            stiltRb.velocity = Vector2.zero;
        }
    }

    // 計算並施加角色身體的平衡力
    private void BalanceBody()
    {
        // 計算兩根高蹺的支撐點中心
        Vector2 midPoint = (leftStiltRb.position + rightStiltRb.position) / 2;

        // 計算角色身體應在的位置
        Vector2 bodyOffset = midPoint - (Vector2)body.position;

        // 將角色身體拉向高蹺中點
        body.GetComponent<Rigidbody2D>().AddForce(bodyOffset * balanceForce);
    }
}