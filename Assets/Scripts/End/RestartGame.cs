using UnityEngine;

namespace UI
{
    public class RestartGame : MonoBehaviour
    {
        public void Restart()
        {
            GameManager.Instance.RestartGame();
        }
    }
}