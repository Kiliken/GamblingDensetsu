using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{

    [SerializeField] List<GameObject> effectPrefabs;
    [SerializeField] List<float> effectCooldowns;

    public int effNum = -1;
    GameObject newEffect;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float ApplyEffect(Enemy enemy)
    {
        if (effNum == -1)
            return -1f;

        //!enemyObj.transform.Find($"{effectPrefabs[effNum].name}(Clone)")
        GameObject enemyObj = enemy.GetEnemyObj();
        if (newEffect = Instantiate(effectPrefabs[effNum], enemyObj.transform)) {
            newEffect.transform.parent = enemyObj.transform;
            return effectCooldowns[effNum];
        }
        
        return -1f;
        
    }
}
