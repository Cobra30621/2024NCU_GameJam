using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace End
{
    public class EndLoader : MonoBehaviour
    {
        public ScreenEffectController EffectController;

        private static EndLoader instance;

        public static EndLoader Instance => instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }


        [Button]
        public void LoadEnd()
        {
            StartCoroutine(EndCoroutine());
        }

        private IEnumerator EndCoroutine()
        {
            yield return EffectController.FadeInCoroutine();
            SceneManager.LoadScene("End");
            yield return new WaitForSeconds(2f); 
            yield return EffectController.FadeOutCoroutine();
            
            var endingSequence = FindObjectOfType<EndingSequence>();
            if (endingSequence == null)
            {
                Debug.LogError("場景中找不到 endingSequence");
            }
            
            endingSequence.StartEndingSequence();
        }
    }
}