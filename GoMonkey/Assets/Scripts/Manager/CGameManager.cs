using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ObjectPool;

public class CGameManager : MonoBehaviour {

    #region Singleton
    
    private static CGameManager m_Instance;
    private static object m_SingtonObject = new object();

    public static CGameManager Instance {
        get
        {
            lock (m_SingtonObject)
            {
                if (m_Instance == null)
                {
                    var go = new GameObject("GameManager");
                    m_Instance = go.AddComponent<CGameManager>();
                }
                return m_Instance;
            }
        }      
    }

    #endregion

    #region Properties

    [Header("Speed boost")]
    [Range(0f, 100f)]
    public float SpeedBoost = 1f;
    public float LimitSpeedBoost = 10f;
    public float m_TimeToLevelUp = 30f;
    public float m_SpeedBoostPerLevelUp = 0.5f;

    [Header("Object Speed")]
    public float GroundMoveSpeed = 5f;
    public float Layer_1_MoveSpeed = 3f;
    public float Layer_2_MoveSpeed = 3f;
    public float Layer_3_MoveSpeed = 3f;
    public float FGMoveSpeed = 1f;

    [Header("Game Status")]
    public bool LoadingComplete;
    public bool PlayerFail;

    private float m_CurrentTime;
    private int m_FGPrefabCount;
    private int m_GroundPrefabCount;
    private int m_Layer_1PrefabCount;
    private int m_Layer_2PrefabCount;
    private int m_Layer_3PrefabCount;
    private Dictionary<CEnum.EGameType, ObjectPool<CObjectController>> m_GamePool;

    #endregion

    #region Implementation MonoBehaviour

    void Awake() {
        m_Instance = this;
    }

    void Start() {
        m_GamePool = new Dictionary<CEnum.EGameType, ObjectPool<CObjectController>>();
        m_CurrentTime = Time.time;
        LoadObjectPool();
    }

    void Update() {
        if (Time.time - m_CurrentTime >= m_TimeToLevelUp && LoadingComplete && PlayerFail)
        {
            SpeedBoost = SpeedBoost < LimitSpeedBoost ? SpeedBoost + m_SpeedBoostPerLevelUp : LimitSpeedBoost;
            m_CurrentTime = Time.time;
        }
    }

    #endregion

    #region Main method

    private void LoadObjectPool() {
        #region Ground
        var groundPrefabs = Resources.LoadAll<GameObject>("Ground");
        m_GroundPrefabCount = groundPrefabs.Length;
        for (int i = 0; i < groundPrefabs.Length; i++)
        {
            var index = CEnum.EGameType.Ground + i;
            m_GamePool[index] = new ObjectPool<CObjectController>();
            for (int j = 0; j < 5; j++)
            {
                var groundController = Instantiate(groundPrefabs[i]).GetComponent<CGroundController>();
                groundController.transform.SetParent(this.transform);
                groundController.SetActive(false);
                groundController.GameType = index;
                groundController.SetSpeed(GroundMoveSpeed);
                groundController.Init();
                m_GamePool[index].Create(groundController);
            }
        }
        #endregion

        #region Layer 1
        var layer1Prefabs = Resources.LoadAll<GameObject>("Layer/Layer 1");
        m_Layer_1PrefabCount = layer1Prefabs.Length;
        for (int i = 0; i < layer1Prefabs.Length; i++)
        {
            var index = CEnum.EGameType.Layer_1 + i + 1;
            m_GamePool[index] = new ObjectPool<CObjectController>();
            for (int j = 0; j < 5; j++)
            {
                var movableController = Instantiate(layer1Prefabs[i]).AddComponent<CLayer1Controller>();
                movableController.transform.SetParent(this.transform);
                movableController.Init();
                movableController.SetActive(false);
                movableController.GameType = index;
                movableController.SetSpeed(Layer_1_MoveSpeed);
                m_GamePool[index].Create(movableController);
            }
        }
        #endregion

        #region Layer 2
        var layer2Prefabs = Resources.LoadAll<GameObject>("Layer/Layer 2");
        m_Layer_2PrefabCount = layer2Prefabs.Length;
        for (int i = 0; i < layer2Prefabs.Length; i++)
        {
            var index = CEnum.EGameType.Layer_2 + i + 1;
            m_GamePool[index] = new ObjectPool<CObjectController>();
            for (int j = 0; j < 5; j++)
            {
                var movableController = Instantiate(layer2Prefabs[i]).AddComponent<CLayer2Controller>();
                movableController.transform.SetParent(this.transform);
                movableController.Init();
                movableController.SetActive(false);
                movableController.GameType = index;
                movableController.SetSpeed(Layer_2_MoveSpeed);
                m_GamePool[index].Create(movableController);
            }
        }
        #endregion

        #region Layer 3
        var layer3Prefabs = Resources.LoadAll<GameObject>("Layer/Layer 3");
        m_Layer_3PrefabCount = layer3Prefabs.Length;
        for (int i = 0; i < layer3Prefabs.Length; i++)
        {
            var index = CEnum.EGameType.Layer_3 + i + 1;
            m_GamePool[index] = new ObjectPool<CObjectController>();
            for (int j = 0; j < 5; j++)
            {
                var movableController = Instantiate(layer3Prefabs[i]).AddComponent<CLayer3Controller>();
                movableController.transform.SetParent(this.transform);
                movableController.Init();
                movableController.SetActive(false);
                movableController.GameType = index;
                movableController.SetSpeed(Layer_3_MoveSpeed);
                m_GamePool[index].Create(movableController);
            }
        }
        #endregion

        #region Foreground
        var fgPrefabs = Resources.LoadAll<GameObject>("FG");
        m_FGPrefabCount = fgPrefabs.Length;
        for (int i = 0; i < fgPrefabs.Length; i++)
        {
            var index = CEnum.EGameType.FG + i + 1;
            m_GamePool[index] = new ObjectPool<CObjectController>();
            for (int j = 0; j < 5; j++)
            {
                var fgController = Instantiate(fgPrefabs[i]).AddComponent<CFGController>();
                fgController.transform.SetParent(this.transform);
                fgController.Init();
                fgController.SetActive(false);
                fgController.GameType = index;
                fgController.SetSpeed(FGMoveSpeed);
                m_GamePool[index].Create(fgController);
            }
        }
        #endregion

        LoadFirstObject();
        LoadPlayer();
    }

