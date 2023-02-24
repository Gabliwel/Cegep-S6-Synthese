using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RareBoy : Enemy
{
    [SerializeField] private float speed = 5;
    [SerializeField] private GameObject[] itemDrop;

    private Sensor catchSensor;
    private Sensor rangeSensor;
    private ISensor<Player> playerRangeSensor;
    private ISensor<Player> playerCatchingSensor;

    private Player player;

    private Animator animator;

    void Awake()
    {
        rangeSensor = transform.Find("RangeSensor").GetComponent<Sensor>();
        catchSensor = transform.Find("CatchSensor").GetComponent<Sensor>();

        playerRangeSensor = rangeSensor.For<Player>();
        playerCatchingSensor = catchSensor.For<Player>();

        playerRangeSensor.OnSensedObject += OnPlayerRangeSense;
        playerRangeSensor.OnUnsensedObject += OnPlayerRangeUnsense;

        playerCatchingSensor.OnSensedObject += OnPlayerCatchSense;
        playerCatchingSensor.OnSensedObject += OnPlayerCatchUnsense;

        animator = GetComponent<Animator>();
    }

    void OnPlayerRangeSense(Player player)
    {
        this.player = player;
    }

    void OnPlayerRangeUnsense(Player player)
    {
        this.player = null;
    }

    void OnPlayerCatchSense(Player player)
    {
        Die();
    }

    void OnPlayerCatchUnsense(Player player)
    {

    }

    public override void Harm(float ammount, float poison)
    {
        ammount = 0;
        poison = 0;
        base.Harm(ammount, poison);
    }

    void Update()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, -1 * speed * Time.deltaTime);
            animator.SetFloat("Move X", transform.position.x - player.transform.position.x);
            animator.SetFloat("Move Y", transform.position.y - player.transform.position.y);
        }
    }

    protected override void Drop()
    {
        Player.instance.GainDrops(xpGiven, goldDropped);
        GameObject item = Instantiate(itemDrop[Random.Range(0, itemDrop.Length)]);
        item.transform.position = transform.position;
    }
}
