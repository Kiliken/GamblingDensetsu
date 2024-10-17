using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEditor;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public Camera cam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI gunNameText;
    public GameObject ReloadText;
    public string gunName = "Gun";
    public float damage = 10f;
    public float fireRate = 15f;
    public float range = 100f;
    public int maxAmmo = 30;
    public int currentAmmo;
    public float reloadTime = 2f;
    private bool isReloading = false;
    private float nextFire = 0f;
    public bool singleShot = false;
    public bool isShotGun = false;

    void Awake(){
        currentAmmo = maxAmmo;
        this.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void OnEnable(){
        // cancel reload
        isReloading = false;
        gunNameText.text = gunName;
        UpdateAmmoText();
    }

    // Update is called once per frame
    void Update()
    {
        if(isReloading)
            return;

        // Shoot
        if(currentAmmo > 0){
            if(singleShot && Input.GetButtonDown("Fire1") && Time.time >= nextFire){
                nextFire = Time.time + 1f/fireRate;
                if(isShotGun)
                    ShotgunShoot();
                else
                    Shoot();
            }
            else if(!singleShot && Input.GetButton("Fire1") && Time.time >= nextFire){
                nextFire = Time.time + 1f/fireRate;
                Shoot();
            }
        }
        // Reload
        if(Input.GetButtonDown("Reload") && currentAmmo != maxAmmo){
            StartCoroutine(Reload());
        }

        
    }


    public void Shoot(){
        muzzleFlash.Play();

        currentAmmo = Math.Max(0, currentAmmo - 1);
        UpdateAmmoText();

        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range)){
            Debug.Log(hit.transform.name);
            Debug.Log(cam.transform.forward);

            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if(enemy != null){
                enemy.TakeDamage(damage);
            }

            GameObject ie = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(ie, 2f);
        }
    }

    public void ShotgunShoot(){
        muzzleFlash.Play();

        currentAmmo = Math.Max(0, currentAmmo - 1);
        UpdateAmmoText();

        Vector3[] shotgunDir = {cam.transform.forward + cam.transform.right * 0.2f, cam.transform.forward + cam.transform.right * -0.2f, 
            cam.transform.forward + cam.transform.up * 0.2f, cam.transform.forward + cam.transform.up * -0.2f};
        for(int i = 0; i < 4; i++){
            RaycastHit hit;
            if(Physics.Raycast(cam.transform.position, shotgunDir[i].normalized, out hit, range)){
                Debug.Log(cam.transform.forward + shotgunDir[i]);

                Enemy enemy = hit.transform.GetComponent<Enemy>();
                if(enemy != null){
                    enemy.TakeDamage(damage);
                }

                GameObject ie = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(ie, 2f);
            }
        }
    }


    IEnumerator Reload(){
        Debug.Log("Reloading...");
        isReloading = true;
        ReloadText.SetActive(true);
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        UpdateAmmoText();
        isReloading = false;
        ReloadText.SetActive(false);
        Debug.Log("Reloaded");
    }

    public void UpdateAmmoText(){
        ammoText.text = currentAmmo + "/" + maxAmmo;
    }
}
