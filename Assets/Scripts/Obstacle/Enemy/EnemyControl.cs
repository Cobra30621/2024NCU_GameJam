using Unity.VisualScripting;
using UnityEngine;

public class EnemyEvent : MonoBehaviour
{
    [Header("���ʳ]�w")]
    public float moveSpeed = 2f;        // ���ʳt��
    public float walkDistance = 5f;     // �Ӧ^���ʪ��Z��

    [Header("�����a���M���")]
    public Transform groundCheck;      // �Ω��ˬd�a��
    public LayerMask groundLayer;      // �Ω�аO�a���ϼh

    private float startPositionX;       // �Ǫ�����l��m
    private bool movingRight = true;    // ����ʤ�V
    private Rigidbody2D rb;             // 2D ���z����
    [SerializeField] int coolDown = 5000;
    [SerializeField] int time;
    [SerializeField] bool shouldRurn = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // ��� Rigidbody2D �ե�
        startPositionX = transform.position.x;  // �O����l��m
        time = coolDown;
    }

    void Update()
    {
        // ���ʩǪ�
        Move();

        // �T�{�O�_�ݭn�ਭ
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

    // ���Ǫ����k�Ӧ^����
    void Move()
    {
        float moveDirection = movingRight ? 1 : -1;
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
    }

    // �����O�_��F��� (�a������)
    bool IsAtEdge()
    {
        if (groundCheck == null) return false;
        // �W�[ Raycast ���Z���A���a���˴���ǽT
        float rayDistance = 1f; // �W�[�� 1�A��}����m�C�@�I
        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, rayDistance, groundLayer);
        Debug.DrawRay(groundCheck.position, Vector2.down * rayDistance, Color.red); // Debug �i�����ˬd
        return groundInfo.collider == null; // �p�G�a�����Q�˴���A�h��^ true
    }


    // �����O�_�������
    bool IsHittingWall()
    {
        float direction = movingRight ? 1 : -1;
        float rayDistance = 1f;
        RaycastHit2D wallInfo = Physics2D.Raycast(transform.position, Vector2.right * direction, rayDistance, groundLayer);
        Debug.DrawRay(transform.position, Vector2.right * direction* rayDistance, Color.red); // Debug �i�����ˬd
        return wallInfo.collider != null; // �p�G�e�観����A�h��^ true
    }

    // ½��Ǫ�
    void Flip()
    {
        movingRight = !movingRight;  // ���ܤ�V
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;  // ½�� X �b
        transform.localScale = localScale;
    }
}
