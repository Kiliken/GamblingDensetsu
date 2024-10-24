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

    void Start()
    {
        timer = 0f;
    }

    private void Update()
    {
        if (inRange)
        {
            timer += 1 * Time.deltaTime;
            if(timer > 1f)
            {
                Debug.Log("GotHitten");
                timer = 0f;
            }
        } else
        {
            timer = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("in");
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("out");
            inRange = false;
        }
    }

}
