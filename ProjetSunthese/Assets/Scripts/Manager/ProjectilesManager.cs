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
    [Header("Wizard")]
    [SerializeField] private int wizardProjectileNb = 20;
    [SerializeField] private GameObject wizardProjectileHolderPrefab;
    private WizardProjectile[] wizardProjectiles;

    public static ProjectilesManager instance;

    private void Awake()
    {
        arrows = new Projectile[arrowNb];
        for (int i = 0; i < arrowNb; i++)
        {
            arrows[i] = Instantiate(arrowPrefab).GetComponent<Arrow>();
            arrows[i].gameObject.SetActive(false);
            arrows[i].transform.parent = transform;
        }

        warlockProjectiles = new WarlockProjectileHolder[warlockProjectileNb];
        for (int i = 0; i < warlockProjectileNb; i++)
        {
            warlockProjectiles[i] = Instantiate(warlockProjectileHolderPrefab).GetComponent<WarlockProjectileHolder>();
            warlockProjectiles[i].gameObject.SetActive(false);
            warlockProjectiles[i].transform.parent = transform;
        }

        wizardProjectiles = new WizardProjectile[wizardProjectileNb];
        for (int i = 0; i < wizardProjectileNb; i++)
        {
            wizardProjectiles[i] = Instantiate(wizardProjectileHolderPrefab).GetComponent<WizardProjectile>();
            wizardProjectiles[i].gameObject.SetActive(false);
            wizardProjectiles[i].transform.parent = transform;
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

    public WizardProjectile[] GetWizardProjectiles()
    {
        return wizardProjectiles;
    }
}
