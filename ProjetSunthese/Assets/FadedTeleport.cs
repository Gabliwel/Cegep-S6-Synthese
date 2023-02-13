using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadedTeleport : BossAttack
{
    private Player player;
    private SpriteRenderer bossSprite;
    private GameObject boss;
    [SerializeField] GameObject dangerCircle;

    private float fadeAmount = 1f;
    private bool attackInProgress = false;

    private bool fadeInProgress = true;

    private float reactionTime = 1.5f;

    public override void Launch()
    {
        gameObject.SetActive(true);
        isAvailable = false;
        attackInProgress = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        isAvailable = false;
        boss = GameObject.FindGameObjectWithTag("Boss");
        bossSprite = boss.GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attackInProgress)
        {
            TeleportUnder();
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

                if (reactionTime <= 0)
                {
                    Reapear();
                    boss.transform.position = dangerCircle.transform.position;
                    dangerCircle.SetActive(false);
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
