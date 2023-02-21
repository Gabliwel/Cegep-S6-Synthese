using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchivementManager : MonoBehaviour
{
    public static AchivementManager instance;

    private AchivementClass achivementData;
    private JSONSave save;

    [SerializeField] bool dead;
    private bool gotRageQuit;
    private float timeSinceDead = 3f;
    void Start()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        save = GetComponent<JSONSave>();
        achivementData = save.LoadData();
        if(achivementData == null)
        {
            achivementData = new AchivementClass();
        }
        gotRageQuit = achivementData.rageQuit;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gotRageQuit)
        {
            if (dead)
            {
                achivementData.rageQuit = true;
                timeSinceDead -= Time.deltaTime;

                if (timeSinceDead <= 0)
                {
                    achivementData.rageQuit = false;
                }
            }
            else
            {
                timeSinceDead = 3f;
            }
        }
    }

    public void OpenedChest()
    {
        achivementData.nbChestOpened++;
        if (achivementData.nbChestOpened == 2)
        {
            achivementData.lotsChestOpened = true;
            save.SaveData(achivementData);
        }
    }

    public void KilledGontrand()
    {
        achivementData.beatGontrand = true;
        save.SaveData(achivementData);
    }

    public void KilledMichael()
    {
        achivementData.beatMichael = true;
        save.SaveData(achivementData);
    }

    public void KilledBob()
    {
        achivementData.beatBob = true;
        save.SaveData(achivementData);
    }

    public void KilledJeanGuy()
    {
        achivementData.beatJeanGuy = true;
        save.SaveData(achivementData);
    }

    public void BeatTheGame()
    {
        achivementData.beatGontrand = true;
        save.SaveData(achivementData);
    }

    public AchivementClass getAchivementData()
    {
        return this.achivementData;
    }
}
