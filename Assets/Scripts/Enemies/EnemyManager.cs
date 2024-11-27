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

    // 0: normal, 1: big, 2: small
    public void SetEnemySize(float size){
        switch(size){
            case 0:
                for(int i = 0; i < transform.childCount; i++){
                    transform.GetChild(i).GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
                }
                break;
            case 1.5f:
                for(int i = 0; i < transform.childCount; i++){
                    transform.GetChild(i).GetComponent<Transform>().localScale = new Vector3(1.5f, 1.5f, 1.5f);
                }
                break;
            case 2:
                for(int i = 0; i < transform.childCount; i++){
                    transform.GetChild(i).GetComponent<Transform>().localScale = new Vector3(2, 2, 2);
                }
                break;
            case -1.5f:
                for(int i = 0; i < transform.childCount; i++){
                    transform.GetChild(i).GetComponent<Transform>().localScale = new Vector3(0.7f, 0.7f, 0.7f);
                }
                break;
            case -2f:
                for(int i = 0; i < transform.childCount; i++){
                    transform.GetChild(i).GetComponent<Transform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);
                }
                break;
        }
    }
}
