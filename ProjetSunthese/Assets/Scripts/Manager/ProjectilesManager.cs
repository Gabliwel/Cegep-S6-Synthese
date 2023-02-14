using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilesManager : MonoBehaviour
{
    [Header("Arrow")]
    [SerializeField] private int arrowNb = 20;
    [SerializeField] private GameObject arrowPrefab;
    private Projectile[] arrows;

    private void Awake()
    {
        arrows = new Projectile[arrowNb];
        for (int i = 0; i < arrowNb; i++)
        {
            arrows[i] = Instantiate(arrowPrefab).GetComponent<Arrow>();
            arrows[i].gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().SetProjectilesManager(this);
    }

    public Projectile[] GetArrows()
    {
        return arrows;
    }
}
