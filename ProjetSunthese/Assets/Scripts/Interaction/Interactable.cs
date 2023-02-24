using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Material")]
    [SerializeField] protected Material defaultMaterial;
    [SerializeField] protected Material selectedMaterial;

    [Header("Description box")]
    [SerializeField] protected bool useTextBox = false;
    [SerializeField] protected string title = "";
    [SerializeField, TextAreaAttribute] protected string desc = "";

    [Header("Only if used in shop")]
    [SerializeField] protected int basePrice = 0;

    protected SpriteRenderer sprite;

    public bool UseTextBox { get => useTextBox; }
    public string Title { get => title; }
    public string Desc { get => desc; }

    protected virtual void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public virtual void ChangeSelectedState(bool selected, DescriptionBox descBox)
    {
        if (selected)
        {
            sprite.material = selectedMaterial;
            if (useTextBox) descBox.PopUp(title, desc);
        }
        else
        {
            sprite.material = defaultMaterial;
            if (useTextBox) descBox.Close();
        }
    }

    public virtual void Interact(Player player) { }

    public virtual int GetPrice()
    {
        return basePrice;
    }
}
