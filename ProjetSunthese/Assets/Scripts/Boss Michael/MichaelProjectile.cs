using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MichaelProjectile : BossAttack
{
    private Player player;
    private SpriteRenderer bossSprite;
    private GameObject boss;
    [SerializeField] GameObject projectile;

    private float damage = 0;

    private float fadeAmount = 1f;
    private bool attackInProgress = false;

    private bool fadeInProgress = true;

    private float reactionTime = 1.5f;

    private Vector3 savedPlayerPos;

    public override void Launch()
    {
        gameObject.SetActive(true);
        isAvailable = false;
        attackInProgress = true;
        
        if(damage == 0)
        {
            damage = transform.parent.GetComponent<MichaelFight>().SendDamage();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isAvailable = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        boss = GameObject.FindGameObjectWithTag("Boss");
        bossSprite = boss.GetComponent<SpriteRenderer>();
        projectile = Instantiate(projectile);
        projectile.SetActive(false);
        type = BossAttackType.Michael;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.root.gameObject.activeSelf)
        {
            if (attackInProgress)
            {
                ShootProjectile();
            }
        }
        else
        {
            projectile.SetActive(false);
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

                    boss.transform.position = new Vector3(vector2.x + savedPlayerPos.x, vector2.y + savedPlayerPos.y, 0);

                    Reapear();

                    projectile.transform.position = boss.transform.position;
                    ProjectilleMovement boll = projectile.GetComponent<ProjectilleMovement>();
                    boll.SetDestination(savedPlayerPos);
                    boll.SetDamage(damage);
                    attackInProgress = false;
                    reactionTime = 1.5f;
                    isAvailable = true;
                }
            }
        }
    }

    private void Disapear()
    {
        Collider2D[] colliders = boss.GetComponentsInChildren<Collider2D>(true);
        colliders[0].enabled = false;
        colliders[1].enabled = false;
        fadeInProgress = false;
        bossSprite.color = new Color(1f, 1f, 1f, fadeAmount);
        fadeAmount = 1f;
    }

    private void Reapear()
    {
        bossSprite.color = new Color(1f, 1f, 1f, 1f);
        Collider2D[] colliders = boss.GetComponentsInChildren<Collider2D>(true);
        colliders[0].enabled = true;
        colliders[1].enabled = true;

        fadeInProgress = true;
    }

    private void Fadeing()
    {
        if (fadeInProgress)
        {
            bossSprite.color = new Color(1f, 1f, 1f, fadeAmount);
            fadeAmount -= 0.025f;
        }
    }
}
