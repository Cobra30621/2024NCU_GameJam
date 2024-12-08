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

    public GameObject cg;

    public GameObject restartButton;
    

    void Start()
    {
        // StartEndingSequence();
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
        BGMManager.Instance.PlayBGM("end");
        completePlayer.SetActive(false);
        shoeAnimator.gameObject.SetActive(true);
        playerAnimator.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        // 1. 播放玩家移動動畫，等待動畫播放完成
        playerAnimator.SetTrigger("Move");
        yield return WaitForAnimationToEnd(playerAnimator, "Wait_Leg_Falling");

        // 2. 播放鞋子落下動畫，等待動畫播放完成
        shoeAnimator.SetTrigger("Drop");
        yield return WaitForAnimationToEnd(shoeAnimator, "EndFalling");

        // 3. 玩家跳水
        playerAnimator.SetTrigger("Jump");
        yield return WaitForAnimationToEnd(playerAnimator, "EndJump");
        
        yield return new WaitForSeconds(1f);
        
        // 3. 播放閃光特效
        yield return effectController.FlashCoroutine(-1,() =>
        {
            cg.SetActive(true);
        });

        yield return new WaitForSeconds(5f);
        cg.SetActive(false);
        
        restartButton.gameObject.SetActive(true);
        
        completePlayer.SetActive(true);
        shoeAnimator.gameObject.SetActive(false);
        playerAnimator.gameObject.SetActive(false);

        Debug.Log("結局完成！");
    }

    private System.Collections.IEnumerator WaitForAnimationToEnd(Animator animator, string endAnimationName)
    {
        // 等待開始播放指定動畫
        yield return new WaitUntil(() =>
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            return stateInfo.IsName(endAnimationName);
        });
    }
}