using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONSave : MonoBehaviour
{
    private string path = "";
    private string persitentPath = "";
    private AchievementManager manager;

    private void Awake()
    {
        SetPaths();
    }
    void Start()
    {
        manager = GetComponent<AchievementManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnApplicationQuit()
    {
        SaveData(manager.GetAchievementData());
    }

    private void SetPaths()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveAchivement.json";
        persitentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveAchivement.json";
    }

    public void SaveData(AchievementsList archives)
    {
        string savePath = persitentPath;

        string json = JsonUtility.ToJson(archives);

        using StreamWriter writer = new StreamWriter(savePath);

        writer.Write(json);
    }

    public AchievementsList LoadData()
    {
        string json;
        if (File.Exists(persitentPath))
        {
            using StreamReader reader = new StreamReader(persitentPath);
            json = reader.ReadToEnd();
        }
        else
        {
            File.Create(persitentPath);
            using StreamReader reader = new StreamReader(persitentPath);
            json = reader.ReadToEnd();
        }

        AchievementsList data = JsonUtility.FromJson<AchievementsList>(json);

        return data;
    }
}
