using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    public Transform player;
    public Transform playerCam;
    public Transform enemy;
    private Quaternion eRot = Quaternion.identity;
    private Vector3 ePos = Vector3.zero;

    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float activeTime = 1.5f;
    float activeTimer = 0f;
    [SerializeField] float fadeTime = 1.5f;
    float maxFadeTime;


    // Start is called before the first frame update
    void Start()
    {
        maxFadeTime = fadeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(activeTimer < activeTime){
            activeTimer += Time.deltaTime;
        }
        else{
            fadeTime -= Time.deltaTime;
            canvasGroup.alpha = fadeTime/maxFadeTime;
            if(fadeTime <= 0){
                Destroy(this.gameObject);
            }
        }
        if(enemy){
            Vector3 dmgLocation = new Vector3(enemy.position.x, player.position.y, enemy.position.z);
            Vector3 dir = (dmgLocation - player.position).normalized;
            float angle = Vector3.SignedAngle(dir, playerCam.forward, Vector3.up);
            transform.localEulerAngles = new Vector3(0, 0, angle);
        }
        else{
            activeTimer = activeTime;
        }
    }
}
