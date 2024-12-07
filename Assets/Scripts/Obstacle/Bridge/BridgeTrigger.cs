//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class BridgeTrigger : MonoBehaviour
//{
//    [SerializeField] GameObject theLeftPart;
//    [SerializeField] GameObject theRightPart;
//    [SerializeField] GameObject playerObject;
//    [SerializeField] float timeToFall = 3.0f;
//    private Rigidbody2D rb;

//    void Start()
//    {
//        rb = GetComponent<Rigidbody2D>();
//        if (rb == null)
//        {
//            Debug.LogError("物體需要一個 Rigidbody2D!");
//        }
//        else
//        {
//            rb.bodyType = RigidbodyType2D.Dynamic; // 受物理影響
//        }
//    }

//    void Update()
//    {
//        // 這裡沒有動作，應該不需要寫任何東西
//    }

//    // 使用 Collider2D 並確保觸發器模式
//    void OnTriggerEnter2D(Collider2D other) // 改為 Collider2D
//    {
//        if (other.gameObject == playerObject) // 判斷觸發的是否為指定的玩家物件
//        {
//            Debug.Log("玩家進入橋的範圍！");
//            Invoke("bridgeFallDown", timeToFall); // 等待 3 秒後執行動作
//        }
//    }

//    void bridgeFallDown()
//    {
//        theLeftPart.GetComponent<BridgeEvent>().canMove = true;
//        rb.bodyType = RigidbodyType2D.Kinematic; // 不受物理影響
//    }
//}

using System.Collections;
using UnityEngine;

public class BridgeTrigger : MonoBehaviour
{
    [SerializeField] GameObject theLeftPart;
    [SerializeField] GameObject theRightPart;
    [SerializeField] GameObject playerObject;
    [SerializeField] float timeToFall = 3.0f; // 等待時間
    private Rigidbody2D rb;
    private Collider2D bridgeCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bridgeCollider = GetComponent<Collider2D>(); // 獲取碰撞器
        if (rb == null)
        {
            Debug.LogError("物體需要一個 Rigidbody2D!");
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic; // 設置為受物理影響
        }

        if (bridgeCollider == null)
        {
            Debug.LogError("物體需要一個 Collider2D!");
        }
    }

    void Update()
    {
        // 這裡不需要額外的邏輯
    }

    // 偵測觸發器進入
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == playerObject) // 當玩家進入觸發範圍
        {
            Debug.Log("玩家進入橋的範圍！");
            // 延遲執行方法
            Invoke("bridgeFallDown", timeToFall);
        }
    }

    // 等待時間後改變碰撞器，讓物體不再與玩家碰撞
    void bridgeFallDown()
    {
        theLeftPart.GetComponent<BridgeEvent>().canMove = true;
        theRightPart.GetComponent<BridgeEvent>().canMove = true;
        // 停止與玩家碰撞
        bridgeCollider.isTrigger = true;  // 將碰撞器設為觸發器，停止與玩家碰撞

        rb.bodyType = RigidbodyType2D.Kinematic; // 讓物體不受物理影響，開始移動
    }
}
