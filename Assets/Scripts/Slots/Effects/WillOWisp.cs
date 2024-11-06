using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WillOWisp : MonoBehaviour
{
    float timer = 3;
    [SerializeField] int effectType;
    [SerializeField] GameObject explosionParticle;
    

    List<Enemy> enemiesInRange = new List<Enemy>();
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else {
            WillOEffect();
        }
    }

    void WillOEffect() {
        Instantiate(explosionParticle, this.transform.position, Quaternion.identity);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5f);
        switch (effectType) {
            case 0:
                
                foreach (Collider hitCollider in hitColliders)
                {
                    if (hitCollider.gameObject.GetComponent<Enemy>() != null) { 
                        Enemy enemy = hitCollider.gameObject.GetComponent<Enemy>();
                        enemy.TakeDamage(10);
                    }
                }
                break;
            case 1:
                foreach (Collider hitCollider in hitColliders)
                {
                    if (hitCollider.gameObject.GetComponent<Enemy>() != null)
                    {
                        Enemy enemy = hitCollider.gameObject.GetComponent<Enemy>();
                        enemy.TakeDamage(5);
                    }
                }
                break;
            case 2:
                foreach (Collider hitCollider in hitColliders)
                {
                    if (hitCollider.gameObject.GetComponent<Player>() != null)
                    {
                        Player player = GameObject.Find("Player").GetComponent<Player>();
                        player.TakeDamage(5);
                    }
                }
                break;
            case 3:
                foreach (Collider hitCollider in hitColliders)
                {
                    if (hitCollider.gameObject.GetComponent<Player>() != null)
                    {
                        Player player = GameObject.Find("Player").GetComponent<Player>();
                        player.TakeDamage(10);
                    }
                }
                break;
        }
        Destroy(gameObject);
    }

    
}
