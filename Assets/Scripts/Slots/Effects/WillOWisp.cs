using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WillOWisp : MonoBehaviour
{
    float timer = 3;
    [SerializeField] int effectType;
    [SerializeField] GameObject explosionParticle;
    AudioSource audioSource;
    

    List<Enemy> enemiesInRange = new List<Enemy>();
    private bool isActive = false;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else {
            if(!isActive)
                WillOEffect();
            else if(isActive && !audioSource.isPlaying)
                Destroy(gameObject);
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
        audioSource.Play();
        isActive = true;
    }

    
}
