using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilesManager : MonoBehaviour
{
    [Header("Arrow")]
    [SerializeField] private int arrowNb = 20;
    [SerializeField] private GameObject arrowPrefab;
    private Projectile[] arrows;
    [Header("Warlock")]
    [SerializeField] private int warlockProjectileNb = 20;
    [SerializeField] private GameObject warlockProjectileHolderPrefab;
    private WarlockProjectileHolder[] warlockProjectiles;

    private ProjectilesManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        arrows = new Projectile[arrowNb];
        for (int i = 0; i < arrowNb; i++)
        {
            arrows[i] = Instantiate(arrowPrefab).GetComponent<Arrow>();
            arrows[i].gameObject.SetActive(false);
        }

        warlockProjectiles = new WarlockProjectileHolder[warlockProjectileNb];
        for (int i = 0; i < warlockProjectileNb; i++)
        {
            warlockProjectiles[i] = Instantiate(warlockProjectileHolderPrefab).GetComponent<WarlockProjectileHolder>();
            warlockProjectiles[i].gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        Player.instance.SetProjectilesManager(this);
    }

    public Projectile[] GetArrows()
    {
        return arrows;
    }

    public WarlockProjectileHolder[] GetWarlockProjectiles()
    {
        return warlockProjectiles;
    }
}
