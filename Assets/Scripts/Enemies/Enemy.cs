using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    protected NavMeshAgent enemyAI;
    protected Transform playerPos;
    protected Player player;
    protected GameController gameController;
    protected AudioSource audioSource;

    public float HP = 50f;
    
    public float moveSpeed = 3.5f;
    public float speedModifier = 0f;
    public float speedIncrease = 0.5f;
   
    public float damage = 10f;
    public float damageIncrease = 2f;
    [SerializeField] protected float attackRange = 1f;
    protected float timer;
    
    [SerializeField] GameObject healthPickup;
    public int itemDropChance = 5;
    public int score = 100;
    public bool enemyActive = true;
    public Transform dmgNoPos;
    


    protected void Start()
    {
        enemyAI = this.GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        if (!(playerPos = GameObject.Find("/Player").GetComponent<Transform>()))
            Debug.LogError("NO OBJECT PLAYER FOUND");
        if (!(player = GameObject.Find("/Player").GetComponent<Player>()))
            Debug.LogError("NO OBJECT PLAYER FOUND");
        gameController = GameObject.Find("/GameController").GetComponent<GameController>();
    }


    protected virtual void Attack(float dmg, float accuracy){

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
        gameController.currentScore += score;
        Destroy(gameObject);
    }


    protected Vector3 GetRadius(Vector3 enemyPos, Vector3 playerPos, float radius)
    {
        Vector3 direction = (enemyPos - playerPos).normalized;
        return playerPos + direction * radius;
    }


    public GameObject GetEnemyObj()
    {
        return this.gameObject;
    }

    public void SetEnemyActive(bool active){
        enemyActive = active;
        enemyAI.enabled = active;
    }

    public virtual void StrengthenEnemy(int level){
        damage += damageIncrease * level;
        moveSpeed += speedIncrease * level;
    }
}
