using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Linq;
using TMPro;

public enum Scene
{
    Tutoriel,
    CentralBoss,
    CharlesLevel,
    GabLevel,
    GabShop,
    KevenLevel,
    MarcAntoine,
    EarlyCentralBoss,
    GabGameOver,
    GabVictory
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //private const float maxLives = 100;

    private GameObject player;
    private Player playerInfo;

    private Scene actualLevel = 0;
    List<Scene> levelSceneList = new List<Scene>
    {
    Scene.CharlesLevel,
    Scene.GabLevel,
    Scene.KevenLevel,
    Scene.MarcAntoine
    };
    [SerializeField] List<Scene> levelsDone;
    List<BossAttack> bofrerStolenAttacks = new List<BossAttack>();

    bool scenesAreInTransition = false;

    private bool textsNotLinked = true;

    private TMP_Text playerGoldText;
    private XpBar playerXpBar;
    private LifeBar playerLifeBar;

    private Animator sceneTransition = null;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        player = GameObject.FindGameObjectWithTag("Player");
        playerInfo = player.GetComponent<Player>();
    }

    void Update()
    {
        linkTexts();
    }

    public List<Scene> GetLevelsDone()
    {
        return levelsDone;
    }

    public List<BossAttack> GetStoredBofrerStolenAttacks()
    {
        return bofrerStolenAttacks;
    }

    public void StoreBofrerStolenAttacks(List<BossAttack> newAttacks)
    {
        bofrerStolenAttacks = new List<BossAttack>(newAttacks);
    }

    public void ClearBofrerStolenAttack()
    {
        bofrerStolenAttacks.Clear();
    }

    private void linkTexts()
    {
        if (textsNotLinked)
        {
            textsNotLinked = false;

            if (actualLevel == Scene.GabGameOver)
            {
                return;
            }
            playerLifeBar = GameObject.FindGameObjectWithTag("Life").GetComponent<LifeBar>();
            playerLifeBar.SetDefault(playerInfo.Health);

            playerGoldText = GameObject.FindGameObjectWithTag("Gold").GetComponentInChildren<TMP_Text>();
            playerGoldText.text = playerInfo.Gold.ToString("n0");

            playerXpBar = GameObject.FindGameObjectWithTag("CurrentXP").GetComponent<XpBar>();
            playerXpBar.UpdateBar(playerInfo.CurrentXp, playerInfo.NeededXp, playerInfo.Level);

            sceneTransition = GameObject.FindGameObjectWithTag("LevelFade").GetComponent<Animator>();
        }
    }

    public bool NeedLinkWithActivePlayer()
    {
        if (actualLevel == Scene.GabGameOver) return false;
        return true;
    }

    public void UpdateGold()
    {
        playerGoldText.text = playerInfo.Gold.ToString("n0");
    }

    public void UpdateXp()
    {
        playerXpBar.UpdateBar(playerInfo.CurrentXp, playerInfo.NeededXp, playerInfo.Level);
    }

    public void UpdateHealth()
    {
        playerLifeBar.UpdateHealth(playerInfo.Health);
    }

    public void GetRandomNextLevelAndStart()
    {
        if (levelSceneList.Count <= 0)
        {
            LoadEndScene();
            return;
        }

        if (actualLevel != Scene.GabShop && levelSceneList.Count != 4)
        {
            int chance = UnityEngine.Random.Range(0, 100);
            chance = 20;
            if (chance < 40)
            {
                actualLevel = Scene.GabShop;
                StartNextlevel(0, actualLevel);
                return;
            }
        }
        else
        {
            int randomChoice = UnityEngine.Random.Range(0, levelSceneList.Count);
            actualLevel = levelSceneList.ElementAt(randomChoice);
            RemoveSceneFromSceneList(actualLevel);
            StartNextlevel(0, actualLevel);

        }
    }

    [ContextMenu("Early wood")]
    public void GoToEarlyBofrer()
    {
        int nbSceneAccessible = levelSceneList.Count;
        if (nbSceneAccessible > 0)
        {
            actualLevel = Scene.EarlyCentralBoss;
            StartNextlevel(0, actualLevel);
        }
        else
        {
            LoadEndScene();
        }
    }

    public void SetNextLevel()
    {
        switch (actualLevel)
        {
            case Scene.Tutoriel:
                GoToEarlyBofrer();
                break;
            case Scene.CentralBoss:
                GetRandomNextLevelAndStart();
                break;
            case Scene.GabShop:
                GetRandomNextLevelAndStart();
                break;
            case Scene.EarlyCentralBoss:
                GetRandomNextLevelAndStart();
                break;
            default:
                GetBackToMainStageAndStart();
                break;
        }
    }

    public void LoadEndScene()
    {
        AchivementManager.instance.AddWeaponWonWith(Player.instance.GetComponentInChildren<WeaponInformations>().GetWeaponType());
        actualLevel = Scene.GabVictory;
        StartNextlevel(0, actualLevel);
    }

    public void GetBackToMainStageAndStart()
    {
        actualLevel = Scene.CentralBoss;
        StartNextlevel(0, actualLevel);
    }

    public void RemoveSceneFromSceneList(Scene sceneToRemove)
    {
        levelsDone.Add(sceneToRemove);
        levelSceneList.Remove(sceneToRemove);
    }

    private void StartNextlevel(float delay, Scene chosenLevel)
    {
        textsNotLinked = true;
        if (scenesAreInTransition) return;
        scenesAreInTransition = true;

        StartCoroutine(RestartLevelDelay(delay, chosenLevel));
    }

    private IEnumerator RestartLevelDelay(float delay, Scene level)
    {
        yield return new WaitForSeconds(delay);
        if (sceneTransition != null)
        {
            sceneTransition.SetTrigger("Start");
            yield return new WaitForSeconds(1);
        }

        textsNotLinked = true;

        if (level.Equals(Scene.Tutoriel))
            SceneManager.LoadScene("Tutoriel");
        else if (level.Equals(Scene.KevenLevel))
            SceneManager.LoadScene("KevenNiveau");
        else if (level.Equals(Scene.CharlesLevel))
            SceneManager.LoadScene("CharlesLevel");
        else if (level.Equals(Scene.GabLevel))
            SceneManager.LoadScene("GabLevel");
        else if (level.Equals(Scene.MarcAntoine))
            SceneManager.LoadScene("MarcAntoine");
        else if (level.Equals(Scene.CentralBoss))
            SceneManager.LoadScene("CentralBoss");
        else if (level.Equals(Scene.EarlyCentralBoss))
            SceneManager.LoadScene("EarlyCentralBoss");
        else if (level.Equals(Scene.GabShop))
            SceneManager.LoadScene("GabShop");
        else if (level.Equals(Scene.GabGameOver))
            SceneManager.LoadScene("GabGameOver");
        else
            SceneManager.LoadScene("GabVictory");

        scenesAreInTransition = false;
    }

    public void PlayerDie()
    {
        UpdateHealth();
    }

    public void SetGameOver()
    {
        actualLevel = Scene.GabGameOver;
        AchivementManager.instance.Died();
        StartNextlevel(3, actualLevel);
    }
}