using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{

    private void OnEnable()
    {
        EnemySpawnManager.SpawnEnemiesEvent += SpawnEnemy;
    }

    private void SpawnEnemy(GameObject enemyToSpawn, Transform target)
    {
        BasicEnemyController spawnedEnemyController = Instantiate(enemyToSpawn, transform.position, Quaternion.identity).GetComponent<BasicEnemyController>();
        spawnedEnemyController.SetTarget(target);
    }

}
