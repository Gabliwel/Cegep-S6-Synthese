using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private const int firstGamingLevel = 1;
    private const int lastGamingLevel = 3;
    private const int maxLives = 3;

    private int actualLevel = 0;

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

    public void RestartLevel(float delay)
    {
        if (scenesAreInTransition) return;  

        scenesAreInTransition = true;

        StartCoroutine(RestartLevelDelay(delay, actualLevel));
    }

    
    public void StartNextlevel(float delay)
    {
        if (scenesAreInTransition) return;  

        scenesAreInTransition = true;

        StartCoroutine(RestartLevelDelay(delay, GetNextLevel()));
    }

    private IEnumerator RestartLevelDelay(float delay, int level)
    {
        yield return new WaitForSeconds(delay);
        textsNotLinked = true;

        if (lives == 0)
            SceneManager.LoadScene("Charles");
        else if (level == 2)
            SceneManager.LoadScene("FPP1");
        else if (level == 3)
            SceneManager.LoadScene("FPP2");
        else
            SceneManager.LoadScene("Keven");

        scenesAreInTransition = false;
    }

    public void ResetGame()
    {
        lives = maxLives;
        actualLevel = 0;
        SceneManager.LoadScene("Scene0");
    }

    private int GetNextLevel()
    {
        if (++actualLevel == lastGamingLevel + 1)
            actualLevel = firstGamingLevel;

        return actualLevel;
    }

    public void PlayerDie()
    {
        lives--;
        playerLivesText.text = lives.ToString();
    }

}