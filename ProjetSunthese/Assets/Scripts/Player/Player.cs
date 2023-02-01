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

    public void Hard(float ammount)
    {
        if(iframesTimer <= 0)
        {
            //hitplayer
        }
    }
}
