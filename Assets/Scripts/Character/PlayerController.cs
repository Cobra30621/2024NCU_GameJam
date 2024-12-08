using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public bool NeverDead = false;
    
    public int maxHealth = 3;
    
    [SerializeField]
    private int currentHealth;

    [SerializeField]
    private float damageCooldown = 1f; // 受傷 CD 時間（秒）
    private float lastDamageTime; // 上次受傷時間

    public static UnityEvent<int, int> OnHealthChanged = new UnityEvent<int, int>();

    private PlayerHurtFeedback _playerHurtFeedback;
    
    public bool IsDead { get; private set; }
    
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
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            NeverDead = true;
            Debug.Log("無敵模式開啟");
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            NeverDead = false;
            Debug.Log("無敵模式關閉");
        }
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
        
        if (currentHealth <= 0 && !NeverDead)
        {
            Die();
        }
        else
        {
            _playerHurtFeedback.PlayHurtFeedback();
        }
    }

    void Die()
    {
        StartCoroutine(DieCoroutine());
    }

    private IEnumerator DieCoroutine()
    {
        IsDead = true;
        // 玩家死亡邏輯
        Debug.Log("Player Died");
        SFXManager.Instance.PlaySound("die");
        yield return new WaitForSeconds(2f);
        GameManager.Instance.RestartGame();
    }


    
    public void InitHealth()
    {
        currentHealth = maxHealth;
        OnHealthChanged.Invoke(currentHealth, maxHealth);
        IsDead = false;
    }
}