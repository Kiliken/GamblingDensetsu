using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float MaxHP = 100f;
    public float HP;
    public float damageReceivedModifier = 0f;

    [Space(12)]
    [SerializeField] Image hpBar;

    // Start is called before the first frame update
    void Start()
    {
        HP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TakeDamage(float dmg){
        HP -= Mathf.Max(0, dmg + damageReceivedModifier);
        hpBar.fillAmount = HP / MaxHP;
        if (HP <= 0f){
            Death();
        }
    }


    public void RestoreHP(float HPRestored){
        HP = Mathf.Min(MaxHP, HP + HPRestored);
        hpBar.fillAmount = HP / MaxHP;
    }


    private void Death(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
