using UnityEngine;
using System.Collections;

public class CFGController : CMovableController
{
    public override void OnEndActionObject()
    {
        base.OnEndActionObject();
        m_GameManager.LoadRandomForeground(GameType, m_Rigidbody2D.velocity.x);
    }
}
