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
    EarlyCentralBoss,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private const float maxLives = 100;

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

    private float currentLife = maxLives;
    private int gold;
    private int currentXp;

    bool scenesAreInTransition = false;

    private bool textsNotLinked = true;

    Text playerGoldText;
    Text playerXPText;
    Text playerLivesText;
    Text gameOverText;

    private string gameOverInfo = "";

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

    private void Start()
    {
        currentXp = playerInfo.CurrentXp;
        gold = playerInfo.Gold;
        currentLife = playerInfo.Health;
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

            if (actualLevel == Scene.GameOver)
            {
                gameOverText = GameObject.FindGameObjectWithTag("GameOver").GetComponent<Text>();
                gameOverText.text = gameOverInfo;
                return;
            }

            playerLivesText = GameObject.FindGameObjectWithTag("Life").GetComponent<Text>();
            playerLivesText.text = currentLife.ToString();

            playerGoldText = GameObject.FindGameObjectWithTag("Gold").GetComponent<Text>();
            playerGoldText.text = gold.ToString();

            playerXPText = GameObject.FindGameObjectWithTag("CurrentXP").GetComponent<Text>();
            playerXPText.text = currentXp.ToString();

            UpdateHUD();
        }
    }

    public void UpdateHUD()
    {
        currentLife = playerInfo.Health;
        gold = playerInfo.Gold;
        currentXp = playerInfo.CurrentXp;

        playerXPText.text = "Xp : " + currentXp.ToString();
        playerGoldText.text = "Gold : " + gold.ToString();
        playerLivesText.text = "Life : " + currentLife.ToString();
    }

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
        actualLevel = Scene.GameOver;
        gameOverInfo = "Victory";
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
        else
            SceneManager.LoadScene("GameOver");

        scenesAreInTransition = false;
    }

    public void ResetGame()
    {
        currentLife = maxLives;
        actualLevel = Scene.Tutoriel;
        SceneManager.LoadScene("Tutoriel");
    }

    public void PlayerDie()
    {
        currentLife--;
        playerLivesText.text = currentLife.ToString();
    }

    public void SetGameOver()
    {
        actualLevel = Scene.GameOver;
        gameOverInfo = "Game Over";
        StartCoroutine(RestartLevelDelay(0, actualLevel));
    }
}