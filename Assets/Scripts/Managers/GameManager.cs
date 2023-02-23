using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private Camera m_camera;
    [SerializeField] private GameInfo m_gameInfoPanel;
    [SerializeField] private GameObject m_playerPrefab;
    [SerializeField] private Transform m_instantiationParent;
    
    public GameSettingsScriptableObject GameSettings;
    public Player Player { get; private set; }
    
    private FollowTarget m_cameraFollow = new FollowTarget();
    
    private bool m_gameIsPlaying;
    
    // Timer
    private float m_gameTimeLeft;
    
    // Spawn Enemies Timer
    private float m_enemySpawnTime;
    private float m_spawnEnemyTimeLeft;
    private EnemySpawner m_enemySpawner = new EnemySpawner();

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    private void Start()
    {
        GameEvents.Instance.OnGameStart += StartGame;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnGameStart -= StartGame;
    }

    // Update is called once per frame
    void Update()
    {
        // Game Timer
        CountDownTime();
        SpawnEnemyTimer();
        
        // Camera Pos
        m_cameraFollow.Follow();
    }
    
    private void StartGame(float gameTime, float enemySpawnTime)
    {
        PoolingManager.Instance.DisableAll();
        
        if (Player) Player.Reset();
        else Player = Instantiate(m_playerPrefab, m_instantiationParent).GetComponent<Player>();

        Transform playerTransform = Player.transform;
        playerTransform.position = new Vector2(0, 0);
        
        m_cameraFollow.Setup(m_camera.transform, playerTransform);

        m_gameIsPlaying = true;
        
        // Start Timers
        m_gameTimeLeft = gameTime;
        m_enemySpawnTime = enemySpawnTime;
        m_spawnEnemyTimeLeft = m_enemySpawnTime;
    }
    
    private void GameOver()
    {
        m_gameIsPlaying = false;
        GameEvents.Instance.GameOver();
    }

    #region Game Timer

    private void CountDownTime()
    {
        if (m_gameIsPlaying)
        {
            if (m_gameTimeLeft > 0)
            {
                m_gameTimeLeft -= Time.deltaTime;
            }
            else
            {
                GameOver();
                m_gameTimeLeft = 0;
            }
            UpdateTimer(m_gameTimeLeft);
        }
    }

    private void SpawnEnemyTimer()
    {
        if (m_gameIsPlaying)
        {
            if (m_spawnEnemyTimeLeft > 0)
            {
                m_spawnEnemyTimeLeft -= Time.deltaTime;
            }
            else
            {
                m_enemySpawner.SpawnEnemy();
                m_spawnEnemyTimeLeft = m_enemySpawnTime;
            }
        }
    }

    private void UpdateTimer(float timeLeft)
    {
        float minutes = Mathf.RoundToInt(timeLeft / 60);
        float seconds = Mathf.RoundToInt(timeLeft % 60);

        m_gameInfoPanel.SetGameTime(string.Format("{0:00}:{1:00}", minutes, seconds));
    }
    
    #endregion

}
