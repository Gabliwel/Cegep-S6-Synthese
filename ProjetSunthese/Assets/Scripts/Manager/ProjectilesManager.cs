using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilesManager : MonoBehaviour
{
    [Header("Arrow")]
    [SerializeField] private int arrowNb = 20;
    [SerializeField] private GameObject arrowPrefab;
    private Projectile[] arrows;
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
