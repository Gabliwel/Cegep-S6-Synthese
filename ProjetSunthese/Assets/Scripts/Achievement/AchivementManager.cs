using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Billy.Weapons;

public class AchivementManager : MonoBehaviour
{
    public static AchivementManager instance;


    public List<Achievement> achievements = new List<Achievement>();

    private AchivementClass achivementData;
    private JSONSave save;
    private DescriptionBox achivementPop;

    private bool dead;
    private bool gotRageQuit;
    private float timeSinceDead = 3f;

    private const string ENEMIES = "You killed 100 enemies!";
    private const string CHEST = "You opened 30 chests";
    private const string BOB = "You defeated Bob for the first time!";
    private const string MICHAEL = "You defeated Michael for the first time!";
    private const string JEANGUY = "You defeated Jean Guy for the first time!";
    private const string GONTRAND = "You defeated Gontrand for the first time!";
    private const string GAMEDONE = "You beat the game for the first time!";
    private const string RAGEQUIT = "You didn\'t take that lost so well...";
    private const string DEATH = "You died... 30 times...";
    private const string WEAPON_MASTER = "You won the game using every weapon!";

    private const string ENEMIES_NAME = "On a rampage";
    private const string CHEST_NAME = "My loot now!";
    private const string BOB_NAME = "Dungeon escape";
    private const string MICHAEL_NAME = "Back to the grave";
    private const string JEANGUY_NAME = "Climbing the mountain";
    private const string GONTRAND_NAME = "Extinguishing the flames";
    private const string RAGEQUIT_NAME = "Rage quit";
    private const string DEATH_NAME = "Learning in death";
    private const string GAMEDONE_NAME = "I did it!";
    private const string WEAPON_MASTER_NAME = "Like the back of my hand";
    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        save = GetComponent<JSONSave>();
        LoadAchievementClass();
        gotRageQuit = achivementData.rageQuit;

        if (transform.parent != null)
        {
            achivementPop = transform.parent.GetComponent<DescriptionBox>();
        }

        if (achivementData.rageQuitFirst && achivementData.rageQuit)
        {
            StartCoroutine(ShowAchivementGot(RAGEQUIT_NAME, RAGEQUIT));
            achivementData.rageQuitFirst = false;
            save.SaveData(achivementData);
        }

    }

    private void LoadAchievementClass()
    {
        achivementData = save.LoadData();
        if (achivementData == null)
        {
            achivementData = new AchivementClass();
        }
    }

    public void ReloadAchievementAndSetInObjects()
    {
        LoadAchievementClass();
        CreateListOfAchievementWithState();
    }

    void CreateListOfAchievementWithState()
    {
        achievements.Clear();
        achievements.Add(new Achievement(CHEST_NAME, CHEST, achivementData.lotsChestOpened));
        achievements.Add(new Achievement(ENEMIES_NAME, ENEMIES, achivementData.nbEnemyKilled));
        achievements.Add(new Achievement(BOB_NAME, BOB, achivementData.beatBob));
        achievements.Add(new Achievement(GONTRAND_NAME, GONTRAND, achivementData.beatGontrand));
        achievements.Add(new Achievement(JEANGUY_NAME, JEANGUY, achivementData.beatJeanGuy));
        achievements.Add(new Achievement(MICHAEL_NAME, MICHAEL, achivementData.beatMichael));
        achievements.Add(new Achievement(DEATH_NAME, DEATH, achivementData.skillIssue));
        achievements.Add(new Achievement(RAGEQUIT_NAME, RAGEQUIT, achivementData.rageQuit));
        achievements.Add(new Achievement(GAMEDONE_NAME, GAMEDONE, achivementData.beatTheGame));
        achievements.Add(new Achievement(WEAPON_MASTER_NAME, WEAPON_MASTER, achivementData.weaponMaster));
    }

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

    public int GetNumOfDeath()
    {
        return achivementData.nbDeath;
    }

    public void Died()
    {
        dead = true;
        achivementData.nbDeath++;

        if (achivementData.nbDeath >= 30)
        {
            achivementData.skillIssue = true;
            StartCoroutine(ShowAchivementGot(DEATH_NAME, DEATH));
        }

        save.SaveData(achivementData);
    }

    public void OpenedChest()
    {
        achivementData.nbChestOpened++;
        if (achivementData.nbChestOpened >= 30 && !achivementData.lotsChestOpened)
        {
            achivementData.lotsChestOpened = true;
            StartCoroutine(ShowAchivementGot(CHEST_NAME, CHEST));
        }
        save.SaveData(achivementData);
    }

    public void KilledEnnemies()
    {
        achivementData.nbKilledTotal += 1;

        if (!achivementData.nbEnemyKilled && achivementData.nbKilledTotal >= 100)
        {
            achivementData.nbEnemyKilled = true;
            StartCoroutine(ShowAchivementGot(ENEMIES_NAME, ENEMIES));
        }
        save.SaveData(achivementData);
    }

    public void KilledGontrand()
    {
        if (achivementData.beatGontrand) return;
        achivementData.beatGontrand = true;
        StartCoroutine(ShowAchivementGot(GONTRAND_NAME, GONTRAND));
        save.SaveData(achivementData);
    }

    [ContextMenu("Test")]
    public void KilledMichael()
    {
        if (achivementData.beatMichael) return;
        achivementData.beatMichael = true;
        StartCoroutine(ShowAchivementGot(MICHAEL_NAME, MICHAEL));
        save.SaveData(achivementData);
    }

    public void KilledBob()
    {
        if (achivementData.beatBob) return;
        achivementData.beatBob = true;
        StartCoroutine(ShowAchivementGot(BOB_NAME, BOB));
        save.SaveData(achivementData);
    }

    public void KilledJeanGuy()
    {
        if (achivementData.beatJeanGuy) return;
        achivementData.beatJeanGuy = true;
        StartCoroutine(ShowAchivementGot(JEANGUY_NAME, JEANGUY));
        save.SaveData(achivementData);
    }

    public void BeatTheGame()
    {
        if (achivementData.beatTheGame) return;
        achivementData.beatTheGame = true;
        save.SaveData(achivementData);
    }

    public void AddWeaponWonWith(WeaponsType type)
    {
        if (achivementData.wonWith.Count == 0)
        {
            BeatTheGame();
        }

        if (!achivementData.wonWith.Contains(type))
        {
            achivementData.wonWith.Add(type);

            save.SaveData(achivementData);
        }
    }
    private IEnumerator ShowAchivementGot(string title, string description)
    {
        if (achivementPop == null || transform.parent == null)
        {
            achivementPop = GameObject.FindGameObjectWithTag("AchivementPopup").GetComponent<DescriptionBox>();
        }

        achivementPop.PopUp(title, description);
        yield return new WaitForSeconds(5f);
        achivementPop.Close();
    }

    public AchivementClass getAchivementData()
    {
        return this.achivementData;
    }
}

public class Achievement
{
    public Achievement(string title, string description, bool isCompleted)
    {
        Title = title;
        Description = description;
        IsCompleted = isCompleted;
    }

    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
}
