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
    EnemyManager enemyManager;
    EffectController effectController;
    public float effectTime = 30f;
    private float effectTimer = 30f;
    public bool effectActive = false;
    public int activeEffectNo = 0;  // 0 = none, 1,2,3... - effects
    // 1:player speed up/down, 2:enemy speed down/up, 3:player damage up/down, 4: enemy damage down/up, 5: explosion, 6: enemy size
    private bool isBuff = true;
    private float effectMultiplier = 1f;
    private int maxEffectNo = 6;
    // movement speed
    public float moveSpeedModifier = 3f;
    public bool playerSlowBullets = false;
    public bool enemySlowBullets = false;
    // damage
    public float damageModifier = 2f;
    public float sizeModifier = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("/Player").GetComponent<Player>();
        playerMovement = GameObject.Find("/Player").GetComponent<PlayerMovement>();
        gun = GetComponent<GunScript>();
        slotsUI = GameObject.Find("/Canvas/Slots").GetComponent<SlotsUI>();
        enemyManager = GameObject.Find("/Enemies").GetComponent<EnemyManager>();
        effectController = GameObject.Find("/CameraHolder/Main Camera/Weapons").GetComponent<EffectController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(effectActive){
            if(effectTimer > 0){
                effectTimer -= Time.deltaTime;
                if(gun.currentWeapon){
                    slotsUI.effectTimerBar.fillAmount = effectTimer / effectTime;
                    if(effectTimer <= 25)
                        slotsUI.HideSlots();
                }
            }
            else{
                if(gun.currentWeapon)
                    RemoveEffects();
                activeEffectNo = 0;
                effectTimer = effectTime;
                effectActive = false;
                Debug.Log(gun.gunName + " Effect time out. Slots usable.");
            }
        }
        if(gun.currentWeapon){
            if(Input.GetButtonDown("PullSlots")){
                if(effectTimer == effectTime){
                    // pull slots
                    StartCoroutine(PullSlots());
                }
                else if(effectActive && isBuff && effectMultiplier == 1){
                    StartCoroutine(Reroll());
                }
                
            }
        }
        
    }

    private IEnumerator PullSlots(){
        int first = Random.Range(0, 2);
        // change first slot sprite
        //slotsUI.ChangeSlotIcon(0, first);
        int second = Random.Range(0, 2);
        // change second slot sprite
        //slotsUI.ChangeSlotIcon(1, second);
        int third = Random.Range(0, 2);
        // change third slot sprite
        //slotsUI.ChangeSlotIcon(2, third);
        slotsUI.SpinSlot(first, second, third);


        activeEffectNo = Random.Range(1, maxEffectNo + 1);
        activeEffectNo = 6; // debug
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

        effectActive = true;
        yield return new WaitForSeconds(1);
        
        if(gun.currentWeapon){
            ApplyEffects();
            slotsUI.PlaySlotsSFX(isBuff);
        }     
    }


    // reroll for greater buff/debuff
    private IEnumerator Reroll(){
        effectMultiplier = 1.5f;
        // greater buff
        if(Random.Range(0, 2) == 0){
            // slotsUI.ChangeSlotIcon(0, 1);
            // slotsUI.ChangeSlotIcon(1, 1);
            // slotsUI.ChangeSlotIcon(2, 1);
            slotsUI.SpinSlot(1, 1, 1);
            Debug.Log("Rerolled greater buff");
        }
        // greater debuff
        else{
            // slotsUI.ChangeSlotIcon(0, 0);
            // slotsUI.ChangeSlotIcon(1, 0);
            // slotsUI.ChangeSlotIcon(2, 0);
            slotsUI.SpinSlot(0, 0, 0);
            isBuff = false;
            Debug.Log("Rerolled greater debuff");
        }
        slotsUI.SetRerollText(false);
        effectTimer = 30.99f;

        effectActive = true;
        yield return new WaitForSeconds(1);
        
        if(gun.currentWeapon){
            ApplyEffects();
            slotsUI.PlaySlotsSFX(isBuff);
        }
    }

    public void ApplyEffects(){
        if(activeEffectNo > 0){
            String plus = "+";
            String minus = "-";
            if(effectMultiplier == 1.5f){
                plus = "++";
                minus = "--";
            }
            else if(isBuff)
                slotsUI.SetRerollText(true);
                
            switch(activeEffectNo){
                case 1: // player speed up/down
                    if(isBuff){
                        playerMovement.speedModifier = moveSpeedModifier * effectMultiplier;
                        slotsUI.SetEffectText("プレイヤー速度 " + plus, true);
                    }
                    else{
                        playerMovement.speedModifier = -moveSpeedModifier * effectMultiplier;
                        slotsUI.SetEffectText("プレイヤー速度 " + minus, false);
                    }
                    break;
                case 2: // enemy speed down/up
                    if(isBuff){
                        enemyManager.ApplyEnemySpeed((-moveSpeedModifier + 1.5f) * effectMultiplier);
                        slotsUI.SetEffectText("敵速度 " + minus, true);
                    }
                    else{
                        enemyManager.ApplyEnemySpeed((moveSpeedModifier - 1.5f) * effectMultiplier);
                        slotsUI.SetEffectText("敵速度 " + plus, false);
                    }
                    break;
                case 3: // player damage up/down
                    if(isBuff){
                        gun.damageModifier = damageModifier * effectMultiplier;
                        slotsUI.SetEffectText("プレイヤーダメージ " + plus, true);
                    }
                    else{
                        gun.damageModifier = -damageModifier * effectMultiplier;
                        slotsUI.SetEffectText("プレイヤーダメージ " + minus, false);
                    }
                    break;
                case 4: // enemy damage down/up
                    if(isBuff){
                        playerScript.damageReceivedModifier = -damageModifier * effectMultiplier;
                        slotsUI.SetEffectText("敵ダメージ " + minus, true);
                    }
                    else{
                        playerScript.damageReceivedModifier = damageModifier * effectMultiplier;
                        slotsUI.SetEffectText("敵ダメージ " + plus, false);
                    }
                    break;
                case 5: // will o wisp
                    switch (true)
                    {
                        case true when (isBuff && effectMultiplier == 1.5f):
                            effectController.effNum = 0;
                            slotsUI.SetEffectText("爆発弾 " + plus, true);
                            break;
                        case true when (isBuff):
                            effectController.effNum = 1;
                            slotsUI.SetEffectText("爆発弾 " + plus, true);
                            break;
                        case true when (effectMultiplier == 1.5f):
                            effectController.effNum = 3;
                            slotsUI.SetEffectText("爆発弾 " + minus, false);
                            break;
                        default:
                            effectController.effNum = 2;
                            slotsUI.SetEffectText("爆発弾 " + minus, false);
                            break;
                    
                    }
                    break;
                    case 6: // enemy size
                    if(isBuff){
                        enemyManager.SetEnemySize(sizeModifier + effectMultiplier);
                        slotsUI.SetEffectText("敵サイズ " + plus, true);
                    }
                    else{
                        enemyManager.SetEnemySize(-(sizeModifier + effectMultiplier));
                        slotsUI.SetEffectText("敵サイズ " + minus, false);
                    }
                    break;
                    
            }
            slotsUI.SetEffectTimeBar(true);
        }
        
    }

    public void RemoveEffects(){
        if(activeEffectNo > 0){
            switch(activeEffectNo){
                case 1: // player speed up/down
                    playerMovement.speedModifier = 0f;
                    break;
                case 2: // enemy speed down/up
                    enemyManager.ApplyEnemySpeed(0f);
                    Debug.Log("Enemy speed effect removed");
                    break;
                case 3: // player damage up/down
                    gun.damageModifier = 0f;
                    break;
                case 4: // enemy damage down/up
                    playerScript.damageReceivedModifier = 0f;
                    Debug.Log("Enemy damage effect removed");
                    break;
                case 5:
                    effectController.effNum = -1;
                    Debug.Log("Will o wisp effect removed");
                    break;
                case 6: // enemy size
                    enemyManager.SetEnemySize(0);
                    Debug.Log("Enemy size effect removed");
                    break;
            }
            slotsUI.HideSlots();
            slotsUI.HideEffectText();
            slotsUI.SetRerollText(false);
            slotsUI.SetEffectTimeBar(false);
        }
        
    }
}
