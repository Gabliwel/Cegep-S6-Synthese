using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogueBox : MonoBehaviour
{
    [Header("Link")]
    [SerializeField] private TMP_Text name;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Image image;
    [SerializeField] private GameObject endImageObj;

    [Header("Info")]
    [SerializeField] private float textSpeed;

    private Coroutine coroutine = null;
    private TalkingCharacter character;
    private string[] currentDialogues;
    private bool waitingForContinue;

    void Start()
    {
        endImageObj.SetActive(false);
        gameObject.SetActive(false);       
    }

    public void RequestDialogue(string newName, string[] dialogues, Sprite sprite, TalkingCharacter newCharacter)
    {
        name.text = newName;
        image.sprite = sprite;
        currentDialogues = dialogues;
        character = newCharacter;
        text.text = "";

        gameObject.SetActive(true);
        coroutine = StartCoroutine(DoDialogue());
    }

    public void Stop()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        gameObject.SetActive(false);
    }

    public void Continue()
    {
        if (!waitingForContinue) return;
        endImageObj.SetActive(false);
        text.text = "";
        waitingForContinue = false;
    }

    private IEnumerator DoDialogue()
    {
        for (int i = 0; i < currentDialogues.Length; i++)
        {
            foreach (char c in currentDialogues[i].ToCharArray())
            {
                text.text += c;
                if (c != ' ') yield return new WaitForSeconds(textSpeed);
            }
            endImageObj.SetActive(true);
            waitingForContinue = true;
            while (waitingForContinue)
            {
                yield return null;
            }
        }

        character.DialogueEnded();
        gameObject.SetActive(false);
    }
}
