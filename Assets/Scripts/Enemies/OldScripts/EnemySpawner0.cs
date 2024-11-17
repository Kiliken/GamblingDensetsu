// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class EnemySpawner : MonoBehaviour
// {
//     [SerializeField] Transform[] enemySpawnPoint;
//     [SerializeField] GameObject[] enemy;
//     [SerializeField] float spawnTime;
//     [SerializeField] GameObject enemyContainer;

//     int enemyCount;
//     int dif;
//     float timer;
//     bool spawnStatus;
//     GameObject newEnemy;

//     // Start is called before the first frame update
//     void Start()
//     {
//         enemyCount = 4;
//         dif = 1;
//         spawnStatus = true;
//         timer = 0;
//     }

//     // Update is called once per frame
//     private void Update()
//     {
//         SpawnEnemy();
//         timer += Time.deltaTime;
//         if (timer > spawnTime)
//         {
//             spawnStatus = true;
//             timer = 0;
//             dif++;
//             enemyCount = 2 * dif;
//         }
//     }

//     private void SpawnEnemy()
//     {
//         if (spawnStatus && enemyCount > 0)
//         {
//             newEnemy = Instantiate(enemy[Random.Range(0, enemy.Length)], enemySpawnPoint[Random.Range(0, enemySpawnPoint.Length)].position, Quaternion.identity);
//             newEnemy.transform.parent = enemyContainer.transform;
//             enemyCount--;
//         }
//         else
//         {
//             spawnStatus = false;
//         }

//     }
// }
