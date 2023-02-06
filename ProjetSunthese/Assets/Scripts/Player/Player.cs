using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerAnimationController animationController;
    private PlayerMovement playerMovement;
    private Weapon weapon;
    private float iframesTimer;

    private void Awake()
    {
        animationController = GetComponent<PlayerAnimationController>();
        playerMovement = GetComponent<PlayerMovement>();
        weapon = GetComponentInChildren<Weapon>();
    }

    private void Update()
    {
        if (iframesTimer > 0)
            iframesTimer -= Time.deltaTime;
    }

    public void AddIframes(float ammount)
    {
        iframesTimer += ammount;
    }

    /// <summary>
    /// harms the player. returns true if it did damage
    /// </summary>
    /// <param name="ammount"></param>
    /// <returns></returns>
    public bool Harm(float ammount)
    {
        if(iframesTimer <= 0)
        {
            Debug.Log("oof ouch ive been hit for " + ammount + " damage");
            return true;
        }
        return false;
    }
}
