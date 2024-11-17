using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : Enemy
{
    [Header("Fire")]
    [SerializeField] float fireRate = 6f;
    [SerializeField] float reactionTime = 18f;
    [SerializeField] ParticleSystem muzzleFlash;
    LayerMask mask = -1;


    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        timer = reactionTime;
    }


    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, (playerPos.position - transform.position), Color.green);
        if ((Physics.Raycast(transform.position, (playerPos.position - transform.position), out RaycastHit ray, attackRange, mask, QueryTriggerInteraction.Ignore)) && ray.collider.CompareTag("Player"))
        {
            enemyAI.ResetPath();
            //Debug.Log("See ya");
            //shoot
            transform.LookAt(playerPos);
            timer += -0.25f;
            if (timer <= 0)
            {
                Attack(damage, 80);
                //Debug.Log("BANG!");
                timer = fireRate;
            }
        }
        else
        {
            timer = reactionTime;
            enemyAI.SetDestination(GetRadius(transform.position, playerPos.position, 1f));
            enemyAI.speed = Mathf.Max(1, moveSpeed + speedModifier);
        }
    }


    protected override void Attack(float damage, float accuracy) {
        muzzleFlash.Play();
        if (Random.Range(1, 100) <= accuracy)
            player.TakeDamage(damage);
    }
}
