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
//            Debug.LogError("����ݭn�@�� Rigidbody2D!");
//        }
//        else
//        {
//            rb.bodyType = RigidbodyType2D.Dynamic; // �����z�v�T
//        }
//    }

//    void Update()
//    {
//        // �o�̨S���ʧ@�A���Ӥ��ݭn�g����F��
//    }

//    // �ϥ� Collider2D �ýT�OĲ�o���Ҧ�
//    void OnTriggerEnter2D(Collider2D other) // �אּ Collider2D
//    {
//        if (other.gameObject == playerObject) // �P�_Ĳ�o���O�_�����w�����a����
//        {
//            Debug.Log("���a�i�J�����d��I");
//            Invoke("bridgeFallDown", timeToFall); // ���� 3 ������ʧ@
//        }
//    }

//    void bridgeFallDown()
//    {
//        theLeftPart.GetComponent<BridgeEvent>().canMove = true;
//        rb.bodyType = RigidbodyType2D.Kinematic; // �������z�v�T
//    }
//}

using System.Collections;
using UnityEngine;

public class BridgeTrigger : MonoBehaviour
{
    [SerializeField] GameObject theLeftPart;
    [SerializeField] GameObject theRightPart;
    [SerializeField] GameObject playerObject;
    [SerializeField] float timeToFall = 3.0f; // ���ݮɶ�
    private Rigidbody2D rb;
    private Collider2D bridgeCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bridgeCollider = GetComponent<Collider2D>(); // ����I����
        if (rb == null)
        {
            Debug.LogError("����ݭn�@�� Rigidbody2D!");
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic; // �]�m�������z�v�T
        }

        if (bridgeCollider == null)
        {
            Debug.LogError("����ݭn�@�� Collider2D!");
        }
    }

    void Update()
    {
        // �o�̤��ݭn�B�~���޿�
    }

    // ����Ĳ�o���i�J
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == playerObject) // ���a�i�JĲ�o�d��
        {
            Debug.Log("���a�i�J�����d��I");
            // ��������k
            Invoke("bridgeFallDown", timeToFall);
        }
    }

    // ���ݮɶ�����ܸI�����A�����餣�A�P���a�I��
    void bridgeFallDown()
    {
        theLeftPart.GetComponent<BridgeEvent>().canMove = true;
        theRightPart.GetComponent<BridgeEvent>().canMove = true;
        // ����P���a�I��
        bridgeCollider.isTrigger = true;  // �N�I�����]��Ĳ�o���A����P���a�I��

        rb.bodyType = RigidbodyType2D.Kinematic; // �����餣�����z�v�T�A�}�l����
    }
}
