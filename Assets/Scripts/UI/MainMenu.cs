using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject startButton;
    ScreenFadeController screenFade;

    // Start is called before the first frame update
    void Start()
    {
        screenFade = GameObject.Find("/Canvas/ScreenFade").GetComponent<ScreenFadeController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartButtonPress(){
        startButton.SetActive(false);
        screenFade.FadeIn(false);
    }
}
