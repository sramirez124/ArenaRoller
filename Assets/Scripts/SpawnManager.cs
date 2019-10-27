﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private int enemiesToSpawn = 3;
    [SerializeField] private GameObject powerupPrefab0;
    [SerializeField] private GameObject powerupPrefab1;
    [SerializeField] private GameObject powerupPrefab2;

    public GameObject enemyPrefab;
    private float spawnRange = 9.0f;
    public int enemyCount;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(enemiesToSpawn);
        Instantiate(powerupPrefab0, GenerateSpawnPosition(), powerupPrefab0.transform.rotation);
        Instantiate(powerupPrefab1, GenerateSpawnPosition(), powerupPrefab1.transform.rotation);
        Instantiate(powerupPrefab2, GenerateSpawnPosition(), powerupPrefab2.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            enemiesToSpawn++;
            SpawnEnemyWave(enemiesToSpawn);
            Instantiate(powerupPrefab0, GenerateSpawnPosition(), powerupPrefab0.transform.rotation);
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
}