using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BridgeEvent : MonoBehaviour
{
    public bool canMove = false; // ����O�_�����z�v�T
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("����ݭn�@�� Rigidbody!");
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Kinematic; // �������z�v�T
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (rb != null)
        {
            if (canMove)
            {
                rb.bodyType = RigidbodyType2D.Dynamic; // �����z�v�T
            }
            else
            {
                rb.bodyType = RigidbodyType2D.Kinematic; // �������z�v�T
            }
        }
        
    }
    
    
}

