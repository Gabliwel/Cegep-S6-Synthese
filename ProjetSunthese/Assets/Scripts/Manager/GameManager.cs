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
    Charles,
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

    private Scene actualLevel = 0;
    List<Scene> levelSceneList = new List<Scene>
    {
    Scene.Charles,
    Scene.GabLevel,
    Scene.GabShop,
    Scene.KevenLevel,
    Scene.MarcAntoine,
    Scene.EarlyCentralBoss
    };
    

    private int lives = maxLives;

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
        
    }

    private void Start()
    {
        SetPlayerPosition();
    }

    void Update()
    {
        linkTexts();
    }

    private void linkTexts()
    {
        if (textsNotLinked)
        {
            textsNotLinked = false;
            if (actualLevel == 0) return;

            playerLivesText = GameObject.FindGameObjectWithTag("Life").GetComponent<Text>();
            playerLivesText.text = lives.ToString();

        }
    }

    public void GetRandomNextLevelAndStart()
    {
        Debug.Log("Before : " + levelSceneList.Count);
        int nbSceneAccessible = levelSceneList.Count;
        if(nbSceneAccessible > 0)
        {
            int randomChoice = UnityEngine.Random.Range(0, nbSceneAccessible);
            Debug.Log(randomChoice);
            StartNextlevel(0, (levelSceneList.ElementAt(randomChoice)));
        }
        else
        {
            LoadEndScene();
        }
    }

    public void LoadEndScene()
    {
        Debug.Log("AHAHAHAHAHAHAHAHHAHAHAHAHAHAHAHAHAHHAHAHAHAHAHAHAH END");
    }

    public void GetBackToMainStageAndStart()
    {
        StartCoroutine(RestartLevelDelay(0, Scene.CentralBoss)); 
    }

    public void RemoveSceneFromSceneList(Scene sceneToRemove)
    {
        levelSceneList.Remove(sceneToRemove);
    }
    
    public void StartNextlevel(float delay, Scene chosenLevel)
    {
        if (scenesAreInTransition) return;  

        scenesAreInTransition = true;

        StartCoroutine(RestartLevelDelay(delay, chosenLevel));
        RemoveSceneFromSceneList(chosenLevel);
        SetPlayerPosition();
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
        else if (level.Equals(Scene.Charles))
            SceneManager.LoadScene("Charles");
        else if(level.Equals(Scene.GabLevel))
            SceneManager.LoadScene("Charles");
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

    public void SetPlayerPosition()
    {
        switch (player.scene.name)
        {
            case "Tutoriel":
                player.transform.position = new Vector3(-165, 34, 0);
                break;
            case "CentralBoss":
                player.transform.position = new Vector3(1000, 1000, 0);
                break;
            case "Charles":
                player.transform.position = new Vector3(1000, 1000, 0);
                break;
            case "GabLevel":
                player.transform.position = new Vector3(1000, 1000, 0);
                break;
            case "GabShop":
                player.transform.position = new Vector3(1000, 1000, 0);
                break;
            case "KevenNiveau":
                player.transform.position = new Vector3(1000, 1000, 0);
                break;
            case "MarcAntoine":
                player.transform.position = new Vector3(1000, 1000, 0);
                break;
        }
    }
}