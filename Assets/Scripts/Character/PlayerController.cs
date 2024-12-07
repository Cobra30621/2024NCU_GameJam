using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 100;
    
    [SerializeField]
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }
    

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // 玩家死亡邏輯
        Debug.Log("Player Died");
    }
}