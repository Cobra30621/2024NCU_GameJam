using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    // 身體與腳的 Transform
    public Transform body;
    public Transform leftFoot;
    public Transform rightFoot;

    // 雙腳的速度
    public float footSpeed = 5f;

    // 地面高度（假設為固定值）
    public float groundHeight = 0f;

    // 平衡調整參數
    public float bodySmoothness = 0.1f;

    void Update()
    {
        // 1. 控制左腳（WASD）
        Vector2 leftInput = new Vector2(
            Input.GetKey(KeyCode.A) ? -1 : Input.GetKey(KeyCode.D) ? 1 : 0,
            Input.GetKey(KeyCode.S) ? -1 : Input.GetKey(KeyCode.W) ? 1 : 0
        );
        MoveFoot(leftFoot, leftInput);

        // 2. 控制右腳（方向鍵）
        Vector2 rightInput = new Vector2(
            Input.GetKey(KeyCode.LeftArrow) ? -1 : Input.GetKey(KeyCode.RightArrow) ? 1 : 0,
            Input.GetKey(KeyCode.DownArrow) ? -1 : Input.GetKey(KeyCode.UpArrow) ? 1 : 0
        );
        MoveFoot(rightFoot, rightInput);

        // 3. 調整身體位置
        // BalanceBody();
    }

    private void MoveFoot(Transform foot, Vector2 input)
    {
        if (input.magnitude > 0)
        {
            // 計算腳的目標位置
            Vector3 targetPosition = foot.position + (Vector3)input.normalized * footSpeed * Time.deltaTime;

            // 限制腳的位置在地面之上
            targetPosition.y = Mathf.Max(targetPosition.y, groundHeight);

            // 移動腳到目標位置
            foot.position = targetPosition;
        }
    }

    private void BalanceBody()
    {
        // 計算身體應在的位置（雙腳中心點）
        Vector3 targetBodyPosition = (leftFoot.position + rightFoot.position) / 2;

        // 緩動移動身體
        body.position = Vector3.Lerp(body.position, targetBodyPosition, bodySmoothness);
    }
}