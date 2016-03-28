using UnityEngine;
using System.Collections;

public class CTrapController : CObjectController {

    private Vector3 m_StartPosition;

    public override void Init()
    {
        base.Init();
        m_StartPosition = m_Transform.position;
    }

    public override void Start()
    {
        base.Start();
        Init();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void OnResetObject()
    {
        base.OnResetObject();
        m_Transform.position = m_StartPosition;
    }

}
