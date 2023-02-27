using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayGame()
    {
        SceneManager.LoadScene("Tutoriel");
    }

    private void OnEnable()
    {
        if(GameManager.instance != null)
        {
            Player.instance = null;
            ProjectilesManager.instance = null;
            Scaling.instance = null;
            ParticleManager.instance = null;
            DamageNumbersManager.instance = null; ;
            GameManager.instance = null;
        }
    }

    public void QuitGame()
    {
        //Application.Quit();
        Destroy(GameManager.instance);
        Debug.Log(GameManager.instance);
    }
}
