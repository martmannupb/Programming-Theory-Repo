using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get;
        private set;
    }

    public TextMeshProUGUI scoreText;

    public List<GameObject> enemyPrefabs;
    public List<GameObject> powerupPrefabs;

    [SerializeField] private float spawnXRange = 7.5f;
    [SerializeField] private float spawnY = 10.0f;

    [SerializeField] private float powerupSpawnRate = 0.1f; // How many powerups spawn per second (on average)

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
        UpdateScore(0);
        InvokeRepeating(nameof(SpawnRandomEnemy), 1.0f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
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