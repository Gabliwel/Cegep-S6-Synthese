using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuStateController : MonoBehaviour
{
    private GameObject pauseUI;
    private void Awake()
    {
        pauseUI = GameObject.FindGameObjectWithTag("PauseUI");
        pauseUI.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetButtonDown("Pause") || Input.GetButtonDown("Cancel"))
        {
            if (pauseUI.activeSelf)
            {
                Unpause();
                return;
            }
            Pause();
        }
    }
    public void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1;
    }

    private void OnEnable()
    {
        Unpause();
    }
}
