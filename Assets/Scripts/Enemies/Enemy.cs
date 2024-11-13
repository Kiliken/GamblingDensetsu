using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float HP = 50f;
    public float moveSpeed = 3.5f;
    public float speedModifier = 0f;
    [SerializeField] GameObject healthPickup;
    public int itemDropChance = 5;

    private void Start()
    {
        
    }

    public void TakeDamage(float dmg){
        HP -= dmg;
        if(HP <= 0f){
            Death();
        }
    }

    public void Death(){
        if(Random.Range(0, itemDropChance + 1) == 0){
            Instantiate(healthPickup, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    public GameObject GetEnemyObj()
    {
        return this.gameObject;
    }
}
