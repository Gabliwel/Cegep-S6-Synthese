using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Material")]
    [SerializeField] protected Material defaultMaterial;
    [SerializeField] protected Material selectedMaterial;

    protected SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public virtual void ChangeSelectedState(bool selected)
    {
        if (selected)
        {
            sprite.material = selectedMaterial;
        }
        else
        {
            sprite.material = defaultMaterial;
        }
    }

    public virtual void Interact(Player player) { }
}
