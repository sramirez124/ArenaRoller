using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    // in-game variables
    [SerializeField] private GameObject powerupPrefab1;
    [SerializeField] private GameObject powerupPrefab2;
    [SerializeField] private GameObject powerupPrefab3;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject enemyPrefabSmall;
    [SerializeField] private GameObject enemyPrefabMedium;
    [SerializeField] private GameObject enemyPrefabLarge;
    [SerializeField] private int smallEnenmyCount;
    [SerializeField] private int mediumEnenmyCount;
    [SerializeField] private int largeEnenmyCount;

    // menu variables
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject nextLevelMenu;
    public Text gameText;

    
    [SerializeField] private float spawnRangeX = 9.0f;
    [SerializeField] private float spawnRangeZ = 9.0f;
    public float enemyCount;
    public int waveCount = 3;
    private bool spawnEnemies = true;

    // Start is called before the first frame update
    void Start()
    {
        EnemySpawner();
        MenuUpdate();

        gameOverMenu.SetActive(false);
        nextLevelMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        EnemySpawner();
        if (Player.transform.position.y < - 4)
        {
            gameOverMenu.SetActive(true);
        }
        MenuUpdate();

        
    }

    private void EnemySpawner()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0 && waveCount >= 1)
        {
            SpawnEnemyWave(smallEnenmyCount, mediumEnenmyCount, largeEnenmyCount);
            smallEnenmyCount++;
            mediumEnenmyCount++;
            waveCount--;
            Instantiate(powerupPrefab1, GenerateSpawnPosition(), powerupPrefab1.transform.rotation);
            Instantiate(powerupPrefab2, GenerateSpawnPosition(), powerupPrefab2.transform.rotation);
            //Instantiate(powerupPrefab3, GenerateSpawnPosition(), powerupPrefab3.transform.rotation);
        }
        else if (waveCount == 0 && enemyCount == 0)
        {
            nextLevelMenu.SetActive(true);
        }
    }

    private void SpawnEnemyWave(int smallEnemies, int mediumEnemies, int largeEnemies)
    {
        for (int i = 0; i < smallEnemies; i++)
        {
            Instantiate(enemyPrefabSmall, GenerateSpawnPosition(), enemyPrefabSmall.transform.rotation);
        }

        for (int i = 0; i < mediumEnemies; i++)
        {
            Instantiate(enemyPrefabMedium, GenerateSpawnPosition(), enemyPrefabMedium.transform.rotation);
        }

        for (int i = 0; i < largeEnemies; i++)
        {
            Instantiate(enemyPrefabLarge, GenerateSpawnPosition(), enemyPrefabMedium.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        float spawnPosZ = Random.Range(-spawnRangeZ, spawnRangeZ);

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

    private void MenuUpdate()
    {
        gameText.text = "Enemies Left: " + enemyCount.ToString() + " Waves Left: " + waveCount.ToString();
    }
}
