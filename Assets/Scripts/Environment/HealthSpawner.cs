using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpawner : MonoBehaviour
{
    [SerializeField] GameObject healthPickup;
    [SerializeField] float spawnTime = 10f;
    private float spawnTimer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnTimer < spawnTime)
        {
            spawnTimer += Time.deltaTime;
        }
        else
        {
            SpawnHealth();
            spawnTimer = 0;
        }
    }

    private void SpawnHealth()
    {
        if(transform.childCount < 2)
        {
            Instantiate(healthPickup, transform.GetChild(0).transform.position, Quaternion.identity, transform);
            //Debug.Log("health spawned");
        }
        //else
        //{
        //    Debug.Log("health already exists");
        //}
    }
}
