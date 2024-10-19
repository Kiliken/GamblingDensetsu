using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class GunScript : MonoBehaviour
{
    public Camera cam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI gunNameText;
    public GameObject ReloadText;
    Animator animator;
    public string gunName = "Gun";
    public float damage = 10f;
    public float fireRate = 0.1f;
    public float range = 100f;
    public int maxAmmo = 30;
    private int currentAmmo;
    public float reloadTime = 2f;
    private bool isReloading = false;
    private float nextFire = 0f;
    public bool singleShot = false;
    public bool isShotGun = false;
    private float defaultZoom = 60f;
    public float ADSZoom = 40f;
    public float ADSSpeed = 120f;

    void Awake(){
        currentAmmo = maxAmmo;
        this.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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

        // ADS
        if(Input.GetButton("Fire2")){
            cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, ADSZoom, ADSSpeed * Time.deltaTime);
            animator.SetBool("aiming", true);
        }
        else if(cam.fieldOfView < defaultZoom){
            cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, defaultZoom, ADSSpeed * Time.deltaTime);
            animator.SetBool("aiming", false);
        }
        // Shoot
        if(currentAmmo > 0){
            if(singleShot && Input.GetButtonDown("Fire1") && Time.time >= nextFire){
                nextFire = Time.time + fireRate;
                if(isShotGun)
                    ShotgunShoot();
                else
                    Shoot();
            }
            else if(!singleShot && Input.GetButton("Fire1") && Time.time >= nextFire){
                nextFire = Time.time + fireRate;
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
            //Debug.Log(cam.transform.forward);

            GameObject ie = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(ie, 2f);

            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if(enemy != null){
                enemy.TakeDamage(damage);
                ie.transform.parent = enemy.transform;
            }

            //cam.transform.Rotate(5f, 0, 0);
        }
    }


    public void ShotgunShoot(){
        muzzleFlash.Play();

        currentAmmo = Math.Max(0, currentAmmo - 1);
        UpdateAmmoText();

        // Vector3[] shotgunDir = {cam.transform.forward + cam.transform.right * 0.2f, cam.transform.forward + cam.transform.right * -0.2f, 
        //     cam.transform.forward + cam.transform.up * 0.2f, cam.transform.forward + cam.transform.up * -0.2f};
        Vector3[] shotgunDir = {cam.transform.forward + cam.transform.up * Random.Range(0.01f, 0.2f), cam.transform.forward + cam.transform.right * Random.Range(0.01f, 0.2f) + cam.transform.up * Random.Range(0.01f, 0.2f), 
            cam.transform.forward + cam.transform.right * Random.Range(0.01f, 0.2f) + cam.transform.up * Random.Range(-0.2f, -0.01f), cam.transform.forward + cam.transform.up * Random.Range(-0.2f, -0.01f),
            cam.transform.forward + cam.transform.right * Random.Range(-0.2f, -0.01f) + cam.transform.up * Random.Range(-0.2f, -0.01f), cam.transform.forward + cam.transform.right * Random.Range(-0.2f, -0.01f) + cam.transform.up * Random.Range(0.01f, 0.2f)};
        for(int i = 0; i < 6; i++){
            RaycastHit hit;
            if(Physics.Raycast(cam.transform.position, shotgunDir[i].normalized, out hit, range)){
                //Debug.Log(cam.transform.forward + shotgunDir[i]);

                GameObject ie = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(ie, 2f);

                Enemy enemy = hit.transform.GetComponent<Enemy>();
                if(enemy != null){
                    enemy.TakeDamage(damage);
                    ie.transform.parent = enemy.transform;
                }
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
