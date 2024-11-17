using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    EnemySpawner enemySpawner;
    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = GameObject.Find("/EnemySpawns").GetComponent<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyEnemySpeed(float speed){
        for(int i = 0; i < transform.childCount; i++){
            transform.GetChild(i).GetComponent<Enemy>().speedModifier = speed;
        }
    }

    public void SetEnemyActive(bool active){
        enemySpawner.spawnStatus = active;
        for(int i = 0; i < transform.childCount; i++){
            transform.GetChild(i).GetComponent<Enemy>().SetEnemyActive(active);
        }
    }
}
