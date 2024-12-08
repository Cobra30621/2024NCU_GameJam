using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScaneControl : MonoBehaviour
{
    public void GameStart()
    {
        print("GameStart");
        SaveManager.Initial();
        SceneManager.LoadScene("Game");
    }
}
