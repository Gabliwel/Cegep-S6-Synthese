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
    [SerializeField, TextAreaAttribute] protected string desc = "";

    [Header("Only if used in shop")]
    [SerializeField] protected int basePrice = 0;

    protected SpriteRenderer sprite;

    public bool UseTextBox { get => useTextBox; }
    public string Desc { get => desc; }

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public virtual void ChangeSelectedState(bool selected, DescriptionBox descBox)
    {
        if (selected)
        {
            sprite.material = selectedMaterial;
            if (useTextBox) descBox.PopUp(desc);
        }
        else
        {
            sprite.material = defaultMaterial;
            if (useTextBox) descBox.Close();
        }
    }

    public virtual void Interact(Player player) { }

    public int GetPrice()
    {
        return basePrice;
    }
}
