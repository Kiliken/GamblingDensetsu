using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagePopUp : MonoBehaviour
{
    [SerializeField] Text text;

    [Space(12)]
    [SerializeField] float lifetime = 0.6f;
    [SerializeField] float minDist = 2f;
    [SerializeField] float maxDist = 3f;

    Vector3 iniPos;
    Vector3 targetPos;
    float timer;
    Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        transform.LookAt(2* transform.position - Camera.main.transform.position);

        canvas.worldCamera = Camera.main;
        float diretion = Random.Range(0, 90f) ;
        iniPos = transform.position;
        float dist = Random.Range(minDist, maxDist);
        targetPos = iniPos + (Quaternion.Euler(0f ,0f ,diretion) * new Vector3(dist,dist,0f));
        transform.localScale = new Vector3(.005f, .005f, .005f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float fraction = lifetime / 2f;

        if (timer > lifetime)
           Destroy(gameObject);
        if (timer < fraction)
            text.color = Color.Lerp(text.color, Color.clear, (timer - fraction) / (lifetime - fraction));

        transform.localPosition = Vector3.Lerp(iniPos, targetPos, Mathf.Sin(timer / lifetime));
        //transform.localPosition = Vector3.Lerp(Vector3.zero, Vector3.one, Mathf.Sin(timer / lifetime));

    }

    public void SetDamageText(float damage) { 
        text.text = damage.ToString();
    }

    public void SetTextColor(Color color) { 
        text.color = color;
    }
}
