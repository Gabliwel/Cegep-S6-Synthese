using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
struct Dialogue
{
    [SerializeField] public string[] lines;
}

public class BofrerSceneManager : MonoBehaviour
{
    [SerializeField] Vector3 playerSpawnPosition;
    [SerializeField] AudioClip normalSong;
    [SerializeField] AudioClip secondPhaseSong;
    [SerializeField] private CinematicBars cinematicBars;
    [SerializeField] private float barTime = 2;
    [SerializeField] private float barSpace = 300;
    [SerializeField] private float zoom = 2;
    [SerializeField] private float timeToBoss = 1.3f;
    [SerializeField] private float smooth = 2;
    [SerializeField] private float buffer = 0.1f;
    [SerializeField] private GameObject bofrer;
    [SerializeField] private Dialogue[] possibleDialogues;
    private TalkingCharacter cutsceneInteractable;

    private void Start()
    {
        Player.instance.transform.position = playerSpawnPosition;
        Player.instance.ChangeLayer("Layer 1", "Layer 1");
        MusicMaker.instance.PlayMusic(normalSong, true);
        cutsceneInteractable = GetComponentInChildren<TalkingCharacter>();
        StartCoroutine(Cinematic());
    }

    public void SwitchToPhase2()
    {
        MusicMaker.instance.FadeTo(secondPhaseSong, true);
    }

    protected virtual IEnumerator Cinematic()
    {
        Player.instance.BlocMovement(true);
        Player.instance.BlocAttack(true);
        bofrer.gameObject.SetActive(false);
        cutsceneInteractable.DeactivateStimuli();

        float firstOrthographicSize = Camera.main.orthographicSize;
        FollowPlayer f = Camera.main.GetComponent<FollowPlayer>();
        f.enabled = false;
        Camera.main.transform.position = new Vector3(Player.instance.transform.position.x, Player.instance.transform.position.y, -15);
        cinematicBars.Activate(barTime, barSpace);

        yield return new WaitForSeconds(1.2f);

        float t = 0;
        Vector3 start = Camera.main.transform.position;
        Vector3 target = new Vector3(bofrer.transform.position.x, bofrer.transform.position.y, Camera.main.transform.position.z);

        while (t < timeToBoss)
        {
            t += Time.deltaTime / timeToBoss;
            Camera.main.transform.position = Vector3.Lerp(start, target, t);
            yield return null;
        }

        //possible de launch animation de boss

        yield return new WaitForSeconds(0.1f);

        while (Camera.main.orthographicSize > zoom + buffer)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, zoom, Time.deltaTime * smooth);
            yield return null;
        }
        cutsceneInteractable.ActivateStimuli();
        cutsceneInteractable.SetDialogues(GetRandomDialogue());
        cutsceneInteractable.Interact(Player.instance);
        while (!cutsceneInteractable.HasDialogueEnded())
            yield return null;
        cutsceneInteractable.DeactivateStimuli();
        yield return new WaitForSeconds(1);

        while (Camera.main.orthographicSize < firstOrthographicSize - buffer)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, firstOrthographicSize, Time.deltaTime * smooth);
            yield return null;
        }

        t = 0;
        while (t < timeToBoss)
        {
            t += Time.deltaTime / timeToBoss;
            Camera.main.transform.position = Vector3.Lerp(target, start, t);
            yield return null;
        }

        cinematicBars.Deactivate(barTime);

        Camera.main.orthographicSize = firstOrthographicSize;

        f.enabled = true;

        cutsceneInteractable.gameObject.SetActive(false);
        bofrer.gameObject.SetActive(true);
        Player.instance.BlocMovement(false);
        Player.instance.BlocAttack(false);
    }

    private string[] GetRandomDialogue()
    {
        int dialogueChosen = UnityEngine.Random.Range(0, possibleDialogues.Length);
        return possibleDialogues[dialogueChosen].lines;
    }
}
