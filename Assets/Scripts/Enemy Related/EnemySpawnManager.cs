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

    public float spawnInterval;

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
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
        }
    }

    

    void SpawnEnemy()
    {
        int pickedSpawner = Random.Range(0, spawnerList.Count);
        spawnerList[pickedSpawner].SpawnEnemy(enemyToSpawn, player.transform, enemiesContainer);
        
    }

}
