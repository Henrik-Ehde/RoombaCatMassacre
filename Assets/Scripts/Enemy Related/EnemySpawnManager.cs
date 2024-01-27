//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    //public static event Action<GameObject, Transform, Transform> SpawnEnemiesEvent;

    public List<EnemySpawnerController> spawnerList;

    public GameObject enemyToSpawn;
    private GameObject player;
    public Transform enemiesContainer;
    public Transform spawnerContainer;

    //public float spawnInterval;

    [Header("Enemies Spawned per 10s")]
    public float spawnRate;
    public float rateIncrease;

    private void Start()
    {
        foreach (Transform transform in spawnerContainer)
        {
            EnemySpawnerController spawner = transform.GetComponent<EnemySpawnerController>();
            spawnerList.Add(spawner);

        }
        player = GameObject.Find("Player");

        StartCoroutine(IntermittentSpawn());

        //if (SpawnEnemiesEvent != null) SpawnEnemiesEvent(enemyToSpawn, player.transform, enemiesContainer);
    }

    IEnumerator IntermittentSpawn()
    {
        while (true)
        {
            float waitTime = 1 / (spawnRate * 0.1f);
            yield return new WaitForSeconds(waitTime);
            spawnRate += waitTime * rateIncrease * 0.1f;
            SpawnEnemy();
        }
    }

    

    void SpawnEnemy()
    {
        int pickedSpawner = Random.Range(0, spawnerList.Count);
        spawnerList[pickedSpawner].SpawnEnemy(enemyToSpawn, player.transform, enemiesContainer);
        
    }

}
