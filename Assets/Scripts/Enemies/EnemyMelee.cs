using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    [Header("Melee")]
    [SerializeField]float radius;

    float timer;
    bool inRange;
    public float damage = 10f;
    public float damageModifier = 0f;   //damage + damageModifier
    Player player;

    void Start()
    {
        timer = 0f;
        if (!(player = GameObject.Find("Player").GetComponent<Player>()))
            Debug.LogError("NO OBJECT PLAYER FOUND");
    }

    private void Update()
    {
        if (inRange)
        {
            timer += 1 * Time.deltaTime;
            if(timer > 1f)
            {
                Attack(damage);
                timer = 0f;
            }
        } else
        {
            timer = 0f;
        }
    }

    private void Attack(float damage) {
        player.TakeDamage(damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("in");
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("out");
            inRange = false;
        }
    }

}
