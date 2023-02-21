using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DescriptionBox : MonoBehaviour
{
    private TMP_Text msg;
    private Animator animator;

    private void Awake()
    {
        msg = GetComponentInChildren<TMP_Text>();
        animator = GetComponent<Animator>();
    }

    public void PopUp(string txt)
    {
        msg.text = txt;
        animator.SetBool("open", true);
    }

    public void Close()
    {
        animator.SetBool("open", false);
    }
}
