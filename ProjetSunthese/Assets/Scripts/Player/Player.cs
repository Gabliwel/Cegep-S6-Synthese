using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerAnimationController animationController;
    private PlayerMovement playerMovement;
    private Weapon weapon;

    private void Awake()
    {
        animationController = GetComponent<PlayerAnimationController>();
        playerMovement = GetComponent<PlayerMovement>();
        weapon = GetComponentInChildren<Weapon>();
    }

    private void Update()
    {
    }
}
