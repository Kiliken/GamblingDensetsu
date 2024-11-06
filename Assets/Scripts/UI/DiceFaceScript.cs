using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceFaceScript : MonoBehaviour
{
    [SerializeField] Sprite dice1;
    [SerializeField] Sprite dice2;
    [SerializeField] Sprite dice3;
    [SerializeField] Sprite dice4;
    [SerializeField] Sprite dice5;
    [SerializeField] Sprite dice6;
    Sprite[] diceFaceArray;
    private bool spriteActive = false;
    private float spriteActiveTime = 3f;
    private float spriteActiveTimer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        diceFaceArray = new Sprite[]{dice1, dice2, dice3, dice4, dice5, dice6};
    }


    // Update is called once per frame
    void Update()
    {
        if(spriteActive){
            if(spriteActiveTimer < spriteActiveTime){
                spriteActiveTimer += Time.deltaTime;
            }
            else{
                HideDiceFace();
            }
        }
    }


    public void SetDiceFace(int faceNo){
        GetComponent<Image>().sprite = diceFaceArray[faceNo - 1];
        GetComponent<Image>().enabled = true;
        spriteActiveTimer = 0f;
        spriteActive = true;
    }


    public void HideDiceFace(){
        GetComponent<Image>().enabled = false;
        spriteActiveTimer = 0f;
        spriteActive = false;
    }
}
