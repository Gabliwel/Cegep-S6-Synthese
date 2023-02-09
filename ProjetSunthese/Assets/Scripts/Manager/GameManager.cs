using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Linq;

public enum Scene  
{
    MainStage,
    Keven,
    FPP1,
    FPP2
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private const int firstGamingLevel = 1;
    private const int lastGamingLevel = 3;
    private const int maxLives = 3;

    private Scene actualLevel = 0;
    List<Scene> sceneList = Enum.GetValues(typeof(Scene)).Cast<Scene>().ToList();

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

        sceneList.Remove(Scene.MainStage);

        DontDestroyOnLoad(gameObject);
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
        Debug.Log("Before : " + sceneList.Count);
        int nbSceneAccessible = sceneList.Count;
        if(nbSceneAccessible > 0)
        {
            int randomChoice = UnityEngine.Random.Range(0, nbSceneAccessible);
            Debug.Log(randomChoice);
            StartNextlevel(0, (sceneList.ElementAt(randomChoice)));
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
        StartCoroutine(RestartLevelDelay(0, Scene.Keven)); 
    }

    public void RemoveSceneFromSceneList(Scene sceneToRemove)
    {
        sceneList.Remove(sceneToRemove);
    }
    
    public void StartNextlevel(float delay, Scene chosenLevel)
    {
        if (scenesAreInTransition) return;  

        scenesAreInTransition = true;

        StartCoroutine(RestartLevelDelay(delay, chosenLevel));
        RemoveSceneFromSceneList(chosenLevel);
        Debug.Log("After : " + sceneList.Count);
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
            SceneManager.LoadScene("Charles");
        else if (level.Equals(Scene.FPP1))
            SceneManager.LoadScene("FPP1");
        else if (level.Equals(Scene.FPP2))
            SceneManager.LoadScene("FPP2");
        else
            SceneManager.LoadScene("Keven");

        scenesAreInTransition = false;
    }

    public void ResetGame()
    {
        lives = maxLives;
        actualLevel = Scene.Keven;
        SceneManager.LoadScene("Gab");
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