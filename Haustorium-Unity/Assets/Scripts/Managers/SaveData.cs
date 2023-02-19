using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveData : MonoBehaviour
{
    public Data mem; //Leaving this public is risky...
    float timer = 0f;

    #region SaveData Singleton
    static private SaveData instance;
    static public SaveData Instance { get { return instance; } }

    void CheckDataInScene()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    private void Awake()
    {
        CheckDataInScene();

        string filePath = Application.persistentDataPath + "/SaveData.json";
        string data;
        try
        {
            data = System.IO.File.ReadAllText(filePath);
            LoadFromJson();
        }
        catch (FileNotFoundException e)
        {
            Debug.LogWarning(e.Message);
            CreateNewData();
        }

    }
    private void Update()
    {
        //Auto saving system
        timer += Time.deltaTime;
        if (timer > 30f)
        {
            timer = 0f;
            SaveToJson();
        }
    }

    #region Data Saving and Loading
    private void CreateNewData()
    {
        mem = new Data();
        SaveToJson();
    }

    public void LoadFromJson()
    {
        string filePath = Application.persistentDataPath + "/SaveData.json";
        string data = System.IO.File.ReadAllText(filePath);

        mem = JsonUtility.FromJson<Data>(data);

        //Load in volume control
        AudioManager am = AudioManager.Instance;
        am.SetMasterVolume(mem.MasterVolume);
        am.SetMusicVolume(mem.MusicVolume);
        am.SetEffectsVolume(mem.EffectsVolume);
        am.SetUIVolume(mem.UIVolume);

        //Load in trigger values

        Debug.Log("Loaded data");
    }

    public void SaveToJson()
    {
        string saveData = JsonUtility.ToJson(mem);
        string filePath = Application.persistentDataPath + "/SaveData.json"; //For my local machine: C:/Users/cpWhe/AppData/LocalLow/DefaultCompany/Jam Maker Unity/SaveData.json
        System.IO.File.WriteAllText(filePath, saveData);
        Debug.Log("Saved data");
    }

    public void ClearData()
    {
        CreateNewData();
    }
    #endregion

}


[System.Serializable]
public class Data
{
    public float MasterVolume;
    public float MusicVolume;
    public float EffectsVolume;
    public float UIVolume;

    public Data()
    {
        //Create with default values
        MasterVolume = 1f;
        MusicVolume = 1f;
        EffectsVolume = 1f;
        UIVolume = 1f;
    }
}
