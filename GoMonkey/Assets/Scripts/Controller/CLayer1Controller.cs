using UnityEngine;
using System.Collections;

public class CLayer1Controller : CMovableController
{
    public override void OnEndActionObject()
    {
        base.OnEndActionObject();
        m_GameManager.LoadRandomLayer1(GameType, m_Rigidbody2D.velocity.x);
    }
}
