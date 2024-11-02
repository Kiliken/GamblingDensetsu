using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    class Buffs {
        public GameObject buffPrefab;
        public string buffName;
    };

    List<Buffs> buffsList;

    GameObject newEffect;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyEffect(int effNum, Enemy enemy)
    {
        GameObject enemyObj = enemy.GetEnemyObj();
        if (!enemyObj.transform.Find(buffsList[effNum].buffName)) {
            newEffect = Instantiate(buffsList[effNum].buffPrefab,enemyObj.transform);
            newEffect.transform.parent = enemyObj.transform;
        }
    }
}
