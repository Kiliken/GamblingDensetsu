using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SlotsUI : MonoBehaviour
{
    Animator slot1;
    Animator slot2;
    Animator slot3;
    Animator slotsHandle;
    Animator[] slotArray;
    public TextMeshProUGUI effectText;
    GameObject effectBG;
    public TextMeshProUGUI rerollText;
    public Image effectTimerBar;
    [SerializeField] Sprite buffSprite;
    [SerializeField] Sprite debuffSprite;
    [SerializeField] AudioClip spinAudio;
    [SerializeField] AudioClip buffAudio;
    [SerializeField] AudioClip debuffAudio;
    AudioSource audioSource;
    private bool slotHidden = false;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        slotsHandle = transform.GetChild(0).gameObject.GetComponent<Animator>();
        slot1 = transform.GetChild(1).gameObject.GetComponent<Animator>();
        slot2 = transform.GetChild(2).gameObject.GetComponent<Animator>();
        slot3 = transform.GetChild(3).gameObject.GetComponent<Animator>();
        slotArray = new Animator[]{slot1, slot2, slot3};
        effectBG = transform.GetChild(4).gameObject;
        effectText = transform.GetChild(5).gameObject.GetComponent<TextMeshProUGUI>();
        rerollText = transform.GetChild(6).gameObject.GetComponent<TextMeshProUGUI>();

        HideSlots();
        effectText.gameObject.SetActive(false);
        effectBG.SetActive(false);
        rerollText.gameObject.SetActive(false);
        effectTimerBar.transform.parent.gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpinSlot(int s1, int s2, int s3){
        slotsHandle.gameObject.SetActive(true);
        slotsHandle.SetTrigger("slotSpin");
        int[] b = {s1, s2, s3};
        for(int i = 0; i < 3; i++){
            slotArray[i].gameObject.SetActive(true);
            if(b[i] == 1){
                // buff animation
                slotArray[i].SetTrigger("spinBuff");
            }
            else{
                // debuff animation
                slotArray[i].SetTrigger("spinDebuff");
            }
        }
        slotHidden = false;

        audioSource.clip = spinAudio;
        audioSource.volume = 0.5f;
        audioSource.Play();
    }

    // public void ChangeSlotIcon(int slotNo, int isBuff){
    //     slotArray[slotNo].gameObject.SetActive(true);
    //     if(isBuff == 1)
    //         slotArray[slotNo].sprite = buffSprite;
    //     else
    //          slotArray[slotNo].sprite = debuffSprite;
    //     slotHidden = false;
    // }


    public void HideSlots(){
        slotsHandle.gameObject.SetActive(false);
        if(!slotHidden){
            for(int i = 0; i < 3; i++){
                slotArray[i].gameObject.SetActive(false);
            }
            slotHidden = true;
        }
    }


    public void SetEffectText(String eText, bool isBuff){
        effectBG.SetActive(true);
        effectText.gameObject.SetActive(true);
        effectText.text = eText;
        if(isBuff)
            effectText.color = Color.green;
        else
            effectText.color = Color.red;
    }


    public void HideEffectText(){
        effectText.gameObject.SetActive(false);
        effectBG.SetActive(false);
    }


    public void SetRerollText(bool show){
        if(show)
            rerollText.gameObject.SetActive(true);
        else
            rerollText.gameObject.SetActive(false);
    }

    public void SetEffectTimeBar(bool show){
        if(show)
            effectTimerBar.transform.parent.gameObject.SetActive(true);
        else
            effectTimerBar.transform.parent.gameObject.SetActive(false);
    }

    public void PlaySlotsSFX(bool isBuff){
        if(isBuff){
            audioSource.clip = buffAudio;
            audioSource.volume = 0.5f;
        }
        else{
            audioSource.clip = debuffAudio;
            audioSource.volume = 0.2f;
        }
        audioSource.Play();
    }
}
