using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject Logo;
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject creditsButton;
    [SerializeField] GameObject HTPButton;
    [SerializeField] GameObject creditsScreen;
    [SerializeField] GameObject HTPScreen;
    ScreenFadeController screenFade;

    // Start is called before the first frame update
    void Start()
    {
        screenFade = GameObject.Find("/Canvas/ScreenFade").GetComponent<ScreenFadeController>();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartButtonPress(){
        startButton.SetActive(false);
        creditsButton.SetActive(false);
        HTPButton.SetActive(false);
        screenFade.FadeIn(false);
    }

    public void OnCreditsButtonPress(){
        startButton.SetActive(false);
        creditsButton.SetActive(false);
        HTPButton.SetActive(false);
        Logo.SetActive(false);
        creditsScreen.SetActive(true);
    }

    public void OnHTPButtonPress(){
        startButton.SetActive(false);
        creditsButton.SetActive(false);
        HTPButton.SetActive(false);
        Logo.SetActive(false);
        HTPScreen.SetActive(true);
    }

    public void OnCreditsBackButtonPress(){
        creditsScreen.SetActive(false);
        startButton.SetActive(true);
        creditsButton.SetActive(true);
        HTPButton.SetActive(true);
        Logo.SetActive(true);
    }

    public void OnHTPBackButtonPress(){
        HTPScreen.SetActive(false);
        startButton.SetActive(true);
        creditsButton.SetActive(true);
        HTPButton.SetActive(true);
        Logo.SetActive(true);
    }
}
