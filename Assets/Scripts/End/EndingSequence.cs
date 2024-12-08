using Sirenix.OdinInspector;
using UnityEngine;

public class EndingSequence : MonoBehaviour
{
    public Animator playerAnimator;      // 玩家動畫控制器
    public Animator shoeAnimator;        // 鞋子動畫控制器
    public ScreenEffectController effectController; // 閃光特效

    public GameObject completePlayer;
    
    [SerializeField]
    private bool isEndingStarted = false;

    void Start()
    {
        StartEndingSequence();
    }

    [Button]
    public void StartEndingSequence()
    {
        if (isEndingStarted) return;
        isEndingStarted = true;

        // 開始整個結局流程
        StartCoroutine(PlayEndingSequence());
    }

    private System.Collections.IEnumerator PlayEndingSequence()
    {
        // 1. 播放玩家移動動畫，等待動畫播放完成
        playerAnimator.SetTrigger("Move");
        yield return WaitForAnimationToEnd(playerAnimator, "PlayerMove");

        // 2. 播放鞋子落下動畫，等待動畫播放完成
        shoeAnimator.SetTrigger("Drop");
        yield return WaitForAnimationToEnd(shoeAnimator, "ShoeDrop");

        // 3. 播放閃光特效
        yield return effectController.FlashCoroutine();
        
        completePlayer.SetActive(true);

        Debug.Log("結局完成！");
    }

    private System.Collections.IEnumerator WaitForAnimationToEnd(Animator animator, string animationName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);;
        
        // 等待開始播放指定動畫
        yield return new WaitUntil(() =>
        {
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            return stateInfo.IsName(animationName);
        });
        
        // 等待指定動畫播完
        while (stateInfo.IsName(animationName) || stateInfo.normalizedTime < 1f)
        {
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            Debug.Log("animation: " + animationName + stateInfo.IsName(animationName) + 
                      ", time: " + stateInfo.normalizedTime);
            yield return null; // 每幀等待
        }
    }
}