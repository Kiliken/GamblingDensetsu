using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform[] enemySpawnPoint;
    // add from spawn rate lowest to highest
    [SerializeField] GameObject[] enemy;
    private int waveNo = 1;
    private int enemyNo;
    // spawn rates of each enemy above
    [SerializeField] int[] spawnRate;
    [SerializeField] float spawnTime = 5f;
    private float spawnTimeMax = 5f;
    private float spawnTimeMin = 5f;
    private float spawnTimer = 0f;
    [SerializeField] GameObject enemyContainer;

    //[SerializeField] int spawnCount = 1;
    [SerializeField] int spawnRateIncrease = 2;
    int rateSum = 0;
    [SerializeField] float nextWaveTime = 30f;
    float nextWaveTimer = 0f;
    public bool spawnStatus = true;
    GameObject newEnemy;

    // Start is called before the first frame update
    void Start()
    {
        enemyNo = enemy.Length;
    }

    // Update is called once per frame
    private void Update()
    {
        if(spawnStatus){
            if(spawnTimer < spawnTime){
                spawnTimer += Time.deltaTime;
            }
            else{
                SpawnEnemy();
                spawnTimer = 0f;
                spawnTime = Random.Range(spawnTimeMin, spawnTimeMax);
            }

            if(nextWaveTimer < nextWaveTime){
                nextWaveTimer += Time.deltaTime;
            }
            else{
                NextWave();
                nextWaveTimer = 0f;
            }
        }
        
    }


    private void SpawnEnemy(){
        rateSum = 0;
        for(int i = 0; i < enemyNo; i++){
            rateSum += spawnRate[i];
        }
        //Debug.Log("Rate sum: " + rateSum);

        int r = Random.Range(0, rateSum + 1);
        int e = enemyNo - 1;
        for(int i = 0; i < enemyNo; i++){
            if(r < spawnRate[i]){
                e = i;
                break;
            }
            r -= spawnRate[i];
        }

        newEnemy = Instantiate(enemy[e], enemySpawnPoint[Random.Range(0, enemySpawnPoint.Length)].position, Quaternion.identity);
        newEnemy.transform.parent = enemyContainer.transform;
        if(waveNo > 1){
            newEnemy.GetComponent<Enemy>().StrengthenEnemy(waveNo);
        }
        Debug.Log("Enemy spawned " + e);
    }


    // private void IncreaseSpawnRates(){
    //     for(int i = 0; i < enemyNo; i++){
    //         spawnRate[i] += spawnRateIncrease;
    //     }
    //     Debug.Log("Spawn rates increased");
    // }


    private void NextWave(){
        waveNo += 1;
        if(waveNo == 2){
            spawnTimeMin = Mathf.Max(2, spawnTimeMin - 1);
        }
        else{
            spawnTimeMin = Mathf.Max(2, spawnTimeMin - 1);
            spawnTimeMax = Mathf.Max(2, spawnTimeMax - 1);
        }
        Debug.Log("WAVE " + waveNo + " STARTED");
    }
}
