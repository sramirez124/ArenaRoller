using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    // in-game variables
    [SerializeField] private GameObject powerupPrefab1;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject enemyPrefabSmall;
    [SerializeField] private GameObject enemyPrefabMedium;
    [SerializeField] private int smallEnenmyCount;
    [SerializeField] private int mediumEnenmyCount;

    // menu variables
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject nextLevelMenu;
    public Text gameText;

    
    private float spawnRange = 9.0f;
    public float enemyCount;
    public int waveCount = 3;
    private bool spawnEnemies = true;

    // Start is called before the first frame update
    void Start()
    {
        EnemySpawner();
        Instantiate(powerupPrefab1, GenerateSpawnPosition(), powerupPrefab1.transform.rotation);
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
            SpawnEnemyWave(smallEnenmyCount, mediumEnenmyCount);
            smallEnenmyCount++;
            mediumEnenmyCount++;
            waveCount--;
            Instantiate(powerupPrefab1, GenerateSpawnPosition(), powerupPrefab1.transform.rotation);
        }
        else if (waveCount == 0)
        {
            nextLevelMenu.SetActive(true);
        }
    }

    private void SpawnEnemyWave(int smallEnemies, int mediumEnemies)
    {
        for (int i = 0; i < smallEnemies; i++)
        {
            Instantiate(enemyPrefabSmall, GenerateSpawnPosition(), enemyPrefabSmall.transform.rotation);
        }

        for (int i = 0; i < mediumEnemies; i++)
        {
            Instantiate(enemyPrefabMedium, GenerateSpawnPosition(), enemyPrefabMedium.transform.rotation);
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
        gameText.text = "Enemies Left: " + enemyCount.ToString() + " Waves Left: " + waveCount.ToString();
    }
}
