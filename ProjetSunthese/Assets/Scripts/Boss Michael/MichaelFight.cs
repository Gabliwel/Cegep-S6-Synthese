using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MichaelFight : Enemy
{
    [SerializeField] GameObject player; 
    [SerializeField] private List<BossAttack> michaelAttacks;

    private string[] Attacks = new string[3];
    private string currentAttack;
    private int attackNb = 0;

    private bool attackInProgress = false;

    private float attackDelay = 3f;

    private Animator animator;

    private HPBar hpBar;

    void Start()
    {
        animator = GetComponent<Animator>();
        Attacks[0] = "CHARGE";
        Attacks[1] = "TELEPORT";
        Attacks[2] = "PROJECTILE";
        player = GameObject.FindGameObjectWithTag("Player");

        hp = Scaling.instance.CalculateHealthOnScaling(baseHP);
        damageDealt = Scaling.instance.CalculateDamageOnScaling(baseDamageDealt);

        for (int i = 0; i < michaelAttacks.Count; i++)
        {
            michaelAttacks[i] = Instantiate(michaelAttacks[i]);
            michaelAttacks[i].transform.SetParent(gameObject.transform);
        }

        hpBar = GetComponentInChildren<HPBar>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
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

    public override void Die()
    {
        Scaling.instance.ScalingIncrease();
        base.Die();
    }

    public override void Harm(float ammount, float poison)
    {
        base.Harm(ammount, poison);
        hpBar.UpdateHp(hp, Scaling.instance.CalculateHealthOnScaling(baseHP));
    }

    protected override void WasPoisonHurt() 
    {
        hpBar.UpdateHp(hp, Scaling.instance.CalculateHealthOnScaling(baseHP));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<Player>().Harm(damageDealt);
        }
    }
}
