using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Enemy
{
    public bool inRange = false;


    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        timer = 0f;
    }


    // Update is called once per frame
    void Update()
    {
        if(enemyActive){
            if (inRange)
            {
                timer += 1 * Time.deltaTime;
                if(timer > 1f)
                {
                    Attack(damage, 0);
                    timer = 0f;
                }
            } else
            {
                timer = 0f;
            }
        }
        
    }


    private void FixedUpdate()
    {
        if(enemyActive){
            enemyAI.SetDestination(GetRadius(transform.position,playerPos.position,attackRange));
            enemyAI.speed = Mathf.Max(1, moveSpeed + speedModifier);
        }
    }


    protected override void Attack(float dmg, float accuracy)
    {
        player.TakeDamage(damage);
    }
}
