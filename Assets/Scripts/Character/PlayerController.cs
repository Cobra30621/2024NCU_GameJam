using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 3;
    
    [SerializeField]
    private int currentHealth;

    [SerializeField]
    private float damageCooldown = 1f; // 受傷 CD 時間（秒）
    private float lastDamageTime; // 上次受傷時間

    public static UnityEvent<int, int> OnHealthChanged = new UnityEvent<int, int>();

    private PlayerHurtFeedback _playerHurtFeedback;
    
    private void Awake()
    {
        _playerHurtFeedback = GetComponent<PlayerHurtFeedback>();
        if (_playerHurtFeedback == null)
        {
            Debug.LogError("PlayerHurtFeedback is null");
        }
    }
    
    
    void Start()
    {
        InitHealth();
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
        _playerHurtFeedback.PlayHurtFeedback();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // 玩家死亡邏輯
        Debug.Log("Player Died");
        // GameManager.Instance.GameOver();
        Respawn();
    }
    
    private void Respawn()
    {
        if (SaveManager.Instance.HasSavedPosition())
        {
            // 如果有存檔點，重置角色位置
            transform.position = SaveManager.Instance.GetSavedPosition();
            InitHealth();
            Debug.Log("角色已在存檔點復活");
        }
        else
        {
            // 如果沒有存檔點，重新加載場景
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("沒有存檔點，重新加載場景");
        }
    }

    private void InitHealth()
    {
        currentHealth = maxHealth;
        OnHealthChanged.Invoke(currentHealth, maxHealth);
    }
}