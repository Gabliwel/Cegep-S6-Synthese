using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Linq;

public enum Scene
{
    Tutoriel,
    CentralBoss,
    CharlesLevel,
    GabLevel,
    GabShop,
    KevenLevel,
    MarcAntoine,
    EarlyCentralBoss
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private const int firstGamingLevel = 1;
    private const int lastGamingLevel = 3;
    private const int maxLives = 3;

    [SerializeField]
    private GameObject player;
    private Player playerInfo;

    private Scene actualLevel = 0;
    List<Scene> levelSceneList = new List<Scene>
    {
    Scene.CharlesLevel,
    Scene.GabLevel,
    Scene.GabShop,
    Scene.KevenLevel,
    Scene.MarcAntoine
    };
    [SerializeField] List<Scene> levelsDone;// = new List<Scene>();
    List<BossAttack> bofrerStolenAttacks = new List<BossAttack>();
<<<<<<< HEAD
    
    private float lives;
    private int gold;
    private int currentXp;
=======


    private int lives = maxLives;
>>>>>>> 4ce8e189340b924ebafa91b447b94d8a2188cdd3

    bool scenesAreInTransition = false;

    private bool textsNotLinked = true;

    Text playerGoldText;
    Text playerXPText;
    Text playerLivesText;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
<<<<<<< HEAD
        playerInfo = player.GetComponent<Player>();
=======

>>>>>>> 4ce8e189340b924ebafa91b447b94d8a2188cdd3
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
        bofrerStolenAttacks = newAttacks;
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
            if (actualLevel == 0) return;

            playerLivesText = GameObject.FindGameObjectWithTag("Life").GetComponent<Text>();
            playerLivesText.text = lives.ToString();

            playerGoldText = GameObject.FindGameObjectWithTag("Gold").GetComponent<Text>();
            playerGoldText.text = gold.ToString();

            playerXPText = GameObject.FindGameObjectWithTag("CurrentXP").GetComponent<Text>();
            playerXPText.text = currentXp.ToString();
        }
    }

    //public void UpdateHUD()
    //{
    //    lives = playerInfo.Health;
    //    gold = playerInfo.Gold;
    //    currentXp = playerInfo.CurrentXp;

    //    playerXPText.text = currentXp.ToString();
    //    playerGoldText.text = gold.ToString();
    //    playerLivesText.text = lives.ToString();
    //}

    public void GetRandomNextLevelAndStart()
    {
        Debug.Log("Before : " + levelSceneList.Count);
        int nbSceneAccessible = levelSceneList.Count;
        if (nbSceneAccessible > 0)
        {
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
        Debug.Log("AHAHAHAHAHAHAHAHHAHAHAHAHAHAHAHAHAHHAHAHAHAHAHAHAH END");
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
        textsNotLinked = true;

        if (lives == 0)
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
        else
            SceneManager.LoadScene("GabShop");

        scenesAreInTransition = false;
    }

    public void ResetGame()
    {
        lives = maxLives;
        actualLevel = Scene.Tutoriel;
        SceneManager.LoadScene("Tutoriel");
    }

    public void PlayerDie()
    {
        lives--;
        playerLivesText.text = lives.ToString();
    }

    public void SetGameOver()
    {

    }
}