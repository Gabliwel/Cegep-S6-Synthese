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
}
