using UnityEngine;
using System.Collections;

public class CLayer2Controller : CMovableController
{
    public override void OnEndActionObject()
    {
        base.OnEndActionObject();
        m_GameManager.LoadRandomLayer2(GameType, m_Rigidbody2D.velocity.x);
    }
}
