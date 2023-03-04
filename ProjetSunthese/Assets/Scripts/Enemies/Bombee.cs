using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombee : Enemy
{
    private const float OFFSET_MIN_MAX = 0.5f;
    [SerializeField] private float speed = 3;
    [SerializeField] private BombHolder bombPrefab;

    private BombHolder projectile;
    private BombHolder deathBomb;
    private PlayerProximitySensor visionProximity;
    private PlayerProximitySensor rangeProximity;
    private Animator animator;

    protected override void Awake()
    {
        base.Awake();
        projectile = Instantiate(bombPrefab);
        projectile.gameObject.SetActive(false);
        deathBomb = Instantiate(bombPrefab);
        deathBomb.gameObject.SetActive(false);

        visionProximity = transform.Find("VisionProximity").GetComponent<PlayerProximitySensor>();
        rangeProximity = transform.Find("RangeProximity").GetComponent<PlayerProximitySensor>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (visionProximity.IsClose())
        {
            animator.SetFloat("Move X", Player.instance.transform.position.x - transform.position.x);
            animator.SetFloat("Move Y", Player.instance.transform.position.y - transform.position.y);

            if (!rangeProximity.IsClose())
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.instance.transform.position, speed * Time.deltaTime);
            }
            else
            {
                if (!projectile.gameObject.activeSelf)
                {
                    projectile.transform.position = transform.position;
                    projectile.SetDestination(Player.instance.transform.position);
                    projectile.gameObject.SetActive(true);
                }
            }
        }
    }

    protected override void Drop()
    {
        Player.instance.GainDrops(xpGiven, goldDropped);
    }

    public override void Die()
    {
        base.Die();
        deathBomb.transform.position = transform.position;
        deathBomb.SetDestination(transform.position + GetRandomOffset());
        deathBomb.SetFuse(0);
        deathBomb.gameObject.SetActive(true);
    }

    private Vector3 GetRandomOffset()
    {
        Vector3 offset = Vector3.zero;
        offset.x = Random.Range(-OFFSET_MIN_MAX, OFFSET_MIN_MAX);
        offset.y = Random.Range(-OFFSET_MIN_MAX, OFFSET_MIN_MAX);

        return offset;
    }
}
