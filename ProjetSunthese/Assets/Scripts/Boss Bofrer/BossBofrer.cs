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
    private BossBofrerBFL bfl;
    private Animator animator;
    private void Awake()
    {
        stolenAttacks = new List<BossAttack>();
        bfl = Instantiate(bflPrefab);
        bfl.transform.position = transform.position;
        bfl.gameObject.SetActive(false);
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            StartRandomStolenAttack();
        }
        if (Input.GetKeyDown(KeyCode.I))
            StealRandomMountainAttack();
        if (Input.GetKeyDown(KeyCode.J))
            StartBFL();
    }
    protected override void Drop()
    {
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

    IEnumerator BFLRoutine()
    {
        animator.SetBool("Charging", true);
        yield return new WaitForSeconds(bflChargeup);
        bfl.Launch();
        animator.SetBool("Charging", false);
    }
}
