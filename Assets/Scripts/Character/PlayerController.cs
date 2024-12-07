using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 3;
    
    [SerializeField]
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }
    

    public void TakeDamage(int damage)
    {
        Debug.Log("Take damage: " + damage);
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