    private void LoadFirstObject()
    {

        var layer1Controller = LoadObject(CEnum.EGameType.Layer_1a);
        var layer1Position = Vector3.zero;
        layer1Position.y = layer1Controller.transform.position.y;
        layer1Controller.transform.position = layer1Position;

        var layer2Controller = LoadObject(CEnum.EGameType.Layer_2a);
        var layer2Position = Vector3.zero;
        layer2Position.y = layer2Controller.transform.position.y;
        layer2Controller.transform.position = layer2Position;

        var layer3Controller = LoadObject(CEnum.EGameType.Layer_3a);
        var layer3Position = Vector3.zero;
        layer3Position.y = layer3Controller.transform.position.y;
        layer3Controller.transform.position = layer3Position;

        var fgController = LoadObject(CEnum.EGameType.FG_1);
        var fgPosition = Vector3.zero;
        fgPosition.y = fgController.transform.position.y;
        fgController.transform.position = fgPosition;

        var groundController = LoadObject(CEnum.EGameType.Ground);
        var gPosition = groundController.transform.position;
        gPosition.x = groundController.XSize / 7f;
        groundController.transform.position = gPosition;
    }

    private void LoadPlayer() {
        var playerInstance = Resources.Load<GameObject>("Player/Player");
        var player = Instantiate<GameObject>(playerInstance);
        var playerController = player.GetComponent<CPlayerController>();
        playerController.GameType = CEnum.EGameType.Player;
        playerController.Init();
        playerController.SetActive(true);
        LoadingComplete = PlayerFail = true;
    }

    public CObjectController LoadRandomGround(float offset = 0f) {
        var random = 0;
        if (m_GroundPrefabCount > 1)
        {
            random = Random.Range(0, 9999) % (m_GroundPrefabCount - 1) + 1;
        }
        return LoadObject(CEnum.EGameType.Ground + random, offset);
    }

    public CObjectController LoadRandomLayer1(CEnum.EGameType objectType, float offset = 0f)
    {
        var random = 1;
        if (m_Layer_1PrefabCount > 0)
        {
            random = Random.Range(0, 9999) % m_Layer_1PrefabCount + 1;
        }
        return LoadObject(CEnum.EGameType.Layer_1 + random, offset);
    }

    public CObjectController LoadRandomLayer2(CEnum.EGameType objectType, float offset = 0f)
    {
        var random = 1;
        if (m_Layer_2PrefabCount > 0)
        {
            random = Random.Range(0, 9999) % m_Layer_2PrefabCount + 1;
        }
        return LoadObject(CEnum.EGameType.Layer_2 + random, offset);
    }

    public CObjectController LoadRandomLayer3(CEnum.EGameType objectType, float offset = 0f)
    {
        var random = 1;
        if (m_Layer_3PrefabCount > 0)
        {
            random = Random.Range(0, 9999) % m_Layer_3PrefabCount + 1;
        }
        return LoadObject(CEnum.EGameType.Layer_3 + random, offset);
    }

    public CObjectController LoadRandomForeground(CEnum.EGameType objectType, float offset = 0f)
    {
        var random = 1;
        if (m_FGPrefabCount > 0)
        {
            random = Random.Range(0, 9999) % m_FGPrefabCount + 1;
        }
        return LoadObject(CEnum.EGameType.FG + random, offset);
    }

    public CObjectController LoadObject(CEnum.EGameType objectType, float offset = 0f) {
        var objectController = m_GamePool[objectType].Get();
        var position = Vector3.right * (objectController.XSize + ((offset - SpeedBoost) * Time.fixedDeltaTime) + (offset * Time.fixedDeltaTime));
        position.y = objectController.transform.position.y;
        objectController.transform.position = position;
        objectController.IsUsed = false;
        objectController.SetActive(true);
        return objectController;
    }

    public void DeleteOldObject(CEnum.EGameType gameType, CObjectController groundController) {
        m_GamePool[gameType].Set(groundController);
        groundController.SetActive(false);
    }

    public void PlayerHaveFail() {
        PlayerFail = false;
        SpeedBoost = 0f;
        m_CurrentTime = Time.time;
    }

    #endregion

}
