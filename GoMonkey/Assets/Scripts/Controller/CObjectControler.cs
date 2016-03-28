using UnityEngine;

public class CObjectController : MonoBehaviour {

    protected CGameManager m_GameManager;
    protected Transform m_Transform;
    protected BoxCollider2D m_BoxCollider;
    protected float m_MoveSpeed;

    public CEnum.EGameType GameType;
    public float XSize;
    public bool IsUsed;

    public virtual void Init()
    {
        m_GameManager = CGameManager.Instance;
        m_Transform = this.transform;
        m_BoxCollider = this.GetComponent<BoxCollider2D>();
    }

    public virtual void OnEnable()
    {

    }

    public virtual void OnDisable()
    {

    }

    public virtual void Start () {
	
	}

    public virtual void FixedUpdate () {

	}

    public virtual void Update()
    {

    }

    public virtual void OnEndActionObject()
    {

    }

    public virtual void OnResetObject() {

    }

    public virtual void SetActive(bool value)
    {
        this.gameObject.SetActive(value);
    }

    public virtual void SetSpeed(float value)
    {
        m_MoveSpeed = value;
    }

}
