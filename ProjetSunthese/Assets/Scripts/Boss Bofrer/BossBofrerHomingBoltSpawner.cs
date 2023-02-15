using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBofrerHomingBoltSpawner : BossAttack
{
    [SerializeField] private int boltNb = 25;
    [SerializeField] private BossBofrerHomingBolt boltPrefab;
    [SerializeField] private int minBoltNb;
    [SerializeField] private int maxBoltNb;
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private float totalTimeToSpawn;
    [SerializeField] private float yAddedValue;
    [SerializeField] private float boltDistance;
    private BossBofrerHomingBolt[] bolts;

    private void Awake()
    {
        bolts = new BossBofrerHomingBolt[boltNb];
        for (int i = 0; i < boltNb; i++)
        {
            bolts[i] = GameObject.Instantiate<BossBofrerHomingBolt>(boltPrefab);
            bolts[i].gameObject.SetActive(false);
        }
        type = BossAttackType.Bofrer;
    }

    public override void Launch()
    {
        StartCoroutine(SpawnBolts());
    }

    public BossBofrerHomingBolt GetAvailableBolt()
    {
        foreach (BossBofrerHomingBolt bolt in bolts)
        {
            if (!bolt.gameObject.activeSelf)
                return bolt;
        }

        bolts[bolts.Length - 1].gameObject.SetActive(false);
        return bolts[bolts.Length - 1];
    }

    IEnumerator SpawnBolts()
    {
        int ammount = Random.Range(minBoltNb, maxBoltNb);
        float rotation = 0;
        float step = 360 / ammount;
        float timePerBolt = totalTimeToSpawn / ammount;
        Vector3 axis = Vector3.zero;

        for (int i = 0; i < ammount; i++)
        {
            BossBofrerHomingBolt bolt = GetAvailableBolt();
            bolt.gameObject.SetActive(true);
            axis.z = rotation;
            bolt.transform.rotation = Quaternion.Euler(Vector3.down);
            bolt.transform.position = transform.position;
            float rad = i * Mathf.PI * 2 / ammount;
            Vector3 newPos = transform.position + (new Vector3(Mathf.Cos(rad) * ammount * boltDistance, Mathf.Sin(rad) * ammount * boltDistance + yAddedValue, 0));
            bolt.transform.position = newPos;


            rotation += step;
            yield return new WaitForSeconds(timePerBolt);
        }
    }
}
