using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 3;
    
    [SerializeField]
    private int currentHealth;

    [SerializeField]
    private float damageCooldown = 1f; // 受傷 CD 時間（秒）
    private float lastDamageTime; // 上次受傷時間

    public static UnityEvent<int, int> OnHealthChanged = new UnityEvent<int, int>();

    void Start()
    {
        currentHealth = maxHealth;
    }
    

    public void TakeDamage(int damage)
    {
        // 檢查是否在 CD 中
        if (Time.time - lastDamageTime < damageCooldown)
        {
            return; // 如果在 CD 中，直接返回不造成傷害
        }

        Debug.Log("Take damage: " + damage);
        currentHealth -= damage;
        lastDamageTime = Time.time; // 更新上次受傷時間
        OnHealthChanged.Invoke(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // 玩家死亡邏輯
        Debug.Log("Player Died");
        GameManager.Instance.GameOver();
    }
}