using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreenScript : MonoBehaviour
{
    [SerializeField] GameObject replayButton;
    Animator animator;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI bestTimeText;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    // // Update is called once per frame
    // void Update()
    // {
        
    // }


    public void SetGameOverScreen(int score, int highScore, float time, float bestTime){
        scoreText.text = "スコア: " + score;
        highScoreText.text = "ハイスコア: " + highScore;

        if(score == highScore)
            highScoreText.color = Color.yellow;

        float m = Mathf.Max(0, Mathf.FloorToInt(time / 60));
        float s = Mathf.Max(0, Mathf.FloorToInt(time % 60));
        float ms = Mathf.Max(0, Mathf.FloorToInt((time % 1f) * 100));
        timeText.text = "タイム: " + m.ToString("00") + ":" + ((int)s).ToString("00") + ":" + ((int)ms).ToString("00");

        m = Mathf.Max(0, Mathf.FloorToInt(bestTime / 60));
        s = Mathf.Max(0, Mathf.FloorToInt(bestTime % 60));
        ms = Mathf.Max(0, Mathf.FloorToInt((bestTime % 1f) * 100));
        bestTimeText.text = "タイム: " + m.ToString("00") + ":" + ((int)s).ToString("00") + ":" + ((int)ms).ToString("00");

        if(time == bestTime)
            bestTimeText.color = Color.yellow;
    }


    public void OnFadeInComplete(){
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        replayButton.SetActive(true);
    }


    public void OnFadeOutComplete(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void OnReplayBtnPress(){
        animator.SetTrigger("fadeOut");
        replayButton.SetActive(false);
    }
}
