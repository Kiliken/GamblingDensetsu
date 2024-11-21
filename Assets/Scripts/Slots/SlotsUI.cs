using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SlotsUI : MonoBehaviour
{
    Image slot1;
    Image slot2;
    Image slot3;
    Image[] slotArray;
    public TextMeshProUGUI effectText;
    public TextMeshProUGUI rerollText;
    public Image effectTimerBar;
    [SerializeField] Sprite buffSprite;
    [SerializeField] Sprite debuffSprite;
    [SerializeField] AudioClip buffAudio;
    [SerializeField] AudioClip debuffAudio;
    AudioSource audioSource;
    private bool slotHidden = false;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        slot1 = transform.GetChild(0).gameObject.GetComponent<Image>();
        slot2 = transform.GetChild(1).gameObject.GetComponent<Image>();
        slot3 = transform.GetChild(2).gameObject.GetComponent<Image>();
        slotArray = new Image[]{slot1, slot2, slot3};
        effectText = transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();
        rerollText = transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>();

        HideSlots();
        effectText.gameObject.SetActive(false);
        rerollText.gameObject.SetActive(false);
        effectTimerBar.transform.parent.gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    public void ChangeSlotIcon(int slotNo, int isBuff){
        slotArray[slotNo].gameObject.SetActive(true);
        if(isBuff == 1)
            slotArray[slotNo].sprite = buffSprite;
        else
             slotArray[slotNo].sprite = debuffSprite;
        slotHidden = false;
    }


    public void HideSlots(){
        if(!slotHidden){
            for(int i = 0; i < 3; i++){
                slotArray[i].gameObject.SetActive(false);
            }
            slotHidden = true;
        }
    }


    public void SetEffectText(String eText, bool isBuff){
        effectText.gameObject.SetActive(true);
        effectText.text = eText;
        if(isBuff)
            effectText.color = Color.green;
        else
            effectText.color = Color.red;
    }


    public void HideEffectText(){
        effectText.gameObject.SetActive(false);
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
            audioSource.volume = 0.4f;
        }
        else{
            audioSource.clip = debuffAudio;
            audioSource.volume = 0.2f;
        }
        audioSource.Play();
    }
}
