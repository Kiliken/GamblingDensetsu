using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponEffects : MonoBehaviour
{
    Player playerScript;
    PlayerMovement playerMovement;
    GunScript gun;
    SlotsUI slotsUI;
    public float effectTime = 20f;
    private float effectTimer = 0f;
    public bool effectActive = false;
    public int activeEffectNo = 0;  // 0 = none, 1,2,3... - effects
    // 1:player speed up/down, 2:enemy speed down/up, 3:player damage up/down, 4: enemy damage down/up
    private bool isBuff = true;
    private float effectMultiplier = 1f;
    private int maxEffectNo = 4;
    // movement speed
    public float moveSpeedModifier = 3f;
    public bool playerSlowBullets = false;
    public bool enemySlowBullets = false;
    // damage
    public float damageModifier = 2f;


    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Player>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        gun = GetComponent<GunScript>();
        slotsUI = GameObject.Find("Slots").GetComponent<SlotsUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(effectActive){
            if(effectTimer < effectTime){
                effectTimer += Time.deltaTime;

                if(effectTimer >= 5 && gun.currentWeapon){
                    slotsUI.HideSlots();
                }
            }
            else{
                if(gun.currentWeapon)
                    RemoveEffects();
                activeEffectNo = 0;
                effectTimer = 0;
                effectActive = false;
                Debug.Log(gun.gunName + " Effect time out. Slots usable.");
            }
        }
        if(gun.currentWeapon){
            if(Input.GetButtonDown("PullSlots") && effectTimer == 0){
                // pull slots
                PullSlots();
            }
        }
        
    }

    private void PullSlots(){
        int first = Random.Range(0, 2);
        // change first slot sprite
        slotsUI.ChangeSlotIcon(0, first);
        int second = Random.Range(0, 2);
        // change second slot sprite
        slotsUI.ChangeSlotIcon(1, second);
        int third = Random.Range(0, 2);
        // change third slot sprite
        slotsUI.ChangeSlotIcon(2, third);


        activeEffectNo = Random.Range(1, maxEffectNo + 1);
        int p = first + second + third;
        Debug.Log(p);
        if(p > 1)
            isBuff = true;
        else
            isBuff = false;
        
        if(p == 3 || p == 0)
            effectMultiplier = 1.5f;
        else
            effectMultiplier = 1f;

        ApplyEffects();
    }

    public void ApplyEffects(){
        if(activeEffectNo > 0){
            String plus = "+";
            String minus = "-";
            if(effectMultiplier == 1.5f){
                plus = "++";
                minus = "--";
            }
                
            switch(activeEffectNo){
                case 1: // player speed up/down
                    if(isBuff){
                        playerMovement.speedModifier = moveSpeedModifier * effectMultiplier;
                        slotsUI.SetEffectText("Player Speed " + plus, true);
                    }
                    else{
                        playerMovement.speedModifier = -moveSpeedModifier * effectMultiplier;
                        slotsUI.SetEffectText("Player Speed " + minus, false);
                    }
                    break;
                case 2: // enemy speed down/up
                    if(isBuff){
                        slotsUI.SetEffectText("Enemy Speed " + minus, true);
                    }
                    else{
                        slotsUI.SetEffectText("Enemy Speed " + plus, false);
                    }
                    break;
                case 3: // player damage up/down
                    if(isBuff){
                        gun.damageModifier = damageModifier * effectMultiplier;
                        slotsUI.SetEffectText("Player Damage " + plus, true);
                    }
                    else{
                        gun.damageModifier = -damageModifier * effectMultiplier;
                        slotsUI.SetEffectText("Player Damage " + minus, false);
                    }
                    break;
                case 4: // enemy damage down/up
                    if(isBuff){
                        slotsUI.SetEffectText("Enemy Damage " + minus, true);
                    }
                    else{
                        slotsUI.SetEffectText("Enemy Damage " + plus, false);
                    }
                    break;
            }
            effectActive = true;
        }
        
    }

    public void RemoveEffects(){
        if(activeEffectNo > 0){
            switch(activeEffectNo){
                case 1: // player speed up/down
                    playerMovement.speedModifier = 0;
                    break;
                case 2: // enemy speed down/up
                    Debug.Log("Enemy speed effect removed");
                    break;
                case 3: // player damage up/down
                    gun.damageModifier = 0;
                    break;
                case 4: // enemy damage down/up
                    Debug.Log("Enemy damage effect removed");
                    break;
            }
            slotsUI.HideSlots();
            slotsUI.HideEffectText();
        }
        
    }
}
