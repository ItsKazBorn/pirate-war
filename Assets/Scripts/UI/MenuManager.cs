using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject m_mainMenu;
    [SerializeField] private GameObject m_menuPanel;
    [SerializeField] private GameObject m_configPanel;
    [SerializeField] private GameObject m_gameInfoPanel;
    [SerializeField] private GameObject m_gameOverPanel;

    [Header("Buttons")] 
    [SerializeField] private Button m_startGameButton;
    [SerializeField] private Button m_optionsButton;
    [SerializeField] private Button m_backButton;
    [SerializeField] private Button m_restartGameButton;
    [SerializeField] private Button m_menuButton;

    [Header("Sliders")]
    [SerializeField] private CustomSlider m_gameTimeSlider;
    [SerializeField] private CustomSlider m_enemySpawnSlider;

    private GameSettingsScriptableObject m_gameSettings;

    // Start is called before the first frame update
    void Start()
    {
        m_mainMenu.SetActive(true);
        m_menuPanel.SetActive(true);
        m_configPanel.SetActive(false);
        m_gameInfoPanel.SetActive(false);
        m_gameOverPanel.SetActive(false);

        m_startGameButton.onClick.AddListener(StartGame);
        m_optionsButton.onClick.AddListener(OpenConfigPanel);
        m_backButton.onClick.AddListener(GoToMenu);
        m_menuButton.onClick.AddListener(GoToMenu);
        m_restartGameButton.onClick.AddListener(StartGame);

        m_gameSettings = GameManager.Instance.GameSettings;

        GameEvents.Instance.OnGameOver += GameOver;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnGameOver -= GameOver;
    }

    private void OpenConfigPanel()
    {
        m_menuPanel.SetActive(false);
        m_configPanel.SetActive(true);
        
        float min = m_gameSettings.MinGameTime;
        float max = m_gameSettings.MaxGameTime;
        m_gameTimeSlider.Setup(min, max);
        
        min = m_gameSettings.MinEnemySpawnTime;
        max = m_gameSettings.MaxEnemySpawnTime;
        m_enemySpawnSlider.Setup(min, max);
    }

    private void GoToMenu()
    {
        m_mainMenu.SetActive(true);
        m_menuPanel.SetActive(true);
        m_configPanel.SetActive(false);
        m_gameOverPanel.SetActive(false);
    }

    private void StartGame()
    {
        m_mainMenu.SetActive(false);
        m_gameOverPanel.SetActive(false);
        m_gameInfoPanel.SetActive(true);
        GameEvents.Instance.GameStart(m_gameTimeSlider.GetValue(), m_enemySpawnSlider.GetValue());
    }

    private void GameOver()
    {
        m_gameOverPanel.SetActive(true);
    }
}
