using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Transform[] enemySpawnPoint;
    [SerializeField] GameObject[] enemy;
    [SerializeField] float spawnTime;

    int enemyCount;
    int dif;
    float timer;
    bool spawnStatus;

    // Start is called before the first frame update
    void Start()
    {
        enemyCount = 4;
        dif = 0;
        spawnStatus = true;
        timer = 0;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        SpawnEnemy();
        timer += 1 * Time.deltaTime;
        if (timer > spawnTime){
            spawnStatus = true;
            timer = 0;
            dif++;
            enemyCount = 2 * dif;
        }
    }

    private void SpawnEnemy() {
        if (spawnStatus && enemyCount > 0)
        {
            Instantiate(enemy[Random.Range(0, enemy.Length)], enemySpawnPoint[Random.Range(0,enemySpawnPoint.Length)].position, Quaternion.identity);
            enemyCount--;
        }
        else { 
            spawnStatus = false;
        }

    }
}
