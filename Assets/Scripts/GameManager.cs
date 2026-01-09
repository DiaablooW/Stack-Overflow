using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Players")]
    public PlayerController playerBlue;
    public PlayerController playerRed;

    [Header("UI")]
    public GameObject gameOverPanel;
    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI finalScoreText;

    [Header("Audio")]
    public AudioClip milestoneSound;
    private AudioSource audioSource;
    private int lastMilestone = 0;

    private bool isPlayerBlueActive = true;
    private float score = 0f;
    private bool isGameOver = false;

    [Header("Difficulty Progression")]
    public float baseSpeed = 5f;
    public float speedIncreaseRate = 0.3f;
    public float maxSpeedMultiplier = 2.5f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        // Setup audio
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        UpdateActivePlayer();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoToMenu();
            return;
        }

        if (isGameOver)
            return;

        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            SwitchPlayer();
        }

        score += Time.deltaTime * 10f;

        if (scoreText != null)
            scoreText.text = "Score: " + Mathf.FloorToInt(score);

        // Check for milestone sounds (every 250 points)
        int currentMilestone = Mathf.FloorToInt(score / 250f);
        if (currentMilestone > lastMilestone)
        {
            lastMilestone = currentMilestone;
            PlayMilestoneSound();
        }

        UpdateObstacleSpeed();
    }

    void PlayMilestoneSound()
    {
        if (milestoneSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(milestoneSound);
        }
    }

    void UpdateObstacleSpeed()
    {
        float multiplier = 1f + (score / 100f) * speedIncreaseRate;
        multiplier = Mathf.Min(multiplier, maxSpeedMultiplier);
        Obstacle.globalSpeed = baseSpeed * multiplier;
    }

    void SwitchPlayer()
    {
        isPlayerBlueActive = !isPlayerBlueActive;
        UpdateActivePlayer();
    }

    void UpdateActivePlayer()
    {
        playerBlue.SetActive(isPlayerBlueActive);
        playerRed.SetActive(!isPlayerBlueActive);
    }

    public void GameOver()
    {
        if (isGameOver)
            return;

        isGameOver = true;
        Time.timeScale = 0f;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            if (finalScoreText != null)
                finalScoreText.text = "" + Mathf.FloorToInt(score);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        Obstacle.globalSpeed = 5f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        Obstacle.globalSpeed = 5f;
        SceneManager.LoadScene("MainMenu");
    }

    public float GetElapsedTime()
    {
        return score / 10f;
    }
}