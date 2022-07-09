using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get;
        private set;
    }

    // ENCAPSULATION
    private bool _isGameActive = true;
    public bool IsGameActive
    {
        get { return _isGameActive; }
        private set { _isGameActive = value; }
    }

    public TextMeshProUGUI scoreText;

    public List<GameObject> enemyPrefabs;
    public List<GameObject> powerupPrefabs;

    [SerializeField] private float spawnXRange = 7.5f;
    [SerializeField] private float spawnY = 11.0f;

    [SerializeField] private float powerupSpawnRate = 0.09f; // How many powerups spawn per second (on average)

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseScreen;

    [SerializeField] private AudioClip musicEasy;
    [SerializeField] private AudioClip musicMedium;
    [SerializeField] private AudioClip musicHard;
    [SerializeField] private AudioClip musicGameOver;
    private AudioSource myAudio;

    private int score = 0;

    // Start is called before the first frame update
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

    private void Start()
    {
        myAudio = GetComponentInChildren<AudioSource>();
        Time.timeScale = 1;
        UpdateScore(0);
        float enemySpawnRate = 5.0f;
        myAudio.clip = musicEasy;
        if (DataManager.Instance != null)
        {
            switch (DataManager.Instance.difficulty)
            {
                case DataManager.Difficulty.EASY:
                    enemySpawnRate = 4.0f;
                    myAudio.clip = musicEasy;
                    break;
                case DataManager.Difficulty.MEDIUM:
                    enemySpawnRate = 2.5f;
                    myAudio.clip = musicMedium;
                    break;
                case DataManager.Difficulty.HARD:
                    enemySpawnRate = 1.75f;
                    myAudio.clip = musicHard;
                    break;
                default:
                    Debug.LogError("Unknown difficulty: " + DataManager.Instance.difficulty);
                    break;
            }
        }
        InvokeRepeating(nameof(SpawnRandomEnemy), 2.0f, enemySpawnRate);
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        IsGameActive = true;
        myAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsGameActive)
        {
            return;
        }

        // Spawn random powerup according to spawn rate
        if (Random.Range(0.0f, 1.0f) <= Time.deltaTime * powerupSpawnRate)
        {
            SpawnRandomPowerup();
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void Pause()
    {
        if (IsGameActive)
        {
            Time.timeScale = 0;
            IsGameActive = false;
            pauseScreen.SetActive(true);
        }
    }

    public void Resume()
    {
        IsGameActive = true;
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }

    public void GameOver()
    {
        if (IsGameActive)
        {
            Debug.Log("Game Over!");
            IsGameActive = false;
            CancelInvoke();
            gameOverScreen.SetActive(true);
            myAudio.clip = musicGameOver;
            myAudio.Play();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void SpawnRandomEnemy()
    {
        if (enemyPrefabs.Count > 0)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-spawnXRange, spawnXRange), spawnY, 0);
            int idx = Random.Range(0, enemyPrefabs.Count);
            Instantiate(enemyPrefabs[idx], spawnPos, enemyPrefabs[idx].transform.rotation);
        }
    }

    private void SpawnRandomPowerup()
    {
        if (powerupPrefabs.Count > 0)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-spawnXRange, spawnXRange), spawnY, 0);
            int idx = Random.Range(0, powerupPrefabs.Count);
            Instantiate(powerupPrefabs[idx], spawnPos, powerupPrefabs[idx].transform.rotation);
        }
    }
}
