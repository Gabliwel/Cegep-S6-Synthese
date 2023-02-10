using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBofrer : Enemy
{
    [Header("Attack Steal")]
    [SerializeField] private List<BossAttack> mountainBossAttackPrefabs;
    [SerializeField] private List<BossAttack> stolenAttacks;
    [Header("BFL")]
    [SerializeField] private BossBofrerBFL bflPrefab;
    [SerializeField] private float bflChargeup;
    [Header("Shield Minions")]
    [SerializeField] private BossBofrerShieldMinionSpawner shieldMinionSpawnerPrefab;
    [SerializeField] private bool shieldActive;
    private BossBofrerShieldMinionSpawner minionSpawner;
    private BossBofrerBFL bfl;
    private Animator animator;
    private GameObject shield;
    private void Awake()
    {
        stolenAttacks = new List<BossAttack>();
        bfl = Instantiate(bflPrefab);
        bfl.transform.position = transform.position;
        bfl.gameObject.SetActive(false);
        minionSpawner = Instantiate(shieldMinionSpawnerPrefab);
        minionSpawner.transform.position = transform.position;
        animator = GetComponent<Animator>();
        shield = transform.Find("Shield").gameObject;
        shield.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            StartRandomStolenAttack();
        if (Input.GetKeyDown(KeyCode.I))
            StealRandomMountainAttack();
        if (Input.GetKeyDown(KeyCode.J))
            StartBFL();
        if (Input.GetKeyDown(KeyCode.K))
            StartMinionSpawn();
        shieldActive = minionSpawner.AnyMinionActive();
        shield.SetActive(shieldActive);
    }
    protected override void Drop()
    {
    }

    public override void Harm(float ammount)
    {
        if (!shieldActive)
            base.Harm(ammount);
    }

    void StealRandomMountainAttack()
    {
        int num = Random.Range(0, mountainBossAttackPrefabs.Count);
        stolenAttacks.Add(Instantiate(mountainBossAttackPrefabs[num]));
        stolenAttacks[stolenAttacks.Count - 1].transform.position = transform.position;
        Debug.Log("stole " + mountainBossAttackPrefabs[num].name + " number " + num);
    }

    void StartRandomStolenAttack()
    {
        int num = Random.Range(0, stolenAttacks.Count);
        stolenAttacks[num].transform.position = transform.position;
        stolenAttacks[num].Launch();
        Debug.Log("firering " + stolenAttacks[num].name + " number " + num);
    }

    void StartRandomNormalAttack()
    {
    }

    void StartBFL()
    {
        StartCoroutine(BFLRoutine());
    }

    void StartMinionSpawn()
    {
        minionSpawner.Launch();
    }

    IEnumerator BFLRoutine()
    {
        animator.SetBool("Charging", true);
        yield return new WaitForSeconds(bflChargeup);
        bfl.Launch();
        animator.SetBool("Charging", false);
    }
}
