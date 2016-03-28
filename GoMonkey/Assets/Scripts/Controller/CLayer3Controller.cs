using UnityEngine;
using System.Collections;

public class CLayer3Controller : CMovableController
{
    public override void OnEndActionObject()
    {
        base.OnEndActionObject();
        m_GameManager.LoadRandomLayer3(GameType, m_Rigidbody2D.velocity.x);
    }
}
