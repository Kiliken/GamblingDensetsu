using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float HPRestored = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, 2.0f, 0.0f, Space.Self);
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            Player player = other.gameObject.GetComponent<Player>();
            if(player != null){
                player.RestoreHP(HPRestored);
                Debug.Log("health restored");
                Destroy(gameObject);
            }
        }
    }
}
