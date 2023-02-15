using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BofCharge : BossAttack
{
    private Player player;
    [SerializeField] GameObject dangerRectangle;
    [SerializeField] GameObject bossShadow;
    [SerializeField] float damage;
    [SerializeField] float speed = 6;

    private bool attackInProgress = false;

    private float reactionTime = 1.5f;
    private Vector3 savedPlayerPos;
    private bool charging = false;

    private bool aoeOnce = true;

    private Sensor sensor;
    private ISensor<Player> playerSensor;

    void Awake()
    {
        bossShadow = Instantiate(bossShadow);
        dangerRectangle = Instantiate(dangerRectangle);
        sensor = bossShadow.GetComponentInChildren<Sensor>(true);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerSensor = sensor.For<Player>();
        playerSensor.OnSensedObject += OnPlayerSense;
        playerSensor.OnUnsensedObject += OnPlayerUnsense;
    }

    void Update()
    {
        if (attackInProgress)
        {
            FadedChargeAttack();
        }
    }

    void OnPlayerSense(Player player)
    {
        Debug.Log("Damage");
        player.Harm(damage);
    }

    void OnPlayerUnsense(Player player)
    {

    }

    [ContextMenu("Test")]
    public override void Launch()
    {
        Debug.Log("Test");
        attackInProgress = true;
        isAvailable = false;
    }

    private void FadedChargeAttack()
    {
        if (attackInProgress)
        {

            if (aoeOnce)
            {
                var vector2 = Random.insideUnitCircle.normalized * 10;

                savedPlayerPos = player.transform.position;

                bossShadow.transform.position = new Vector3(vector2.x + savedPlayerPos.x, vector2.y + savedPlayerPos.y, 0);

                Vector3 bossPos = bossShadow.transform.position;

                Vector3 spot = new Vector3(savedPlayerPos.x, savedPlayerPos.y, 0);
                Vector3 position = (bossPos + spot) / 2;

                float distanceY = Mathf.Pow(Mathf.Abs(savedPlayerPos.y - bossPos.y), 2);
                float distanceX = Mathf.Pow(Mathf.Abs(savedPlayerPos.x - bossPos.x), 2);
                float distance = Mathf.Sqrt(distanceX + distanceY);

                dangerRectangle.transform.rotation = Quaternion.LookRotation(Vector3.forward, position - bossPos) * Quaternion.Euler(0, 0, 90);
                dangerRectangle.transform.position = position;
                dangerRectangle.transform.localScale = new Vector3(distance, 1.5f, 1);

                dangerRectangle.SetActive(true);
                aoeOnce = false;
            }

            if (!aoeOnce)
            {
                reactionTime -= Time.deltaTime;

                if (reactionTime <= 0)
                {
                    charging = true;
                    reactionTime = 1.5f;
                }
            }

            if (!aoeOnce && charging)
            {
                bossShadow.SetActive(true);
                dangerRectangle.SetActive(false);
                bossShadow.transform.position = Vector2.MoveTowards(bossShadow.transform.position, savedPlayerPos, speed * Time.deltaTime);

                if (bossShadow.transform.position == savedPlayerPos)
                {
                    attackInProgress = false;
                    charging = false;
                    isAvailable = true;
                    aoeOnce = true;
                    bossShadow.gameObject.SetActive(false);
                }
            }
        }
    }
}
