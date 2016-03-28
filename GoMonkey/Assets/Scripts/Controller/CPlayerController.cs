using UnityEngine;
using System.Collections;

public class CPlayerController : CObjectController
{
    [SerializeField]
    private float m_JumpSpeed = 6f;
    [SerializeField]
    private int m_IsJump = 0;
    [SerializeField]
    private int m_JumpCount = 2;
    [SerializeField]
    private float m_CooldownBetweenJumps = 0f; 

    private bool m_InGround;
    private float m_LastJumpTime;
    private Rigidbody2D m_Rigidbody2D;
    private SkeletonAnimation m_SkeletonAnimation;

    public int BananaCount = 0;

	public override void Init () {
        base.Init();
        m_SkeletonAnimation = this.GetComponent<SkeletonAnimation>();
        m_Rigidbody2D = this.GetComponent<Rigidbody2D>();
        SetAnimation(CEnum.EPlayerState.Run);
        m_LastJumpTime = Time.time;
    }

    public override void FixedUpdate() {
        base.FixedUpdate();
        if (InputJump())
        {
            if (Time.time - m_LastJumpTime < m_CooldownBetweenJumps)
            {
                return;
            }
            var direction = Vector2.up * m_JumpSpeed;
            m_Rigidbody2D.AddForce(direction, ForceMode2D.Impulse);
            m_IsJump += 1;
            m_InGround = false;
            m_LastJumpTime = Time.time;
        }
        SetAnimation(CEnum.EPlayerState.Run);
    }

    private bool InputJump() {
        var isInput = false;
        if (Input.touchCount == 1 || Input.GetKey(KeyCode.A) || Input.GetMouseButton(0))
        {
            isInput = m_IsJump < m_JumpCount;
        }
        return isInput;
    }

    public void SetAnimation(CEnum.EPlayerState animation) {
        if (m_SkeletonAnimation != null)
        {
            m_SkeletonAnimation.AnimationName = "animation";
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.name.IndexOf("Ground") != -1)
        {
            m_IsJump = 0;
            m_InGround = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.IndexOf("Banana") != -1)
        {
            BananaCount += 1;
            other.gameObject.SetActive(false);
        }
        else if (other.name.IndexOf("Trap") != -1)
        {
            //m_GameManager.PlayerHaveFail();
        }
    }

}
