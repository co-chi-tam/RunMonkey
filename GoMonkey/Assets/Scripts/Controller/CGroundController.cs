using UnityEngine;
using System.Collections;

public class CGroundController : CMovableController
{

    [SerializeField]
    private GameObject m_BananaParentObject;
    private GameObject[] m_BananaObject;

    public override void Init()
    {
        base.Init();

        LoadAllBanana();
    }

    private void LoadAllBanana() {
        m_BananaObject = new GameObject[m_BananaParentObject.transform.childCount];
        for (int i = 0; i < m_BananaObject.Length; i++)
        {
            m_BananaObject[i] = m_BananaParentObject.transform.GetChild(i).gameObject;
        }
    }

    public override void OnEndActionObject()
    {
        base.OnEndActionObject();
        m_GameManager.LoadRandomGround(m_Rigidbody2D.velocity.x);
    }

    public override void SetActive(bool value)
    {
        base.SetActive(value);
        if (m_BananaObject == null)
            return;
        for (int i = 0; i < m_BananaObject.Length; i++)
        {
            m_BananaObject[i].SetActive(value);
        }
    }
}
