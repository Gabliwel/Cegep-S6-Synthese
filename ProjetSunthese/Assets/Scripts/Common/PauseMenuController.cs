using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("StartingMenu");
    }
    
    public void Resume()
    {
        GameManager.instance.ChangeState();
    }
}
