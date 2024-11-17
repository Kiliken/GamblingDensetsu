using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GunScript : MonoBehaviour
{
    public Camera cam;
    PlayerCam camScript;
    EffectController effectController;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI gunNameText;
    public TextMeshProUGUI ReloadText;
    DiceFaceScript diceFace;
    Animator animator;
    GameObject crosshairDefault;
    [SerializeField] GameObject crosshairCustom;
    public string gunName = "Gun";
    public float damage = 10f;
    public float damageModifier = 0f;
    public float critDamage = 20f;
    public int critChance = 6;
    public float fireRate = 0.1f;
    public float range = 100f;
    public int maxAmmoDefault = 30;
    public float ammoModSmall = 0.2f;
    public float ammoModLarge = 0.5f;
    private int maxAmmo;
    private int currentAmmo;
    public float reloadTime = 2f;
    private bool isReloading = false;
    private float nextFire = 0f;
    public float recoil = 1f;
    public bool singleShot = false;
    public bool isShotGun = false;
    private float defaultZoom = 90f;
    public float ADSZoom = 40f;
    public float ADSSpeed = 120f;
    public float aimSensDefault = 400f;
    public float aimSensADS = 200f;
    //[SerializeField] bool customCrosshair = false;
    public bool currentWeapon = false;
    public float effectCooldown = 0f;
    [SerializeField] GameObject damageParticle;


    void Awake(){
        animator = GetComponent<Animator>();
        maxAmmo = maxAmmoDefault;
        currentAmmo = maxAmmoDefault;
        //this.gameObject.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        effectController = GameObject.Find("/CameraHolder/Main Camera/Weapons").GetComponent<EffectController>();
        camScript = cam.GetComponent<PlayerCam>();
        currentAmmo = maxAmmo;
        diceFace = GameObject.Find("/Canvas/DiceFace").GetComponent<DiceFaceScript>();
        crosshairDefault = GameObject.Find("/Canvas/Crosshair");
        if(crosshairCustom == null){
            crosshairCustom = crosshairDefault;
        }
    }


    // void OnEnable(){
    //     // cancel reload
    //     isReloading = false;
    //     gunNameText.text = gunName;
    //     UpdateAmmoText();
    //     ReloadText.gameObject.SetActive(false);
    // }


    // Update is called once per frame
    void Update()
    {
        if(currentWeapon){
            // ADS
            if(Input.GetButton("Fire2") && !isReloading){
                cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, ADSZoom, ADSSpeed * Time.deltaTime);
                animator.SetBool("aiming", true);
                camScript.sensX = camScript.sensY = aimSensADS;

            }
            else if(cam.fieldOfView < defaultZoom){
                cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, defaultZoom, ADSSpeed * Time.deltaTime);
                animator.SetBool("aiming", false);
                camScript.sensX = camScript.sensY = aimSensDefault;
            }

            if(isReloading){
                return;
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

            if (effectCooldown > 0f)
                effectCooldown -= Time.deltaTime;
            else
                effectCooldown = 0f;
        }
    }


    public void Shoot(){
        muzzleFlash.Play();

        currentAmmo = Math.Max(0, currentAmmo - 1);
        UpdateAmmoText();

        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range, LayerMask.GetMask("Ground", "Enemy"))){
            Debug.Log(hit.transform.name);
            //Debug.Log(cam.transform.forward);

            GameObject ie = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(ie, 2f);

            if(hit.transform.tag == "CritSpot"){
                Enemy enemy = hit.transform.parent.GetComponent<Enemy>();
                if(enemy != null){
                    float dmg = Mathf.Max(1, critDamage + damageModifier);
                    enemy.TakeDamage(dmg);
                    DamagePopUp popUpDamage = Instantiate(damageParticle, enemy.transform.position, Quaternion.identity).GetComponent<DamagePopUp>();
                    popUpDamage.SetDamageText((int)dmg);
                    popUpDamage.SetTextColor(Color.red);
                    Debug.Log(" Crit Spot Critical Hit");

                    ie.transform.parent = enemy.transform;
                    if (effectCooldown == 0f) 
                        effectCooldown = effectController.ApplyEffect(enemy);
                    
                    
                }
            }
            else{
                Enemy enemy = hit.transform.GetComponent<Enemy>();
                if(enemy != null){
                    if(Random.Range(0, critChance + 1) == 0){
                        float dmg = Mathf.Max(1, critDamage + damageModifier);
                        enemy.TakeDamage(dmg);
                        DamagePopUp popUpDamage = Instantiate(damageParticle, enemy.transform.position, Quaternion.identity).GetComponent<DamagePopUp>();
                        popUpDamage.SetDamageText((int)dmg);
                        popUpDamage.SetTextColor(Color.yellow);
                        Debug.Log("Critical Hit");
                    }
                    else
                    {
                        float dmg = Mathf.Max(1, damage + damageModifier);
                        enemy.TakeDamage(dmg);
                        DamagePopUp popUpDamage = Instantiate(damageParticle, enemy.transform.position, Quaternion.identity).GetComponent<DamagePopUp>();
                        popUpDamage.SetDamageText((int)dmg);
                    }
                        

                    ie.transform.parent = enemy.transform;
                    if (effectCooldown == 0f)
                        effectCooldown = effectController.ApplyEffect(enemy);
                }
            }
        }

        camScript.recoil += recoil;
        animator.SetBool("shooting", true);
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
            if(Physics.Raycast(cam.transform.position, shotgunDir[i].normalized, out hit, range, LayerMask.GetMask("Ground", "Enemy"))){
                //Debug.Log(cam.transform.forward + shotgunDir[i]);

                GameObject ie = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(ie, 2f);

                if(hit.transform.tag == "CritSpot"){
                    Enemy enemy = hit.transform.parent.GetComponent<Enemy>();
                    if(enemy != null){
                        float dmg = Mathf.Max(1, critDamage + damageModifier);
                        enemy.TakeDamage(dmg);
                        DamagePopUp popUpDamage = Instantiate(damageParticle, enemy.transform.position, Quaternion.identity).GetComponent<DamagePopUp>();
                        popUpDamage.SetDamageText((int)dmg);
                        popUpDamage.SetTextColor(Color.red);
                        Debug.Log("Crit Spot Critical Hit");

                        ie.transform.parent = enemy.transform;
                        if (effectCooldown == 0f)
                            effectCooldown = effectController.ApplyEffect(enemy);
                    }
                }
                else{
                    Enemy enemy = hit.transform.GetComponent<Enemy>();
                    if(enemy != null){
                        if (Random.Range(0, critChance + 1) == 0)
                        {
                            float dmg = Mathf.Max(1, critDamage + damageModifier);
                            enemy.TakeDamage(dmg);
                            DamagePopUp popUpDamage = Instantiate(damageParticle, enemy.transform.position, Quaternion.identity).GetComponent<DamagePopUp>();
                            popUpDamage.SetDamageText((int)dmg);
                            popUpDamage.SetTextColor(Color.yellow);
                            Debug.Log("Critical Hit");
                        }
                        else {
                            float dmg = Mathf.Max(1, damage + damageModifier);
                            enemy.TakeDamage(dmg);
                            DamagePopUp popUpDamage = Instantiate(damageParticle, enemy.transform.position, Quaternion.identity).GetComponent<DamagePopUp>();
                            popUpDamage.SetDamageText((int)dmg);
                        }
                        

                        ie.transform.parent = enemy.transform;
                        if (effectCooldown == 0f)
                            effectCooldown = effectController.ApplyEffect(enemy);
                    }
                }
            }
        }
        camScript.recoil += recoil;
        animator.SetBool("shooting", true);
    }

    #region //old reload
    // IEnumerator Reload(){
    //     Debug.Log("Reloading...");
    //     isReloading = true;
    //     ReloadText.gameObject.SetActive(true);
    //     int t = Random.Range(1, 7);
    //     reloadTime = t;
    //     Debug.Log(reloadTime + "s reload time");
    //     yield return new WaitForSeconds(reloadTime);
    //     #region //ammo dice
    //     // int c = Random.Range(0, 6);
    //     // switch(c){
    //     //     case 0:
    //     //         maxAmmo = maxAmmoDefault - (int)(maxAmmoDefault * ammoModLarge);
    //     //         Debug.Log("Dice 1, least ammo");
    //     //         break;
    //     //     case 1:
    //     //         maxAmmo = maxAmmoDefault - (int)(maxAmmoDefault * ammoModSmall);
    //     //         Debug.Log("Dice 2, less ammo");
    //     //         break;
    //     //     case 2:
    //     //     case 3:
    //     //         maxAmmo = maxAmmoDefault;
    //     //         Debug.Log("Dice 3 or 4, normal ammo");
    //     //         break;
    //     //     case 4:
    //     //         maxAmmo = maxAmmoDefault + (int)(maxAmmoDefault * ammoModSmall);
    //     //         Debug.Log("Dice 5, more ammo");
    //     //         break;
    //     //     case 5:
    //     //         maxAmmo = maxAmmoDefault + (int)(maxAmmoDefault * ammoModLarge);
    //     //         Debug.Log("Dice 6, most ammo");
    //     //         break;
    //     // }
    //     #endregion
    //     currentAmmo = maxAmmo;
    //     UpdateAmmoText();
    //     isReloading = false;
    //     ReloadText.gameObject.SetActive(false);
    //     Debug.Log("Reloaded");
    // }
