using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MichaelFight : Enemy
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject dangerCircle;
    [SerializeField] GameObject dangerRectangle;
    [SerializeField] GameObject projectile;
    [SerializeField] private List<BossAttack> michaelAttacks;

    private string[] Attacks = new string[3];
    private string currentAttack;
    private int attackNb = 0;

    private float fadeAmount = 1f;
    private SpriteRenderer sprite;
    private bool attackInProgress = false;

    private bool fadeInProgress = true;

    private float attackDelay = 3f;
    private float reactionTime = 1.5f;

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
        player = GameObject.FindGameObjectWithTag("Player");

        for(int i = 0; i < michaelAttacks.Count; i++)
        {
            michaelAttacks[i] = Instantiate(michaelAttacks[i]);
        }
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
                attackNb = Random.Range(0, 3);
                currentAttack = Attacks[attackNb];
                ExecuteCurrentAttack();
            }
        }
        else
        {
            if (michaelAttacks[attackNb].IsAvailable())
            {
                ResetAttackState();
            }
        }
    }

    private void CheckAnimationSide()
    {
        animator.SetFloat("Move X", player.transform.position.x - transform.position.x);
        animator.SetFloat("Move Y", player.transform.position.y - transform.position.y);
    }

    private void ExecuteCurrentAttack()
    {
            switch (currentAttack)
            {
                case "CHARGE":
                StartFadedCharge();
                break;
                case "TELEPORT":
                    StartTeleportUnder();
                    break;
                case "PROJECTILE":
                    StartProjectile();
                    break;
            }
    }

    public void ResetAttackState()
    {
        attackInProgress = false;
        attackDelay = 3f;
    }

    private void StartFadedCharge()
    {
        michaelAttacks[0].Launch();
    }

    private void StartProjectile()
    {
        michaelAttacks[2].Launch();
    }


    private void StartTeleportUnder()
    {
        michaelAttacks[1].Launch();
    }

    protected override void Drop()
    {
        player.GetComponent<Player>().GainDrops(2, xpGiven, goldDropped);

        GetComponent<BossDrops>().BossDrop(transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<Player>().Harm(damageDealt);
        }
    }
}
