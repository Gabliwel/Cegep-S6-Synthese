using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONSave : MonoBehaviour
{
    private string path = "";
    private string persitentPath = "";
    private AchivementManager manager;

    private void Awake()
    {
        SetPaths();
    }
    void Start()
    {
        manager = GetComponent<AchivementManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnApplicationQuit()
    {
        SaveData(manager.getAchivementData());
    }

    private void SetPaths()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveAchivement.json";
        persitentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveAchivement.json";
    }

    public void SaveData(AchivementClass achives)
    {
        string savePath = persitentPath;

        string json = JsonUtility.ToJson(achives);

        using StreamWriter writer = new StreamWriter(savePath);

        writer.Write(json);
    }

    public AchivementClass LoadData()
    {
        using StreamReader reader = new StreamReader(persitentPath);
        string json = reader.ReadToEnd();

        AchivementClass data = JsonUtility.FromJson<AchivementClass>(json);

        return data;
    }
}
