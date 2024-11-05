using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WillOWisp : MonoBehaviour
{
    float timer = 5;
    [SerializeField] int effectType;
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
            Destroy(gameObject);
        }
    }
}
