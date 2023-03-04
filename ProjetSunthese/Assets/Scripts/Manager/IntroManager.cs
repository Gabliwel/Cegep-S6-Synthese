using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private TalkingCharacter[] talkingCharacters;
    [SerializeField] private CinematicBars cinematicBars;
    [SerializeField] private SimpleBillyMovement billy;
    [SerializeField] private GameObject canvas;

    [SerializeField] private float barTime = 2;
    [SerializeField] private float barSpace = 300;

    private float speed = 2.5f;

    [SerializeField] private Transform billyStartPosition;
    [SerializeField] private Transform billyMidPosition;
    [SerializeField] private Transform billyEndPosition;
    private Animator sceneTransition;

    [SerializeField] private GameObject[] playerPossiblities;
    private Player player;

    private bool isChangingLevel = false;

    private void Awake()
    {
        sceneTransition = GameObject.FindGameObjectWithTag("LevelFade").GetComponent<Animator>();

        int index = Random.Range(0, playerPossiblities.Length);
        playerPossiblities[index].SetActive(true);
        player = playerPossiblities[index].GetComponent<Player>();
        player.BlocAttack(true);
        player.BlocMovement(true);
    }

    private void Start()
    {
        player.gameObject.GetComponent<PlayerInteractables>().AvoidLink();
        billy.transform.position = billyStartPosition.position;
        StartCoroutine(Cinematic());
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && !isChangingLevel)
        {
            isChangingLevel = false;
            StopAllCoroutines();
            canvas.SetActive(false);
            StartCoroutine(ChangeScene());
        }
    }

    private IEnumerator ChangeScene()
    {
        isChangingLevel = true;
        sceneTransition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        player.BlocAttack(false);
        player.BlocMovement(false);
        SceneManager.LoadScene("Tutoriel");
    }

    private IEnumerator Cinematic()
    {
        cinematicBars.Activate(barTime, barSpace);
        yield return new WaitForSeconds(barTime);

        billy.MoveUpAnim();
        while (billy.transform.position.y < billyMidPosition.position.y)
        {
            billy.transform.Translate(Vector3.up * Time.deltaTime * speed);
            yield return null;
        }
        billy.IdleAnim();

        for (int i = 0; i < talkingCharacters.Length; i++)
        {
            TalkingCharacter charact = talkingCharacters[i];


            yield return new WaitForSeconds(0.1f);
            
            charact.ActivateStimuli();
            charact.Interact(null);
            while (!charact.HasDialogueEnded())
                yield return null;
            charact.DeactivateStimuli();
            yield return new WaitForSeconds(0.2f);
        }

        billy.MoveUpAnim();
        while (billy.transform.position.y < billyEndPosition.position.y)
        {
            billy.transform.Translate(Vector3.up * Time.deltaTime * speed);
            yield return null;
        }

        StartCoroutine(ChangeScene());
    }
}
