using System;
using System.Collections;
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
    public PlayerController PlayerController;

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
        
    }

    private void Start()
    {
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
        InitialPlayer();
        
    }

    public void OnApplicationQuit()
    {
        SaveManager.Initial();
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

    public void InitialPlayer()
    {
        StartCoroutine(InitialPlayerCoroutine());
    }

    private IEnumerator InitialPlayerCoroutine()
    {
        PlayerController.SetCanMove(false);
        if (SaveManager.HasSavedPosition())
        {
            PlayerController.transform.position = SaveManager.GetSavedPosition();
        }
        PlayerController.InitHealth();
        
        yield return new WaitForSeconds(0.5f);
        PlayerController.SetCanMove(true);
    }
}