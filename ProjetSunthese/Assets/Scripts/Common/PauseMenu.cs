using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool isPause = false;
    private GameObject player;
    private GameManager gameManager;
    private AudioSource camSrc;
    private SoundManager soundManager;
    private float musicTime = 0;

    private void Start()
    {
        musicTime = 0;
        camSrc = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        //soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)/* && gameManager.CanPause()*/)
        {
            ChangeState();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        isPause = true;
        //player.GetComponent<PlayerController>().ChangePauseState(isPause);
        ManageSound();
        gameObject.SetActive(true);
    }

    private void ManageSound()
    {
        if (camSrc != null)
        {
            if (isPause)
            {
                musicTime = camSrc.time;
                camSrc.Stop();
                //camSrc.PlayOneShot(soundManager.Pause);
            }
            else
            {
                //camSrc.PlayOneShot(soundManager.Unpause);
                camSrc.time = musicTime;
                camSrc.Play();
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        isPause = false;
        //player.GetComponent<PlayerController>().ChangePauseState(isPause);
        ManageSound();
        gameObject.SetActive(false);
    }

    public void ChangeState()
    {
        if (isPause)
            Resume();
        else
            Pause();
        //gameManager.PauseUI(isPause);
    }
}
