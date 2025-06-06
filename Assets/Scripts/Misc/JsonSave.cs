using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonSave : MonoBehaviour
{
    public class SaveFile{
        public int highScore;
        public float bestTime;
    }

    public SaveFile gameSave;


    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }


    public void SaveGame(int score, float time){
        SaveFile newSave = new SaveFile{
            highScore = score,
            bestTime = time
        };

        string json = JsonUtility.ToJson(newSave);
        File.WriteAllText(Application.persistentDataPath + "/save.txt", json);
    }


    public bool LoadGame() {
        if (File.Exists(Application.persistentDataPath + "/save.txt")) {

            Debug.Log(Application.persistentDataPath + "/save.txt");
            string json = File.ReadAllText(Application.persistentDataPath + "/save.txt");
            gameSave = JsonUtility.FromJson<SaveFile>(json);
            Debug.Log(json);

            return true;
        }

        return false;
    }
}
