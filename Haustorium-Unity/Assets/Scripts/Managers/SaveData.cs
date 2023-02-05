using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class SaveData : MonoBehaviour
{
    public Memory mem; //Leaving this public is risky...
    float timer = 0f;

    #region SaveData Singleton
    static private SaveData data;
    static public SaveData DATA { get { return data; } }

    void CheckDataInScene()
    {

        if (data == null)
        {
            data = this;
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
        mem = new Memory();
        SaveToJson();
    }

    public void LoadFromJson()
    {
        string filePath = Application.persistentDataPath + "/SaveData.json";
        string data = System.IO.File.ReadAllText(filePath);

        mem = JsonUtility.FromJson<Memory>(data);

        //Load in volume control
        AudioManager am = AudioManager.AM;
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
public class Memory
{
    public float MasterVolume;
    public float MusicVolume;
    public float EffectsVolume;
    public float UIVolume;

    public Memory()
    {
        //Create with default values
        MasterVolume = 1f;
        MusicVolume = 1f;
        EffectsVolume = 1f;
        UIVolume = 1f;
    }
}
