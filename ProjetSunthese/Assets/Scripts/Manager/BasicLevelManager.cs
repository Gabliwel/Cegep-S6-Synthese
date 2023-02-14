using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicLevelManager : MonoBehaviour
{
    private GameObject player;
    private Player playerScript;
    private GameObject boss;
    private GameObject followingBoss;

    [Header("Cinematic")]
    [SerializeField] private bool doCinematic = true;
    [SerializeField] private CinematicBars cinematicBars;
    [SerializeField] private float barTime = 2;
    [SerializeField] private float barSpace = 300;
    [SerializeField] private float zoom = 2;
    [SerializeField] private float unzoom = 4;
    [SerializeField] private float timeToBoss = 1.3f;
    private float smooth = 2;
    private float buffer = 0.1f;
    private Camera cam;

    [Header("Scene activations")]
    [SerializeField] private GameObject[] activatedOnlyDuringFollowing;
    [SerializeField] private GameObject[] activatedOnlyAfterFollowing;

    [Header("Layers")]
    [SerializeField] private string spawnLayer;
    [SerializeField] private string bossLayer;

    [Header("Tranforms for positions")]
    [SerializeField] private Transform firstPlayerTrans;
    [SerializeField] private Transform followingBossTrans;
    [SerializeField] private Transform secondPlayerTrans;
    [SerializeField] private Transform bossTrans;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        boss = GameObject.FindGameObjectWithTag("Boss");
        followingBoss = GameObject.FindGameObjectWithTag("FollowingBoss");
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        playerScript = player.GetComponent<Player>();

        ChangeListsActivation(true);
        SetStartPositions();

        boss.SetActive(false);
        followingBoss.SetActive(true);
    }

    protected virtual void SetStartPositions()
    {
        playerScript.ChangeLayer(spawnLayer, spawnLayer);
        player.transform.position = firstPlayerTrans.position;
        followingBoss.transform.position = followingBossTrans.position;
    }

    private void Start()
    {
        if(doCinematic)
        {
            StartCoroutine(Cinematic());
        }
        else
        {
            followingBoss.GetComponent<AnimatedFollow>().StartChasing(player.transform, this);
        }
    }

    protected virtual IEnumerator Cinematic()
    {
        playerScript.BlocMovement(true);
        playerScript.BlocAttack(true);

        float firstOrthographicSize = cam.orthographicSize;
        FollowPlayer f = cam.GetComponent<FollowPlayer>();
        f.enabled = false;
        cam.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -15);
        cinematicBars.Activate(barTime, barSpace);

        while (cam.orthographicSize > zoom + buffer)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoom, Time.deltaTime * smooth);
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        while (cam.orthographicSize < unzoom - buffer)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, unzoom, Time.deltaTime * smooth);
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        float t = 0;
        Vector3 start = cam.transform.position;
        Vector3 target = new Vector3(followingBoss.transform.position.x, followingBoss.transform.position.y, cam.transform.position.z);

        while(t < timeToBoss)
        {
            t += Time.deltaTime / timeToBoss;
            cam.transform.position = Vector3.Lerp(start, target, t);
            yield return null;
        }

        //possible de launch animation de boss

        yield return new WaitForSeconds(0.1f);

        while (cam.orthographicSize > zoom + buffer)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoom, Time.deltaTime * smooth);
            yield return null;
        }

        yield return new WaitForSeconds(1);

        t = 0;

        while (t < timeToBoss)
        {
            t += Time.deltaTime / timeToBoss;
            cam.transform.position = Vector3.Lerp(target, start, t);
            yield return null;
        }

        cinematicBars.Deactivate(barTime);

        while (cam.orthographicSize < firstOrthographicSize - buffer)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, firstOrthographicSize, Time.deltaTime * smooth);
            yield return null;
        }
        cam.orthographicSize = firstOrthographicSize;

        f.enabled = true;
        followingBoss.GetComponent<AnimatedFollow>().StartChasing(player.transform, this);
        playerScript.BlocMovement(false);
        playerScript.BlocAttack(false);
    }

    // Status du "during" en param, et iverse pour le "after"
    private void ChangeListsActivation(bool status)
    {
        foreach (GameObject obj in activatedOnlyDuringFollowing)
        {
            obj.SetActive(status);
        }
        foreach (GameObject obj in activatedOnlyAfterFollowing)
        {
            obj.SetActive(!status);
        }
    }

    public void FollowingBossTouched()
    {
        StartCoroutine(TransitionToBoss());
    }

    protected virtual IEnumerator TransitionToBoss()
    {
        playerScript.BlocMovement(true);
        playerScript.BlocAttack(true);
        yield return new WaitForSeconds(0.2f);
        //do something (sound, anim...)


        cinematicBars.Activate(0.75f, 2000);
        yield return new WaitForSeconds(1f);
        ChangeListsActivation(false);
        followingBoss.SetActive(false);
        boss.transform.position = bossTrans.position;
        player.transform.position = secondPlayerTrans.position;
        playerScript.ChangeLayer(bossLayer, bossLayer);
        boss.SetActive(true);
        cinematicBars.Deactivate(0.75f);
        yield return new WaitForSeconds(1f);
        //begin movement and attack
        playerScript.BlocMovement(false);
        playerScript.BlocAttack(false);
    }

    protected virtual void FirstSpawn()
    {
        player.transform.position = firstPlayerTrans.position;
        followingBoss.transform.position = followingBossTrans.position;
    }

    protected virtual void SecondSpawn()
    {
        player.transform.position = secondPlayerTrans.position;
        boss.transform.position = bossTrans.position;
    }
}
