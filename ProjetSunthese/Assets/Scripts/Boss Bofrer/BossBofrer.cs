using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBofrer : Enemy
{
    [SerializeField] private List<BossAttack> mountainBossAttackPrefabs;
    [SerializeField] private List<BossAttack> stolenAttacks;
    [SerializeField] private BossBofrerBFL bflPrefab;
    private BossBofrerBFL bfl;
    private void Awake()
    {
        stolenAttacks = new List<BossAttack>();
        bfl = Instantiate(bflPrefab);
        bfl.transform.position = transform.position;
        bfl.gameObject.SetActive(false);
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
        bfl.Launch();
    }
}
