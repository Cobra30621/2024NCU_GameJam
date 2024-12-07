using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
        
    [Required]
    public PlayerController PlayerController;

    [Required]
    public SaveManager SaveManager;
        
    public static UnityEvent  OnWinGame = new UnityEvent();
    public static UnityEvent OnGameOver = new UnityEvent();
        
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        StartGame();
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private void Update()
    {
        // 當按下 R 鍵時重新開始遊戲
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            PlayerController.TakeDamage(1000);
        }
    }

    public void StartGame()
    {
        SaveManager.Initial();
    }

    public void GameOver(){
         
        // Show Game Over UI
        OnGameOver.Invoke();
    }

    public void WinGame()
    {
        Debug.Log("Win");
        OnWinGame.Invoke();
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