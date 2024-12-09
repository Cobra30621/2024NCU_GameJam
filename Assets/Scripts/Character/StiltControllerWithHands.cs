using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class StiltControllerWithHands : MonoBehaviour
{
    private PlayerController _characterController;
    public Transform body;            // 角色身體
    public Rigidbody2D leftStiltRb;   // 左高蹺的剛體
    public Rigidbody2D rightStiltRb;  // 右高蹺的剛體

    
    [SerializeField] private TouchGroundChecker leftGroundChecker;   // 左腳地面檢查器
    [SerializeField] private TouchGroundChecker rightGroundChecker;  // 右腳地面檢查器

    private PlayerInput playerInput;
    private InputAction leftStickAction;
    private InputAction rightStickAction;
    private Vector2 leftStickValue;
    private Vector2 rightStickValue;


    public float angleStrength = 50f;  // 角度控制強度
    public float balanceForce = 20f;   // 身體平衡力
    public float groundedAngleStrength = 200f;  // 著地時的角度控制強度

    public HingeJoint2D leftHingeJoint;   // 左邊的 HingeJoint2D
    public HingeJoint2D rightHingeJoint;  // 右邊的 HingeJoint2D
    
    
    [SerializeField] private TMP_InputField angleStrengthInput;    // 改用 InputField
    [SerializeField] private TMP_InputField groundAngleStrengthInput;

    private void Awake()
    {
        _characterController = GetComponent<PlayerController>();
        playerInput = GetComponent<PlayerInput>();
        leftStickAction = playerInput.actions["LeftStick"];
        rightStickAction = playerInput.actions["RightStick"];
    }

    private void OnEnable()
    {
        leftStickAction.Enable();
        rightStickAction.Enable();
    }

    private void OnDisable()
    {
        leftStickAction.Disable();
        rightStickAction.Disable();
    }

    void Update()
    {   
        // 如果角色已死亡，則不處理任何輸入
        if (!_characterController.CanMove) return;
        
        // 獲取搖桿數值
        leftStickValue = leftStickAction.ReadValue<Vector2>();
        rightStickValue = rightStickAction.ReadValue<Vector2>();

        // 控制左高蹺的角度
        ControlStiltAngle(leftStiltRb, leftStickValue.x);

        // 控制右高蹺的角度
        ControlStiltAngle(rightStiltRb, rightStickValue.x);
    }

    void FixedUpdate()
    {
        // 如果角色已死亡，則不進行平衡
        if (!_characterController.CanMove) return;
        
        // 在物理更新中保持角色身體的平衡
        BalanceBody();
    }

    // 控制高蹺角度與推力
    private void ControlStiltAngle(Rigidbody2D stiltRb, float input)
    {
        // 根據不同的高蹺選擇對應的 HingeJoint
        HingeJoint2D currentJoint = (stiltRb == leftStiltRb) ? leftHingeJoint : rightHingeJoint;

        if (input != 0)
        {
            // 當有輸入時，關閉 HingeJoint 的限制
            currentJoint.useLimits = false;

            // 判斷是否雙腳著地
            bool bothGrounded = leftGroundChecker.IsGrounded() && rightGroundChecker.IsGrounded();
            
            // 根據著地狀態選擇不同的力道
            float currentAngleStrength = bothGrounded ? groundedAngleStrength : angleStrength;
            
            // 根據玩家的輸入施加角度力（Torque）
            // float torque = input * currentAngleStrength;
            // stiltRb.AddTorque(torque);
            float torque = input * currentAngleStrength * Time.fixedDeltaTime * 60f;
            stiltRb.AddTorque(torque);

        }
        else
        {
            // 當沒有輸入時，重新啟用 HingeJoint 的限制
            currentJoint.useLimits = true;
            stiltRb.AddTorque(0);
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

    void Start()
    {
        // 設置輸入框的初始值
        angleStrengthInput.text = angleStrength.ToString();
        groundAngleStrengthInput.text = groundedAngleStrength.ToString();
        
        // 添加輸入框的值改變監聽器
        angleStrengthInput.onEndEdit.AddListener(UpdateAngleStrength);
        groundAngleStrengthInput.onEndEdit.AddListener(UpdateGroundAngleStrength);
    }

    // 更新處理方法以處理字符串輸入
    private void UpdateAngleStrength(string value)
    {
        if (float.TryParse(value, out float newValue))
        {
            angleStrength = newValue;
        }
        else
        {
            // 如果輸入無效，恢復為原始值
            angleStrengthInput.text = angleStrength.ToString();
        }
    }

    private void UpdateGroundAngleStrength(string value)
    {
        if (float.TryParse(value, out float newValue))
        {
            groundedAngleStrength = newValue;
        }
        else
        {
            // 如果輸入無效，恢復為原始值
            groundAngleStrengthInput.text = groundedAngleStrength.ToString();
        }
    }
}