using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicatorHolder : MonoBehaviour
{
    [SerializeField] GameObject damageIndicator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateIndicator(Transform playerLocation, Transform playerCam, Transform enemyLocation){
        if(transform.childCount < 15){
            GameObject ind = Instantiate(damageIndicator,transform.position, transform.rotation, transform);
            ind.GetComponent<DamageIndicator>().player = playerLocation;
            ind.GetComponent<DamageIndicator>().playerCam = playerCam;
            ind.GetComponent<DamageIndicator>().enemy = enemyLocation;
            ind.SetActive(true);
        }
    }
}
