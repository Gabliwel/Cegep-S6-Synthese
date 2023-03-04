using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tomstone : Interactable
{
    [Header("Action")]
    [SerializeField] private bool replay = false;
    [SerializeField] private bool menu = false;

    private Animator sceneTransitor;
    private Coroutine coroutine = null;
    private SimpleBillyMovement billy;

    private void Start()
    {
        sceneTransitor = GameObject.FindGameObjectWithTag("LevelFade").GetComponent<Animator>();
        billy = GameObject.FindGameObjectWithTag("Billy").GetComponent<SimpleBillyMovement>();
    }


    // Just at end scene, so simple player with SimpleBillyMovement
    public override void Interact(Player player) 
    {
        if (coroutine != null) return; 
        coroutine = StartCoroutine(ChangeScene());
    }

    private IEnumerator ChangeScene()
    {
        billy.StopMove();
        if (sceneTransitor != null)
        {
            sceneTransitor.SetTrigger("Start");
            yield return new WaitForSeconds(1);
        }

        if(replay) SceneManager.LoadScene("GabIntro");
        else if(menu) SceneManager.LoadScene("StartingMenu");
    }
}
