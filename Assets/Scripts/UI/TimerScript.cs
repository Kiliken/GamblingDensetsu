using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    TextMeshProUGUI timerText;
    private int minutes = 0;
    private float seconds = 0f;
    private float milliseconds = 0f; 
    public float elaspedTime = 0f;
    [SerializeField] string timerName = "Time: ";
    [SerializeField] bool decreaseTimer = false;
    public float resetTime = 0f;    // in seconds
    public bool timerActive = true;


    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        if(decreaseTimer)
            elaspedTime = resetTime;
    }


    // Update is called once per frame
    void Update()
    {
        if(timerActive){
            if(resetTime > 0f && !decreaseTimer){
                if(elaspedTime >= resetTime)
                    elaspedTime = 0f;
            }
            else if(decreaseTimer && elaspedTime <= 0f){
                elaspedTime = resetTime;
            }

            if(decreaseTimer)
                elaspedTime -= Time.deltaTime;
            else
                elaspedTime += Time.deltaTime;

            minutes = Mathf.Max(0, Mathf.FloorToInt(elaspedTime / 60));
            seconds = Mathf.Max(0, Mathf.FloorToInt(elaspedTime % 60));
            milliseconds = Mathf.Max(0, Mathf.FloorToInt((elaspedTime % 1f) * 100));
            
            timerText.text = timerName + minutes.ToString("00") + ":" + ((int)seconds).ToString("00") + ":" + ((int)milliseconds).ToString("00");
        }
        
    }
}
