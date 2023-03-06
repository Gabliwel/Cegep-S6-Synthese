using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RareBoy : Enemy
{
    [SerializeField] private float speed = 5;
    [SerializeField] private GameObject[] itemDrop;

    private Sensor catchSensor;
    private PlayerProximitySensor rangeSensor;
    private ISensor<Player> playerCatchingSensor;

    private Animator animator;

    protected override void Awake()
    {
        base.Awake();
        rangeSensor = transform.Find("RangeSensor").GetComponent<PlayerProximitySensor>();
        catchSensor = transform.Find("CatchSensor").GetComponent<Sensor>();

        playerCatchingSensor = catchSensor.For<Player>();

        playerCatchingSensor.OnSensedObject += OnPlayerCatchSense;
        playerCatchingSensor.OnUnsensedObject += OnPlayerCatchUnsense;

        animator = GetComponent<Animator>();
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
        if (rangeSensor.IsClose())
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.instance.transform.position, -1 * speed * Time.deltaTime);
            animator.SetFloat("Move X", transform.position.x - Player.instance.transform.position.x);
            animator.SetFloat("Move Y", transform.position.y - Player.instance.transform.position.y);
        }
    }

    protected override void Drop()
    {
        Player.instance.GainDrops(xpGiven, goldDropped);
        GameObject item = Instantiate(itemDrop[Random.Range(0, itemDrop.Length)]);
        item.transform.position = transform.position;
    }
}
