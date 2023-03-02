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

    private GameObject pauseUI;

    private Scene actualLevel = 0;
    List<Scene> levelSceneList = new List<Scene>
    {
    Scene.CharlesLevel,
    Scene.GabLevel,
    Scene.KevenLevel,
    Scene.MarcAntoine,
    Scene.GabShop
    };
    [SerializeField] List<Scene> levelsDone;// = new List<Scene>();
    List<BossAttack> bofrerStolenAttacks = new List<BossAttack>();

    //private float currentLife = maxLives;
    //private int gold;
    //private int currentXp;

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
            /*pauseUI = GameObject.FindGameObjectWithTag("PauseUI");
            pauseUI.SetActive(false);*/
            if (actualLevel == Scene.GabGameOver)
            {
                return;
            }
            Debug.Log(playerInfo);
            Debug.Log(playerInfo.Health.CurrentMax);
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
        Debug.Log("Before : " + levelSceneList.Count);
        int nbSceneAccessible = levelSceneList.Count;

        if (nbSceneAccessible > 0)
        {
            if(nbSceneAccessible == 5)
            {
                nbSceneAccessible -= 1;
            }

            if (nbSceneAccessible == 1 && levelSceneList.ElementAt(0) == Scene.GabShop)
            {
                LoadEndScene();
                return;
            }

            int randomChoice = UnityEngine.Random.Range(0, nbSceneAccessible);
            Debug.Log(randomChoice);
            actualLevel = levelSceneList.ElementAt(randomChoice);
            StartNextlevel(0, actualLevel);
        }
        else
        {
            LoadEndScene();
        }
    }

    [ContextMenu("Early wood")]
    public void GoToEarlyBofrer()
    {
        Debug.Log("leaving to early");
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
        //AchivementManager.instance.AddWeaponWonWith(Player.instance.GetComponentInChildren<WeaponInformations>().GetWeaponType());
        Debug.Log("AHAHAHAHAHAHAHAHHAHAHAHAHAHAHAHAHAHHAHAHAHAHAHAHAH END");
        actualLevel = Scene.GabVictory;
        StartCoroutine(RestartLevelDelay(0, actualLevel));
    }

    public void GetBackToMainStageAndStart()
    {
        actualLevel = Scene.CentralBoss;
        StartCoroutine(RestartLevelDelay(0, Scene.CentralBoss));
    }

    public void RemoveSceneFromSceneList(Scene sceneToRemove)
    {
        levelsDone.Add(sceneToRemove);
        levelSceneList.Remove(sceneToRemove);
    }

    public void StartNextlevel(float delay, Scene chosenLevel)
    {
        textsNotLinked = true;
        if (scenesAreInTransition) return;
        scenesAreInTransition = true;

        StartCoroutine(RestartLevelDelay(delay, chosenLevel));
        RemoveSceneFromSceneList(chosenLevel);
        Debug.Log("After : " + levelSceneList.Count);
    }

    public void RestartLevel(float delay)
    {
        if (scenesAreInTransition) return;

        scenesAreInTransition = true;

        StartCoroutine(RestartLevelDelay(delay, actualLevel));
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
        StartCoroutine(RestartLevelDelay(3, actualLevel));
        //AchivementManager.instance.Died();
    }
}