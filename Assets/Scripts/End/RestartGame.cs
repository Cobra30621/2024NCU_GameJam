using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class RestartGame : MonoBehaviour
    {
        public void Restart()
        {
            SceneManager.LoadScene("Game");
        }
    }
}