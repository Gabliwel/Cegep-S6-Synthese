using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MichaelFight : Enemy
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject dangerCircle;
    [SerializeField] GameObject dangerRectangle;
    [SerializeField] GameObject projectile;

    private string[] Attacks = new string[3];
    private string currentAttack;

    private float fadeAmount = 1f;
    private SpriteRenderer sprite;
    private bool attackInProgress = false;

    private bool fadeInProgress = true;

    private float attackDelay = 3f;
    private float reactionTime = 1.5f;
    private Vector3 savedPlayerPos;
    private bool charging = false;

    private Animator animator;

    void Start()
    {
        scaling = GameObject.FindGameObjectWithTag("Scaling").GetComponent<Scaling>();
        hp = 100 * scaling.SendScaling();
        xpGiven = 110;
        goldDropped = 50;

        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        dangerRectangle.transform.localScale = new Vector3(10, 1.5f, 1);
        Attacks[0] = "CHARGE";
        Attacks[1] = "TELEPORT";
        Attacks[2] = "PROJECTILE";
    }

    // Update is called once per frame
    void Update()
    {
        if (!attackInProgress)
        {
            attackDelay -= Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 0.009f);
            CheckAnimationSide();
            if (attackDelay <= 0)
            {
                attackDelay = 3f;
                attackInProgress = true;
                currentAttack = Attacks[Random.Range(0, 3)];
            }
        }
        ExecuteCurrentAttack();
    }

    private void CheckAnimationSide()
    {

        if (Mathf.Abs(Mathf.Abs(player.transform.position.x) - Mathf.Abs(transform.position.x))
                > Mathf.Abs((Mathf.Abs(player.transform.position.y) - Mathf.Abs(transform.position.y))))
        {
            if (player.transform.position.x < transform.position.x)
            {
                animator.Play("Michael_Ani_Left");
            }
            if (player.transform.position.x > transform.position.x)
            {
                animator.Play("Michael_Ani_Right");
            }
        }

        if (Mathf.Abs(Mathf.Abs(player.transform.position.x) - Mathf.Abs(transform.position.x))
                < Mathf.Abs((Mathf.Abs(player.transform.position.y) - Mathf.Abs(transform.position.y)))) {
            if (player.transform.position.y < transform.position.y)
            {
                animator.Play("Michael_Ani");
            }
            if (player.transform.position.y > transform.position.y)
            {
                animator.Play("Michael_Ani_Back");
            }
        }
    }

    private void ExecuteCurrentAttack()
    {
        switch (currentAttack)
        {
            case "CHARGE":
                FadedCharge();
                break;
            case "TELEPORT":
                TeleportUnder();
                break;
            case "PROJECTILE":
                ShootProjectile();
                break;
        }
    }


    private void TeleportUnder()
    {
        if (attackInProgress)
        {
            Fadeing();

            if (fadeAmount <= 0)
            {
                Disapear();

                dangerCircle.SetActive(true);
                dangerCircle.transform.position = player.transform.position;
            }

            if (!fadeInProgress)
            {
                reactionTime -= Time.deltaTime;

                if(reactionTime <= 0)
                {
                    Reapear();
                    gameObject.transform.position = dangerCircle.transform.position;
                    dangerCircle.SetActive(false);
                    attackInProgress = false;
                    reactionTime = 1.5f;
                }
            }
        }
    }

    private void FadedCharge()
    {
        if (attackInProgress)
        {
            if (!charging)
            {
                Fadeing();
            }

            if (fadeAmount <= 0)
            {
                Disapear();

                var vector2 = Random.insideUnitCircle.normalized * 10;

                savedPlayerPos = player.transform.position;

                gameObject.transform.position = new Vector3(vector2.x + savedPlayerPos.x, vector2.y + savedPlayerPos.y, 0);

                Vector3 bossPos = gameObject.transform.position;

                Vector3 spot = new Vector3(savedPlayerPos.x, savedPlayerPos.y, 0);
                Vector3 position = (bossPos + spot) / 2;

                float distanceY = Mathf.Pow(Mathf.Abs(savedPlayerPos.y - bossPos.y), 2);
                float distanceX = Mathf.Pow(Mathf.Abs(savedPlayerPos.x - bossPos.x), 2);
                float distance = Mathf.Sqrt(distanceX + distanceY);

                dangerRectangle.transform.rotation = Quaternion.LookRotation(Vector3.forward, position - bossPos) * Quaternion.Euler(0, 0, 90);
                dangerRectangle.transform.position = position;
                dangerRectangle.transform.localScale = new Vector3(distance / 5, 0.2f, 1);

                dangerRectangle.SetActive(true);
            }

            if (!fadeInProgress)
            {
                reactionTime -= Time.deltaTime;

                if (reactionTime <= 0)
                {
                    Reapear();
                    charging = true;
                    reactionTime = 1.5f;
                }
            }

            if(fadeInProgress && charging)
            {
                dangerRectangle.SetActive(false);
                transform.position = Vector2.MoveTowards(transform.position, savedPlayerPos, 1);

                if(transform.position == savedPlayerPos)
                {
                    attackInProgress = false;
                    charging = false;
                }
            }
        }
    }

    private void ShootProjectile()
    {
        if (attackInProgress)
        {
            Fadeing();

            if (fadeAmount <= 0)
            {
                Disapear();
            }

            if (!fadeInProgress)
            {
                reactionTime -= Time.deltaTime;

                if (reactionTime <= 0)
                {
                    var vector2 = Random.insideUnitCircle.normalized * 10;

                    savedPlayerPos = player.transform.position;

                    gameObject.transform.position = new Vector3(vector2.x + savedPlayerPos.x, vector2.y + savedPlayerPos.y, 0);

                    sprite.color = new Color(1f, 1f, 1f, 1f);
                    Collider2D[] colliders = GetComponentsInChildren<Collider2D>(true);
                    colliders[0].enabled = true;
                    colliders[1].enabled = true;

                    projectile.transform.position = transform.position;
                    projectile.GetComponent<ProjectilleMovement>().SetDestination(savedPlayerPos);
                    attackInProgress = false;
                    fadeInProgress = true;
                    reactionTime = 1.5f;
                }
            }
        }
    }

    private void Disapear()
    {
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>(true);
        colliders[0].enabled = false;
        colliders[1].enabled = false;
        fadeInProgress = false;
        fadeAmount = 1f;
    }

    private void Reapear()
    {
        sprite.color = new Color(1f, 1f, 1f, 1f);
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>(true);
        colliders[0].enabled = true;
        colliders[1].enabled = true;

        fadeInProgress = true;
    }

    private void Fadeing()
    {
        if (fadeInProgress)
        {
            sprite.color = new Color(1f, 1f, 1f, fadeAmount);
            fadeAmount -= 0.025f;
        }
    }

    protected override void Drop()
    {
        player.GetComponent<Player>().GainDrops(2, xpGiven, goldDropped);
        GameObject.FindGameObjectWithTag("WeaponSwitch").GetComponent<WeaponSwitchManager>().SwitchWeaponOnGround(Random.Range(1, 5), transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<Player>().Harm(damageDealt);
        }
    }
}
