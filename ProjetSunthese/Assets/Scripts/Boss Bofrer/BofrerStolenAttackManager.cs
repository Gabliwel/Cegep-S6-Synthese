using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BofrerStolenAttackManager : MonoBehaviour
{
    [SerializeField] private List<BossAttack> mountainBossAttackPrefabs;
    [SerializeField] private List<BossAttack> lavaBossAttackPrefabs;
    [SerializeField] private List<BossAttack> bobBossAttackPrefabs;
    [SerializeField] private List<BossAttack> michaelBossAttackPrefabs;


    private List<BossAttack> stolenAttacks;

    private void Awake()
    { // TODO: implement game manager
        stolenAttacks = GameManager.instance.GetStoredBofrerStolenAttacks();
        UpdateStolenAttackList();
    }

    private void UpdateStolenAttackList()
    { // TODO: implement game manager
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
    }

    bool HasAttackTypeOf(BossAttackType type)
    {
        foreach (var attack in stolenAttacks)
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
        GameManager.instance.StoreBofrerStolenAttacks(stolenAttacks);
    }

    void StealRandomMountainAttack()
    {
        int num = Random.Range(0, mountainBossAttackPrefabs.Count);
        stolenAttacks.Add(Instantiate(mountainBossAttackPrefabs[num]));
        stolenAttacks[stolenAttacks.Count - 1].transform.position = transform.position;
        stolenAttacks[stolenAttacks.Count - 1].gameObject.SetActive(false);
        Debug.Log("stole " + mountainBossAttackPrefabs[num].name + " number " + num);
    }

    void StealRandomLavaAttack()
    {
        int num = Random.Range(0, lavaBossAttackPrefabs.Count);
        stolenAttacks.Add(Instantiate(lavaBossAttackPrefabs[num]));
        stolenAttacks[stolenAttacks.Count - 1].transform.position = transform.position;
        stolenAttacks[stolenAttacks.Count - 1].gameObject.SetActive(false);
        Debug.Log("stole " + lavaBossAttackPrefabs[num].name + " number " + num);
    }

    void StealRandomBobAttack()
    {
        int num = Random.Range(0, bobBossAttackPrefabs.Count);
        stolenAttacks.Add(Instantiate(bobBossAttackPrefabs[num]));
        stolenAttacks[stolenAttacks.Count - 1].transform.position = transform.position;
        stolenAttacks[stolenAttacks.Count - 1].gameObject.SetActive(false);
        Debug.Log("stole " + bobBossAttackPrefabs[num].name + " number " + num);
    }

    void StealRandomMichaelAttack()
    {
        int num = Random.Range(0, michaelBossAttackPrefabs.Count);
        stolenAttacks.Add(Instantiate(michaelBossAttackPrefabs[num]));
        stolenAttacks[stolenAttacks.Count - 1].transform.position = transform.position;
        stolenAttacks[stolenAttacks.Count - 1].gameObject.SetActive(false);
        Debug.Log("stole " + michaelBossAttackPrefabs[num].name + " number " + num);
    }
}
