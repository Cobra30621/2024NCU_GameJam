using System;
using Cinemachine;
using End;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Required]
    public PlayerController playerPrefab;
    
    [Required]
    public CinemachineVirtualCamera followCamera;

    [Required]
    public PlayerController PlayerController;

    [Required]
    public SaveManager SaveManager;

    public SFXManager SfxManager;

    public BGMManager BGMManager;
        
    public static UnityEvent  OnWinGame = new UnityEvent();
    public static UnityEvent OnGameOver = new UnityEvent();
        
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        if (followCamera == null)
        {
            followCamera = FindObjectOfType<CinemachineVirtualCamera>();
            if (followCamera == null)
            {
                Debug.LogError("場景中找不到 CinemachineVirtualCamera");
            }
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

        if (Input.GetKeyDown(KeyCode.W))
        {
            WinGame();
        }
    }

    public void StartGame()
    {
        BGMManager.Instance.PlayBGM("game");
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
        EndLoader.Instance.LoadEnd();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }
        
    public void TakeDamage(int damage)
    {
        PlayerController.TakeDamage(damage);
    }

    public void RespawnPlayer()
    {
        if (SaveManager.Instance.HasSavedPosition())
        {
            // 如果有存檔點，重置角色位置
            Destroy(PlayerController.gameObject);
            PlayerController = Instantiate(playerPrefab);
            
            PlayerController.transform.position = SaveManager.Instance.GetSavedPosition();
            PlayerController.InitHealth();
            followCamera.Follow = PlayerController.transform;
            Debug.Log("角色已在存檔點復活");
        }
        else
        {
            // 如果沒有存檔點，重新加載場景
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("沒有存檔點，重新加載場景");
        }
    }
}