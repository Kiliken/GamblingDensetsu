using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy: MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float attackRange;
    NavMeshAgent enemyAI;
    Transform playerPos;


    // Start is called before the first frame update
    void Start()
    {
        enemyAI = this.GetComponent<NavMeshAgent>();
        if (!(playerPos = GameObject.Find("Player").GetComponent<Transform>()))
            Debug.LogError("NO OBJECT PLAYER FOUND");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        enemyAI.SetDestination(GetRadius(transform.position,playerPos.position,attackRange));
    }

    Vector3 GetRadius(Vector3 enemyPos, Vector3 playerPos, float radius)
    {
        Vector3 direction = (enemyPos - playerPos).normalized;
        return playerPos + direction * radius;
    }
}
