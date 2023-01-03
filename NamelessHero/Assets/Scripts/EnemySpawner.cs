using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //Gets enemy sprite (can be changed in inspector)
    [SerializeField] private GameObject enemyOnePrefab;
    [SerializeField] private GameObject enemyTwoPrefab;
    [SerializeField] private GameObject enemyThreePrefab;

    //Sets time between each spawn (can be changed in inspector)
    [SerializeField] private float enemyOneInterval = 1f;
    [SerializeField] private float enemyTwoInterval = 1.5f;
    [SerializeField] private float enemyThreeInterval = 1.5f;



    void Start()
    {
        StartCoroutine(spawnEnemy(enemyOneInterval, enemyOnePrefab));
        StartCoroutine(spawnEnemy(enemyTwoInterval, enemyTwoPrefab));
        StartCoroutine(spawnEnemy(enemyThreeInterval, enemyThreePrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {

        //Sets the position in which the enemy is spawned
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(50, -1.25f, 0), Quaternion.identity);

        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
