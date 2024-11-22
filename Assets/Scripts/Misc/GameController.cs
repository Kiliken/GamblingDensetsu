using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    EnemyManager enemyManager;
    ScreenFadeController screenFade;
    TextMeshProUGUI scoreText;
    TimerScript timer;
    JsonSave saveManager;
    GameOverScreenScript gameOverScript;
    AudioSource BGM;
    private int _currentScore = 0;
    public int currentScore {get{
        return _currentScore;
    }
    set{
        _currentScore = value;
        Debug.Log(_currentScore);
        // set UI score
        scoreText.text = "スコア: " + _currentScore;
    }}
    public int highScore = 0;
    public float currentTime = 0f;
    public float bestTime = 0f;


    // Start is called before the first frame update
    void Start()
    {
        saveManager = GetComponent<JsonSave>();
        enemyManager = GameObject.Find("/Enemies").GetComponent<EnemyManager>();
        screenFade = GameObject.Find("/Canvas/ScreenFade").GetComponent<ScreenFadeController>();
        scoreText = GameObject.Find("/Canvas/Score").GetComponent<TextMeshProUGUI>();
        timer = GameObject.Find("/Canvas/Timer").GetComponent<TimerScript>();
        gameOverScript = GameObject.Find("/Canvas/GameOverScreen").GetComponent<GameOverScreenScript>();
        BGM = GameObject.Find("/BGM").GetComponent<AudioSource>();

        if(saveManager.LoadGame()){
            highScore = saveManager.gameSave.highScore;
            bestTime = saveManager.gameSave.bestTime;
        }
    }


    // // Update is called once per frame
    // void Update()
    // {
        
    // }


    public void GameOver(){
        // get time and score
        timer.timerActive = false;
        currentTime = timer.elaspedTime;
        if(currentTime > bestTime)
            bestTime = currentTime;
        if(currentScore > highScore)
            highScore = currentScore;
        // save game
        saveManager.SaveGame(highScore, bestTime);
        // set game over screen
        gameOverScript.SetGameOverScreen(currentScore, highScore, currentTime, bestTime);

        enemyManager.SetEnemyActive(false);
        screenFade.FadeIn(true);
        BGM.Stop();
    }

}
