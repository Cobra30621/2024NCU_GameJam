using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DefaultNamespace
{
    public class PlayerHurtFeedback : MonoBehaviour
    {
        [SerializeField] private List<SpriteRenderer> playerSprites;
        [SerializeField] private float flashDuration = 0.1f;
        [SerializeField] private int flashCount = 1;
        [SerializeField] private float vibrationDuration = 0.2f;
        [SerializeField] private float vibrationStrength = 0.5f;
        
        private List<Color> originalColors = new List<Color>();
        private static readonly Color hurtColor = Color.red;

        private void Awake()
        {
            if (playerSprites == null || playerSprites.Count == 0)
                playerSprites = new List<SpriteRenderer> { GetComponent<SpriteRenderer>() };
                
            originalColors = playerSprites.Select(sprite => sprite.color).ToList();
        }

        public void PlayHurtFeedback()
        {
            StartCoroutine(FlashRoutine());
            StartCoroutine(VibrateRoutine());
        }

        private IEnumerator FlashRoutine()
        {
            for (int i = 0; i < flashCount; i++)
            {
                for (int j = 0; j < playerSprites.Count; j++)
                {
                    playerSprites[j].color = hurtColor;
                }
                yield return new WaitForSeconds(flashDuration);
                
                for (int j = 0; j < playerSprites.Count; j++)
                {
                    playerSprites[j].color = originalColors[j];
                }
                yield return new WaitForSeconds(flashDuration);
            }
        }

        private IEnumerator VibrateRoutine()
        {
            var gamepad = Gamepad.current;
            Debug.Log("Gamepad: " + gamepad);
            if (gamepad != null)
            {
                gamepad.SetMotorSpeeds(vibrationStrength, vibrationStrength);
                yield return new WaitForSeconds(vibrationDuration);
                gamepad.SetMotorSpeeds(0f, 0f);
            }
        }
    }
}