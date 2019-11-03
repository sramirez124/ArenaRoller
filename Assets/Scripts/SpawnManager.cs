using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    // in-game variables
    [SerializeField] private int enemiesToSpawn = 3;
    [SerializeField] private GameObject powerupPrefab0;
    [SerializeField] private GameObject powerupPrefab1;
    [SerializeField] private GameObject powerupPrefab2;
    [SerializeField] private GameObject Player;

    // menu variables
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject nextLevelMenu;
    public Text gameText;

    public GameObject enemyPrefab;
    private float spawnRange = 9.0f;
    public float enemyCount;
    public int waveCount = 3;
    private bool spawnEnemies = true;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(enemiesToSpawn);
        Instantiate(powerupPrefab0, GenerateSpawnPosition(), powerupPrefab0.transform.rotation);
        //Instantiate(powerupPrefab1, GenerateSpawnPosition(), powerupPrefab1.transform.rotation);
        //Instantiate(powerupPrefab2, GenerateSpawnPosition(), powerupPrefab2.transform.rotation);
        MenuUpdate();

        gameOverMenu.SetActive(false);
        nextLevelMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        EnemySpawner();
        if (Player.transform.position.y < -10)
        {
            Destroy(gameObject);
            gameOverMenu.SetActive(true);
        }
        MenuUpdate();
    }

    private void EnemySpawner()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0 && waveCount >= 0)
        {
            SpawnEnemyWave(enemiesToSpawn);
            enemiesToSpawn++;
            Instantiate(powerupPrefab0, GenerateSpawnPosition(), powerupPrefab0.transform.rotation);
            waveCount--;
        }
        else if (waveCount == 0)
        {
            spawnEnemies = false;
            nextLevelMenu.SetActive(true);
            enemiesToSpawn = 0;
        }
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

    private void MenuUpdate()
    {
        gameText.text = "Enemies Left: " + enemyCount.ToString() + "Waves Left: " + waveCount.ToString();
    }
}
