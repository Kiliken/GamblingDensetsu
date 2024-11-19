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


    // Start is called before the first frame update
    void Start()
    {
        enemyManager = GameObject.Find("/Enemies").GetComponent<EnemyManager>();
        screenFade = GameObject.Find("/Canvas/ScreenFade").GetComponent<ScreenFadeController>();
        scoreText = GameObject.Find("/Canvas/Score").GetComponent<TextMeshProUGUI>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver(){
        enemyManager.SetEnemyActive(false);
        screenFade.FadeIn(true);
    }

}
