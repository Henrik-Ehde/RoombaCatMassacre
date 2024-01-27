using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static event Action<GameObject, Transform> SpawnEnemiesEvent;

    public GameObject enemyToSpawn;
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");

        if (SpawnEnemiesEvent != null) SpawnEnemiesEvent(enemyToSpawn, player.transform);
    }

}
