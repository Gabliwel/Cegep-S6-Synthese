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
        MusicMaker.instance.PauseState(true);
        pauseUI.SetActive(true);
        Time.timeScale = 0;
        SoundMaker.instance.PauseSound(Camera.main.transform.position);
    }

    public void Unpause()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1;
        MusicMaker.instance.PauseState(false);
        SoundMaker.instance.UnpauseSound(Camera.main.transform.position);
    }

    private void OnEnable()
    {
        Unpause();
    }
}
