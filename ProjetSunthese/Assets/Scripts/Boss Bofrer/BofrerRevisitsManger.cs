using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BofrerRevisitsManger : MonoBehaviour
{
    [SerializeField] private List<BossAttack> mountainBossAttackPrefabs;
    [SerializeField] private List<BossAttack> lavaBossAttackPrefabs;
    [SerializeField] private List<BossAttack> bobBossAttackPrefabs;
    [SerializeField] private List<BossAttack> michaelBossAttackPrefabs;
    private List<BossAttack> stolenAttacks = new List<BossAttack>();
    private List<BossAttack> chosenAttacks;

    private void Awake()
    {
        chosenAttacks = GameManager.instance.GetStoredBofrerStolenAttacks();
        UpdateStolenAttackList();
    }

    private void InitStolenAttacks()
    {
        foreach (var attack in chosenAttacks)
        {
            stolenAttacks.Add(Instantiate(attack));
            stolenAttacks[stolenAttacks.Count - 1].transform.position = transform.position;
            stolenAttacks[stolenAttacks.Count - 1].gameObject.SetActive(false);
        }
    }

    private void UpdateStolenAttackList()
    {
        List<Scene> levelsDone = GameManager.instance.GetLevelsDone();
        foreach (var item in levelsDone)
        {
            switch (item)
            {
                case Scene.CharlesLevel:
                    if (!HasAttackTypeOf(BossAttackType.Michael))
                        StealRandomMichaelAttack();
                    break;
                case Scene.GabLevel:
                    if (!HasAttackTypeOf(BossAttackType.Bob))
                        StealRandomBobAttack();
                    break;
                case Scene.KevenLevel:
                    if (!HasAttackTypeOf(BossAttackType.Lava))
                        StealRandomLavaAttack();
                    break;
                case Scene.MarcAntoine:
                    if (!HasAttackTypeOf(BossAttackType.Mountain))
                        StealRandomMountainAttack();
                    break;
                default:
                    break;
            }
        }

        InitStolenAttacks();
    }

    bool HasAttackTypeOf(BossAttackType type)
    {
        foreach (var attack in chosenAttacks)
        {
            if (attack.GetBossAttackType() == type)
                return true;
        }
        return false;
    }

    public List<BossAttack> GetStolenAttacks()
    {
        return stolenAttacks;
    }

    public void StoreAttacks()
    {
        GameManager.instance.StoreBofrerStolenAttacks(chosenAttacks);
    }

    void StealRandomMountainAttack()
    {
        int num = Random.Range(0, mountainBossAttackPrefabs.Count);
        chosenAttacks.Add(mountainBossAttackPrefabs[num]);
        Debug.Log("stole " + mountainBossAttackPrefabs[num].name + " number " + num);
    }

    void StealRandomLavaAttack()
    {
        int num = Random.Range(0, lavaBossAttackPrefabs.Count);
        chosenAttacks.Add(lavaBossAttackPrefabs[num]);
        Debug.Log("stole " + lavaBossAttackPrefabs[num].name + " number " + num);
    }

    void StealRandomBobAttack()
    {
        int num = Random.Range(0, bobBossAttackPrefabs.Count);
        chosenAttacks.Add(bobBossAttackPrefabs[num]);
        Debug.Log("stole " + bobBossAttackPrefabs[num].name + " number " + num);
    }

    void StealRandomMichaelAttack()
    {
        int num = Random.Range(0, michaelBossAttackPrefabs.Count);
        chosenAttacks.Add(michaelBossAttackPrefabs[num]);
        Debug.Log("stole " + michaelBossAttackPrefabs[num].name + " number " + num);
    }
}
