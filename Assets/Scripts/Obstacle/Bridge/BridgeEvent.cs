using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BridgeEvent : MonoBehaviour
{
    public bool canMove = false; // 控制是否受物理影響
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("物體需要一個 Rigidbody!");
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Kinematic; // 不受物理影響
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (rb != null)
        {
            if (canMove)
            {
                rb.bodyType = RigidbodyType2D.Dynamic; // 受物理影響
            }
            else
            {
                rb.bodyType = RigidbodyType2D.Kinematic; // 不受物理影響
            }
        }
        
    }
    
    
}