#endregion

    IEnumerator Reload(){
        Debug.Log("Reloading...");
        isReloading = true;
        reloadTime = Random.Range(1, 7);
        Debug.Log(reloadTime + "s reload time");

        // ammo bonus
        // 5,6s - 50% more ammo, 3,4s - 20% more ammo
        if(reloadTime > 4){
            maxAmmo = maxAmmoDefault + (int)(maxAmmoDefault * ammoModLarge);
        }
        else if(reloadTime > 2 && reloadTime < 5){
            maxAmmo = maxAmmoDefault + (int)(maxAmmoDefault * ammoModSmall);
        }
        else
            maxAmmo = maxAmmoDefault;
        
        diceFace.SetDiceFace((int)reloadTime);
        ReloadText.gameObject.SetActive(true);
        while(reloadTime > 0){
            reloadTime -= 1.0f;
            if(currentWeapon)
                ReloadText.text = " リロード中... " + (reloadTime + 1) + "s";
            yield return new WaitForSeconds(1);
        }
        currentAmmo = maxAmmo;
        if(currentWeapon){
            UpdateAmmoText();
            ReloadText.gameObject.SetActive(false);
            diceFace.HideDiceFace();
        }
        isReloading = false;
        Debug.Log(gunName + " Reloaded");
    }

    public void SelectThisWeapon(){
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        currentWeapon = true;

        gunNameText.text = gunName;
        UpdateAmmoText();
        if(isReloading){
            ReloadText.text = " リロード中... " + (reloadTime + 1) + "s";
            ReloadText.gameObject.SetActive(true);
        }
        else{
            ReloadText.gameObject.SetActive(false);
        }
    }

    public void SelectOtherWeapon(){
        currentWeapon = false;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        animator.SetBool("aiming", false);
        diceFace.HideDiceFace();
    }


    public void UpdateAmmoText(){
        ammoText.text = currentAmmo + "/" + maxAmmo;
    }


    public void ShootEvent(){
        animator.SetBool("shooting", false);
    }

    public void AimDownEvent(){
        crosshairDefault.SetActive(false);
        crosshairCustom.SetActive(true);
    }

    public void AimUpEvent(){
        crosshairDefault.SetActive(true);
        crosshairCustom.SetActive(false);
    }
}
