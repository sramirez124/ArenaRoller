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
    public float waveCount = 3;

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
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (waveCount >= 0)
        {
            enemiesToSpawn++;
            SpawnEnemyWave(enemiesToSpawn);
            Instantiate(powerupPrefab0, GenerateSpawnPosition(), powerupPrefab0.transform.rotation);
            waveCount--;
            gameOverMenu.SetActive(false);
            if (waveCount == 0)
            {
                nextLevelMenu.SetActive(true);
                enemiesToSpawn = 0;
            }
        }

        if (Player.transform.position.y < -10)
        {
            Destroy(gameObject);
            gameOverMenu.SetActive(true);
        }

        if (waveCount == 0)
        {
            nextLevelMenu.SetActive(true);
        }
        MenuUpdate();
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
