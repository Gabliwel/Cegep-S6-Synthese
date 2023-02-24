using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Billy.Weapons;

public class AchivementManager : MonoBehaviour
{
    public static AchivementManager instance;

    private AchievementsList achivementData;
    private JSONSave save;
    private DescriptionBox achivementPop;

    private bool dead;
    private bool gotRageQuit;
    private float timeSinceDead = 3f;

    private const string ENEMIES = "You killed 30 enemies!";
    private const string CHEST = "You opened 30 chests";
    private const string BOB = "You defeated Bob for the first time!";
    private const string MICHAEL = "You defeated Michael for the first time!";
    private const string JEANGUY = "You defeated Jean Guy for the first time!";
    private const string GONTRAND = "You defeated Gontrand for the first time!";
    private const string GAMEDONE = "You beated the game for the first time!";
    private const string RAGEQUIT = "You didnt take that lost so well...";
    //void Start()
    //{
    //    if (instance == null)
    //        instance = this;

    //    else if (instance != this)
    //        Destroy(gameObject);

    //    DontDestroyOnLoad(gameObject);

    //    save = GetComponent<JSONSave>();
    //    achivementData = save.LoadData();
    //    if(achivementData == null)
    //    {
    //        achivementData = new AchivementClass();
    //    }
    //    gotRageQuit = achivementData.rageQuit;

    //    achivementPop = GameObject.FindGameObjectWithTag("AchivementPopup").GetComponent<DescriptionBox>();

    //    if (achivementData.rageQuitFirst && achivementData.rageQuit)
    //    {
    //        StartCoroutine(ShowAchivementGot(RAGEQUIT));
    //        achivementData.rageQuitFirst = false;
    //        save.SaveData(achivementData);
    //    }

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (!gotRageQuit)
    //    {
    //        if (dead)
    //        {
    //            achivementData.rageQuit = true;
    //            timeSinceDead -= Time.deltaTime;

    //            if (timeSinceDead <= 0)
    //            {
    //                achivementData.rageQuit = false;
    //            }
    //        }
    //        else
    //        {
    //            timeSinceDead = 3f;
    //        }
    //    }
    //}

    //public void Died()
    //{
    //    dead = true;
    //    achivementData.nbDeath++;
    //    save.SaveData(achivementData);
    //}

    //public void OpenedChest()
    //{
    //    achivementData.nbChestOpened++;
    //    if (achivementData.nbChestOpened > 2)
    //    {
    //        achivementData.lotsChestOpened = true;
    //        StartCoroutine(ShowAchivementGot(CHEST));
    //    }
    //    save.SaveData(achivementData);
    //}

    //public void KilledEnnemies()
    //{
    //    achivementData.nbKilledTotal += 1;

    //    if(!achivementData.nbEnemyKilled && achivementData.nbKilledTotal > 1)
    //    {
    //        achivementData.nbEnemyKilled = true;
    //        StartCoroutine(ShowAchivementGot(ENEMIES));
    //    }
    //    save.SaveData(achivementData);
    //}

    //public void KilledGontrand()
    //{
    //    achivementData.beatGontrand = true;
    //    StartCoroutine(ShowAchivementGot(GONTRAND));
    //    save.SaveData(achivementData);
    //}

    //public void KilledMichael()
    //{
    //    achivementData.beatMichael = true;
    //    StartCoroutine(ShowAchivementGot(MICHAEL));
    //    save.SaveData(achivementData);
    //}

    //public void KilledBob()
    //{
    //    achivementData.beatBob = true;
    //    StartCoroutine(ShowAchivementGot(BOB));
    //    save.SaveData(achivementData);
    //}

    //public void KilledJeanGuy()
    //{
    //    achivementData.beatJeanGuy = true;
    //    StartCoroutine(ShowAchivementGot(JEANGUY));
    //    save.SaveData(achivementData);
    //}

    //public void BeatTheGame()
    //{
    //    achivementData.beatGontrand = true;
    //    StartCoroutine(ShowAchivementGot(GAMEDONE));
    //    save.SaveData(achivementData);
    //}

    //public void AddWeaponWonWith(WeaponsType type)
    //{
    //    if (!achivementData.wonWith.Contains(type))
    //    {
    //        achivementData.wonWith.Add(type);
    //        save.SaveData(achivementData);
    //    }
    //}
    //private IEnumerator ShowAchivementGot(string description)
    //{
    //    achivementPop.PopUp("achievement", description);
    //    yield return new WaitForSeconds(5f);
    //    achivementPop.Close();
    //}

    //public AchievementsList getAchivementData()
    //{
    //    return this.achivementData;
    //}
}
