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
    private const string GAMEDONE = "You beated the game for the first time!";
    private const string RAGEQUIT = "You didnt take that lost so well...";
    private const string DEATH = "You died... 30 times...";
    private const string WEAPON_MASTER = "You won the game using every weapons!";

    private const string ENEMIES_NAME = "On a rampage";
    private const string CHEST_NAME = "My loot now!";
    private const string BOB_NAME = "Dungeon escape";
    private const string MICHAEL_NAME = "Back to the grave";
    private const string JEANGUY_NAME = "Climbing the mountain";
    private const string GONTRAND_NAME = "Extinguishing the flames";
    private const string GAMEDONE_NAME = "I did it!";
    private const string RAGEQUIT_NAME = "Rage quit";
    private const string DEATH_NAME = "Learning in death";
    private const string WEAPON_MASTER_NAME = "Like the back of my hands";
    void Awake()
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


        if(transform.parent != null)
        {
            achivementPop = transform.parent.GetComponent<DescriptionBox>();
        }

        if (achivementData.rageQuitFirst && achivementData.rageQuit)
        {
            StartCoroutine(ShowAchivementGot(RAGEQUIT_NAME, RAGEQUIT));
            achivementData.rageQuitFirst = false;
            save.SaveData(achivementData);
        }

        CreateListOfAchievementWithState();
    }

    void CreateListOfAchievementWithState()
    {
        achievements.Add(new Achievement("My loot now!", CHEST, achivementData.lotsChestOpened));
        achievements.Add(new Achievement("On a rampage", ENEMIES, achivementData.nbEnemyKilled));
        achievements.Add(new Achievement("Dungeon escape", BOB, achivementData.beatBob));
        achievements.Add(new Achievement("Extinguishing the flames", GONTRAND, achivementData.beatGontrand));
        achievements.Add(new Achievement("Climbing the mountain", JEANGUY, achivementData.beatJeanGuy));
        achievements.Add(new Achievement("Back to the grave", MICHAEL, achivementData.beatMichael));
        achievements.Add(new Achievement("Learning in death", DEATH, achivementData.skillIssue));
        achievements.Add(new Achievement("Rage quit", RAGEQUIT, achivementData.rageQuit));
        achievements.Add(new Achievement("I did it!", GAMEDONE, achivementData.beatTheGame));
        achievements.Add(new Achievement("Like the back of my hands", WEAPON_MASTER, achivementData.weaponMaster));
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
        if (achivementData.nbChestOpened >= 30)
        {
            achivementData.lotsChestOpened = true;
            StartCoroutine(ShowAchivementGot(CHEST_NAME, CHEST));
        }
        save.SaveData(achivementData);
    }

    public void KilledEnnemies()
    {
        achivementData.nbKilledTotal += 1;

        if(!achivementData.nbEnemyKilled && achivementData.nbKilledTotal >= 100)
        {
            achivementData.nbEnemyKilled = true;
            StartCoroutine(ShowAchivementGot(ENEMIES_NAME, ENEMIES));
        }
        save.SaveData(achivementData);
    }

    public void KilledGontrand()
    {
        achivementData.beatGontrand = true;
        StartCoroutine(ShowAchivementGot(GONTRAND_NAME, GONTRAND));
        save.SaveData(achivementData);
    }

    [ContextMenu("Test")]
    public void KilledMichael()
    {
        achivementData.beatMichael = true;
        StartCoroutine(ShowAchivementGot(MICHAEL_NAME, MICHAEL));
        save.SaveData(achivementData);
    }

    public void KilledBob()
    {
        achivementData.beatBob = true;
        StartCoroutine(ShowAchivementGot(BOB_NAME, BOB));
        save.SaveData(achivementData);
    }

    public void KilledJeanGuy()
    {
        achivementData.beatJeanGuy = true;
        StartCoroutine(ShowAchivementGot(JEANGUY_NAME, JEANGUY));
        save.SaveData(achivementData);
    }

    public void BeatTheGame()
    {
        achivementData.beatTheGame = true;
        //StartCoroutine(ShowAchivementGot(GAMEDONE));
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

            if(achivementData.wonWith.Count == 6)
            {
                //StartCoroutine(ShowAchivementGot(WEAPON_MASTER));
            }

            save.SaveData(achivementData);
        }
    }
    private IEnumerator ShowAchivementGot(string title, string description)
    {
        if(achivementPop == null || transform.parent == null)
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
