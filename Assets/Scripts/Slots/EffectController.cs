using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{

    [SerializeField] List<GameObject> effectPrefabs;

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

    public void ApplyEffect(Enemy enemy)
    {
        if (effNum == -1)
            return;

        GameObject enemyObj = enemy.GetEnemyObj();
        if (!enemyObj.transform.Find($"{effectPrefabs[effNum].name}(Clone)")) {
            newEffect = Instantiate(effectPrefabs[effNum],enemyObj.transform);
            newEffect.transform.parent = enemyObj.transform;
        }
    }
}
