using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public Camera cam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public float damage = 10f;
    public float fireRate = 15f;
    public float range = 100f;
    private float nextFire = 0f;
    public bool singleShot = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(singleShot && Input.GetButtonDown("Fire1") && Time.time >= nextFire){
            nextFire = Time.time + 1f/fireRate;
            Shoot();
        }
        else if(!singleShot && Input.GetButton("Fire1") && Time.time >= nextFire){
            nextFire = Time.time + 1f/fireRate;
            Shoot();
        }
    }

    public void Shoot(){
        muzzleFlash.Play();

        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range)){
            Debug.Log(hit.transform.name);

            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if(enemy != null){
                enemy.TakeDamage(damage);
            }

            GameObject ie = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(ie, 2f);
        }
    }
}
