using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    GameController gameController;
    [SerializeField] GameObject backButton;
    [SerializeField] GameObject titleButton;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("/GameController").GetComponent<GameController>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnBackButtonPressed(){
        // resume game
        gameController.UnpauseGame();
    }


    public void OnTitleButtonPressed(){
        backButton.SetActive(false);
        titleButton.SetActive(false);
        gameController.UnpauseGame();
        gameController.QuitGame();
    }
}
