using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDrops : MonoBehaviour
{
    [SerializeField] List<GameObject> weaponsDrop;
    [SerializeField] GameObject portal;
    [SerializeField] Vector3 portalSpawnPosition;

    private void Awake()
    {
        for (int i = 0; i < weaponsDrop.Count; i++)
        {
            weaponsDrop[i] = Instantiate(weaponsDrop[i]);
            weaponsDrop[i].SetActive(false);
        }
        portal = Instantiate(portal);
        portal.gameObject.SetActive(false);
    }

    public void BossDrop(Vector3 bossPosition)
    {
        string layerName = GetComponent<SpriteRenderer>().sortingLayerName;

        if (weaponsDrop.Count > 0)
        {
            GameObject weapon = weaponsDrop[Random.Range(0, weaponsDrop.Count)];
            weapon.transform.position = bossPosition;
            weapon.layer = LayerMask.NameToLayer(layerName);
            weapon.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer(layerName);
            weapon.SetActive(true);
        }

        portal.transform.position = portalSpawnPosition;
        portal.layer = LayerMask.NameToLayer(layerName);
        portal.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer(layerName);
        portal.SetActive(true);
    }

    public void BossDrop()
    {
        string layerName = GetComponent<SpriteRenderer>().sortingLayerName;

        portal.transform.position = portalSpawnPosition;
        portal.SetActive(true);
    }
}
