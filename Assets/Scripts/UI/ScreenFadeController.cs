using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenFadeController : MonoBehaviour
{
    Animator animator;
    private bool gameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void FadeIn(bool go){
        animator.SetTrigger("FadeIn");
        gameOver = go;
    }


    public void OnFadeInComplete(){
        if(gameOver){
            Debug.Log("Game Over");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else{
            Debug.Log("Main menu");
        }
    }
}
