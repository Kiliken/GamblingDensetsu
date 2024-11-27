using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    PlayerMovement playerMovement;
    PlayerCam playerCam;
    WeaponsScript weapons;
    GameController gameController;
    public float MaxHP = 100f;
    public float HP;
    public float damageReceivedModifier = 0f;

    [Space(12)]
    [SerializeField] Image hpBar;
    public bool playerActive = true;

    AudioSource audioSource;
    [SerializeField] AudioClip playerHurtSFX;
    [SerializeField] AudioClip playerHealSFX;



    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
        playerCam = GameObject.Find("/CameraHolder/Main Camera").GetComponent<PlayerCam>();
        weapons = GameObject.Find("/CameraHolder/Main Camera/Weapons").GetComponent<WeaponsScript>();
        gameController = GameObject.Find("/GameController").GetComponent<GameController>();
        HP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TakeDamage(float dmg){
        HP -= Mathf.Max(0, dmg + damageReceivedModifier);
        hpBar.fillAmount = HP / MaxHP;

        if(!audioSource.isPlaying){
            audioSource.clip = playerHurtSFX;
            audioSource.Play();
        }

        if (HP <= 0f){
            Death();
        }
    }


    public void RestoreHP(float HPRestored){
        HP = Mathf.Min(MaxHP, HP + HPRestored);
        hpBar.fillAmount = HP / MaxHP;
        audioSource.clip = playerHealSFX;
        audioSource.Play();
    }


    private void Death(){
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SetPlayerActive(false);
        gameController.GameOver();

    }


    public void SetPlayerActive(bool active){
        playerMovement.playerActive = active;
        playerCam.playerActive = active;
        if(!active)
            weapons.HideWeapons();
    }
}
