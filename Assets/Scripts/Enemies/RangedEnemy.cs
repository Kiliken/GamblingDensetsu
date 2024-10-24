using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class RangedEnemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float attackRange;
    NavMeshAgent enemyAI;
    Transform playerPos;
    [SerializeField] float fireRate;
    [SerializeField] float reactionTime;

    Player player;
    LayerMask mask = -1;

    public float HP = 50f;
    float timer;
    public float damage = 10f;
    public float damageModifier = 0f;   //damage + damageModifier

    // Start is called before the first frame update
    void Start()
    {
        timer = reactionTime;
        enemyAI = this.GetComponent<NavMeshAgent>();

        if (!(playerPos = GameObject.Find("Player").GetComponent<Transform>()))
            Debug.LogError("NO OBJECT PLAYER FOUND");

        if (!(player = GameObject.Find("Player").GetComponent<Player>()))
            Debug.LogError("NO OBJECT PLAYER FOUND");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //Debug.DrawRay(transform.position, (playerPos.position - transform.position), Color.green);
        if ((Physics.Raycast(transform.position, (playerPos.position - transform.position), out RaycastHit ray, attackRange, mask, QueryTriggerInteraction.Ignore)) && ray.collider.CompareTag("Player"))
        {
            enemyAI.ResetPath();
            //Debug.Log("See ya");
            //shoot
            timer += -0.25f;
            if (timer <= 0)
            {
                Shoot(damage, 80);
                Debug.Log("BANG!");
                timer = fireRate;
            }
        }
        else
        {
            timer = reactionTime;
            enemyAI.SetDestination(GetRadius(transform.position, playerPos.position, 1f));
        }
    }

    Vector3 GetRadius(Vector3 enemyPos, Vector3 playerPos, float radius)
    {
        Vector3 direction = (enemyPos - playerPos).normalized;
        return playerPos + direction * radius;
    }


    private void Shoot(float damage, int accuracy) { 
        if(Random.Range(1,100) <= accuracy)
            player.TakeDamage(damage);
    }
    public void TakeDamage(float dmg){
        HP -= dmg;
        if(HP <= 0f){
            Death();
        }
    }

    public void Death(){
        Destroy(gameObject);
    }
}
