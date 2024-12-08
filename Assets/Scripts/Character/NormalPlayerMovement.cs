using UnityEngine;

namespace DefaultNamespace
{
    public class NormalPlayerMovement : MonoBehaviour
    {
        // 移動參數
        public float moveSpeed = 5f; // 移動速度
        public float jumpForce = 10f; // 跳躍力

        // 跳躍參數
        private bool isGrounded = false; // 是否接觸地面
        private int jumpCount = 0; // 跳躍次數

        // 組件引用
        private Rigidbody2D rb;
        private Collider2D coll;

        void Start()
        {
            // 獲取組件
            rb = GetComponent<Rigidbody2D>();
            coll = GetComponent<Collider2D>();
        }

        void Update()
        {
            // 水平移動
            float moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

            // 角色翻轉（面向移動方向）
            if (moveInput > 0)
                transform.localScale = new Vector3(1, 1, 1);
            else if (moveInput < 0)
                transform.localScale = new Vector3(-1, 1, 1);

            // 跳躍
            if (Input.GetButtonDown("Jump") && jumpCount < 2) // 檢查是否可以跳躍
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpCount++;
            }
        }

        // void OnCollisionEnter2D(Collision2D collision)
        // {
        //     // 確保碰撞的是地面
        //     if (collision.gameObject.CompareTag("ground"))
        //     {
        //         isGrounded = true;
        //         jumpCount = 0; // 重置跳躍次數
        //     }
        // }
        //
        // void OnCollisionExit2D(Collision2D collision)
        // {
        //     // 離開地面
        //     if (collision.gameObject.CompareTag("ground"))
        //     {
        //         isGrounded = false;
        //     }
        // }
    }
}