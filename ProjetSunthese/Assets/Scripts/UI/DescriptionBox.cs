using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Billy.Rarity;

public class DescriptionBox : MonoBehaviour
{
    private Image back;
    private TMP_Text msg;
    private Animator animator;

    [Header("Default mats")]
    [SerializeField] private Material defaultMat;
    [SerializeField] private Material defaultOutlineMat;

    [Header("Rarity mats")]
    [SerializeField] private Material communMat;
    [SerializeField] private Material rareMat;
    [SerializeField] private Material epicMat;
    [SerializeField] private Material legendaryMat;
    [SerializeField] private Material uniqueMat;


    private void Start()
    {
        back = GetComponent<Image>();
        msg = GetComponentInChildren<TMP_Text>();
        animator = GetComponent<Animator>();

        back.material = defaultMat;
    }

    public void PopUp(string txt)
    {
        if(animator == null) animator = GetComponent<Animator>();
        back.material = defaultMat;
        msg.text = txt;
        animator.SetBool("open", true);
    }

    public void PopUp(string txt, ItemRarity rarity)
    {
        if (animator == null) animator = GetComponent<Animator>();

        switch (rarity)
        {
            case ItemRarity.COMMUN:
                back.material = communMat;
                break;
            case ItemRarity.RARE:
                back.material = rareMat;
                break;
            case ItemRarity.EPIC:
                back.material = epicMat;
                break;
            case ItemRarity.LEGENDARY:
                back.material = legendaryMat;
                break;
            case ItemRarity.UNIQUE:
                back.material = uniqueMat;
                break;
        }

        msg.text = txt;
        animator.SetBool("open", true);
    }

    public void Close()
    {
        animator.SetBool("open", false);
    }
}
