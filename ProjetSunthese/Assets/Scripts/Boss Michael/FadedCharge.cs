using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadedCharge : BossAttack
{
    private Player player;
    private SpriteRenderer bossSprite;
    private GameObject boss;
    [SerializeField] GameObject dangerRectangle;

    private float fadeAmount = 1f;
    private bool attackInProgress = false;

    private bool fadeInProgress = true;

    private float reactionTime = 1.5f;
    private Vector3 savedPlayerPos;
    private bool charging = false;

    void Start()
    {
        tempBossAttackReady = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        boss = GameObject.FindGameObjectWithTag("Boss");
        bossSprite = boss.GetComponent<SpriteRenderer>();
        dangerRectangle = Instantiate(dangerRectangle);
        dangerRectangle.SetActive(false);
    }

    void Update()
    {
        if (attackInProgress)
        {
            FadedChargeAttack();
        }
    }

    public override void Launch()
    {
        attackInProgress = true;
        tempBossAttackReady = false;
    }

    private void FadedChargeAttack()
    {
        Debug.Log("Charge");
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

                boss.transform.position = new Vector3(vector2.x + savedPlayerPos.x, vector2.y + savedPlayerPos.y, 0);

                Vector3 bossPos = boss.transform.position;

                Vector3 spot = new Vector3(savedPlayerPos.x, savedPlayerPos.y, 0);
                Vector3 position = (bossPos + spot) / 2;

                float distanceY = Mathf.Pow(Mathf.Abs(savedPlayerPos.y - bossPos.y), 2);
                float distanceX = Mathf.Pow(Mathf.Abs(savedPlayerPos.x - bossPos.x), 2);
                float distance = Mathf.Sqrt(distanceX + distanceY);

                dangerRectangle.transform.rotation = Quaternion.LookRotation(Vector3.forward, position - bossPos) * Quaternion.Euler(0, 0, 90);
                dangerRectangle.transform.position = position;
                dangerRectangle.transform.localScale = new Vector3(distance, 1.5f, 1);

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

            if (fadeInProgress && charging)
            {
                dangerRectangle.SetActive(false);
                boss.transform.position = Vector2.MoveTowards(boss.transform.position, savedPlayerPos, 1);

                if (boss.transform.position == savedPlayerPos)
                {
                    attackInProgress = false;
                    charging = false;
                    tempBossAttackReady = true;
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
