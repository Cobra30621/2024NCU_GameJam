using DefaultNamespace;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameOverUI : MonoBehaviour
    {
        [Required]
        [SerializeField] 
        private Button restartButton;

        [Required]
        [SerializeField]
        private GameObject mainPanel;

        private void Awake()
        {
            // 初始時隱藏介面
            mainPanel.SetActive(false);
            
            // 綁定重新開始按鈕事件
            restartButton.onClick.AddListener(OnRestartButtonClick);
        }

        private void OnEnable()
        {
            // 訂閱遊戲結束事件
            GameManager.OnGameOver.AddListener(ShowGameOver);
        }

        private void OnDisable()
        {
            // 取消訂閱遊戲結束事件
            GameManager.OnGameOver.RemoveListener(ShowGameOver);
        }

        private void ShowGameOver()
        {
            mainPanel.SetActive(true);
        }

        private void OnRestartButtonClick()
        {
            // 隱藏遊戲結束介面
            mainPanel.SetActive(false);
            // 重新開始遊戲
            GameManager.Instance.RestartGame();
        }
    }
}