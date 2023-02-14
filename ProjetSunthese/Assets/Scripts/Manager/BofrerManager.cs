using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BofrerManager : MonoBehaviour
{
    private List<BossAttack> stolenAttacks;

    private void Awake()
    {
        stolenAttacks = new List<BossAttack>();
    }

    public List<BossAttack> GetStolenAttacks()
    {
        return stolenAttacks;
    }

    public void AddStolenAttack(BossAttack bossAttack)
    {
        stolenAttacks.Add(bossAttack);
    }

    public void ResetStolenAttacks()
    {
        stolenAttacks.Clear();
    }
}
