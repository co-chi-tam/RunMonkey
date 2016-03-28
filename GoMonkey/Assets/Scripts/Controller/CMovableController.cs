using UnityEngine;
using System.Collections;

public class CMovableController : CObjectController
{
    protected Rigidbody2D m_Rigidbody2D;

    public override void Init() {
        base.Init();
        m_Rigidbody2D = this.GetComponent<Rigidbody2D>();
        XSize = m_BoxCollider.size.x * m_Transform.localScale.x;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        m_Rigidbody2D.velocity = Vector2.left * m_MoveSpeed * m_GameManager.SpeedBoost;
        if (m_Transform.position.x <= 0f && IsUsed == false)
        {
            OnEndActionObject();
            IsUsed = true;
        }
        if (m_Transform.position.x <= -XSize)
        {
            OnResetObject();
        }
    }

    public override void OnResetObject()
    {
        base.OnResetObject();
        m_GameManager.DeleteOldObject(GameType, this);
    }
}
