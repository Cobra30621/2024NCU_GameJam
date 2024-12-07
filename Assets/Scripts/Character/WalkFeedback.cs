using UnityEngine;

namespace DefaultNamespace
{
    public class WalkFeedback : MonoBehaviour
    {
        private TouchGroundChecker groundCheck;
        private bool wasGrounded;

        private void Start()
        {
            groundCheck = GetComponent<TouchGroundChecker>();
            wasGrounded = groundCheck.IsGrounded();
        }

        private void Update()
        {
            // 檢測是否剛剛著地
            if (groundCheck.IsGrounded() && !wasGrounded)
            {
                PlayLandingEffect();
            }
            wasGrounded = groundCheck.IsGrounded();
        }

        private void PlayLandingEffect()
        {
            Debug.Log("Landing");
            SFXManager.Instance.PlaySound("walk");
        }
    }
}