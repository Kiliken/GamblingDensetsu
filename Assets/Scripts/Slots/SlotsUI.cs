using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotsUI : MonoBehaviour
{
    Image slot1;
    Image slot2;
    Image slot3;
    Image[] slotArray;
    public TextMeshProUGUI effectText;
    [SerializeField] Sprite buffSprite;
    [SerializeField] Sprite debuffSprite;


    // Start is called before the first frame update
    void Start()
    {
        slot1 = transform.GetChild(0).gameObject.GetComponent<Image>();
        slot2 = transform.GetChild(1).gameObject.GetComponent<Image>();
        slot3 = transform.GetChild(2).gameObject.GetComponent<Image>();
        slotArray = new Image[]{slot1, slot2, slot3};
        effectText = transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();

        HideSlots();
        HideEffectText();
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
    }


    public void HideSlots(){
        for(int i = 0; i < 3; i++){
            slotArray[i].gameObject.SetActive(false);
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
}
