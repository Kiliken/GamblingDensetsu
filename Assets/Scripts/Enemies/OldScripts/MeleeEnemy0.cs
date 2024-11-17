// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.AI;

// public class MeleeEnemy: MonoBehaviour
// {
//     [Header("Movement")]
//     [SerializeField] float attackRange;
//     NavMeshAgent enemyAI;
//     Transform playerPos;
//     Enemy enemyScript;


//     // Start is called before the first frame update
//     void Start()
//     {
//         enemyScript = GetComponent<Enemy>();
//         enemyAI = this.GetComponent<NavMeshAgent>();
//         if (!(playerPos = GameObject.Find("Player").GetComponent<Transform>()))
//             Debug.LogError("NO OBJECT PLAYER FOUND");
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }

//     private void FixedUpdate()
//     {
//         enemyAI.SetDestination(GetRadius(transform.position,playerPos.position,attackRange));
//         enemyAI.speed = Mathf.Max(1, enemyScript.moveSpeed + enemyScript.speedModifier);
//     }

//     Vector3 GetRadius(Vector3 enemyPos, Vector3 playerPos, float radius)
//     {
//         Vector3 direction = (enemyPos - playerPos).normalized;
//         return playerPos + direction * radius;
//     }
// }
