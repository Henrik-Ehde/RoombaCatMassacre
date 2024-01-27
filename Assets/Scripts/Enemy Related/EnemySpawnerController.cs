using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{

    private void OnEnable()
    {
        //EnemySpawnManager.SpawnEnemiesEvent += SpawnEnemy;
    }

    public void SpawnEnemy(GameObject enemyToSpawn, Transform target, Transform enemyContainer)
    {
        BasicEnemyController spawnedEnemyController = Instantiate(enemyToSpawn, transform.position, Quaternion.identity, enemyContainer).GetComponent<BasicEnemyController>();
        spawnedEnemyController.SetTarget(target);
        spawnedEnemyController.enemyContainer = enemyContainer;
    }

}
