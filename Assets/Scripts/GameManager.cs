using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
        
    public PlayerController PlayerController;
        
    public static UnityEvent OnGameOver = new UnityEvent();
        
        
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
            
    }

    public void GameOver(){
         
        // Show Game Over UI
        OnGameOver.Invoke();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }
        
    public void TakeDamage(int damage)
    {
        PlayerController.TakeDamage(damage);
    }
}