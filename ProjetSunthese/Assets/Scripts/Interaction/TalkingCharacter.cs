using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingCharacter : Interactable
{
    private DialogueBox dialogueBox;

    [Header("Info")]
    [SerializeField] private string name;
    [SerializeField] private string[] dialogues;

    private bool isDoingDialogue = false;

    private void Awake()
    {
        base.Awake();
        dialogueBox = GameObject.FindGameObjectWithTag("DialogueBox").GetComponent<DialogueBox>();
    }

    public override void ChangeSelectedState(bool selected, DescriptionBox descBox)
    {
        if (selected)
        {
            sprite.material = selectedMaterial;
        }
        else
        {
            sprite.material = defaultMaterial;
            if (isDoingDialogue) dialogueBox.Stop();
            isDoingDialogue = false;
        }
    }

    public override void Interact(Player player)
    {
        if (isDoingDialogue)
        {
            dialogueBox.Continue();
        }
        else
        {
            dialogueBox.RequestDialogue(name, dialogues, sprite.sprite, this);
            isDoingDialogue = true;
        }
    }

    public void DialogueEnded()
    {
        isDoingDialogue = false;
    }

    public bool HasDialogueEnded()
    {
        return !isDoingDialogue;
    }

    public bool IsDialogueWaiting()
    {
        return dialogueBox.IsWaiting();
    }

    public void SetDialogues(string[] newDialogues) 
    {
        dialogues = newDialogues;
    }

    public void ActivateStimuli()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
    public void DeactivateStimuli()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
