using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsScript : MonoBehaviour
{
    AudioSource audioSource;
    public int currentWeapon = 0;
    //public int weaponCount;
    private int scrollStrength = 0;
    private bool isActive = true;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        transform.GetChild(0).GetComponent<GunScript>().SelectThisWeapon();
        transform.GetChild(1).GetComponent<GunScript>().SelectOtherWeapon();
        transform.GetChild(2).GetComponent<GunScript>().SelectOtherWeapon();
        transform.GetChild(3).GetComponent<GunScript>().SelectOtherWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive){
            // scroll wheel 
            if(Input.GetAxisRaw("Mouse ScrollWheel") > 0f && scrollStrength == 0){
                scrollStrength += 100;
                if(currentWeapon >= transform.childCount - 1)
                    SwitchWeapon(0);
                else
                    SwitchWeapon(currentWeapon+1);
                //Debug.Log("scroll up");
            }
            else if(Input.GetAxisRaw("Mouse ScrollWheel") < 0f && scrollStrength == 0){
                scrollStrength += 100;
                if(currentWeapon <= 0)
                    SwitchWeapon(transform.childCount - 1);
                else
                    SwitchWeapon(currentWeapon-1);
                //Debug.Log("scroll down");
            }
            else if(scrollStrength > 0){
                scrollStrength = Math.Max(0, scrollStrength - 10);
                //Debug.Log("no scroll");
            }

            // num key
            if(Input.GetKeyDown(KeyCode.Alpha1)){
                SwitchWeapon(0);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2){
                SwitchWeapon(1);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3){
                SwitchWeapon(2);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4){
                SwitchWeapon(3);
            }
        }
    }

    public void SwitchWeapon(int weapNo){
        // transform.GetChild(currentWeapon).gameObject.SetActive(false);
        // transform.GetChild(weapNo).gameObject.SetActive(true);
        if(weapNo != currentWeapon){
            transform.GetChild(currentWeapon).GetComponent<GunScript>().SelectOtherWeapon();
            transform.GetChild(currentWeapon).GetComponent<WeaponEffects>().RemoveEffects();
            transform.GetChild(weapNo).GetComponent<GunScript>().SelectThisWeapon();
            transform.GetChild(weapNo).GetComponent<WeaponEffects>().ApplyEffects();
            currentWeapon = weapNo;
        }
        audioSource.Play();
    }


    public void EnableWeapon(){
        transform.GetChild(currentWeapon).GetComponent<GunScript>().currentWeapon = true;
        isActive = true;
    }


    public void DisableWeapon(){
        transform.GetChild(currentWeapon).GetComponent<GunScript>().currentWeapon = false;
        isActive = false;
    }


    public void HideWeapons(){
        transform.GetChild(currentWeapon).GetComponent<GunScript>().SelectOtherWeapon();
        transform.GetChild(currentWeapon).GetComponent<WeaponEffects>().RemoveEffects();
        isActive = false;
    }
}
