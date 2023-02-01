using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONSave : MonoBehaviour
{
    private string path = "";
    private string persitentPath = "";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetPaths()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveAchivement.json";
        persitentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveAchivement.json";
    }

    public void SaveData(AchivementClass achives)
    {
        string savePath = path;

        string json = JsonUtility.ToJson(achives);

        using StreamWriter writer = new StreamWriter(savePath);

        writer.Write(json);
    }

    public void LoadData()
    {
        using StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();

        AchivementClass data = JsonUtility.FromJson<AchivementClass>(json);
    }
}
