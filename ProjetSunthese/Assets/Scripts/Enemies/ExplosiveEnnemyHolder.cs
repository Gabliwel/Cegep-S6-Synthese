using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveEnnemyHolder : MonoBehaviour
{
    private EnnemyExplosion explosion;
    private Enemy ennemy;

    private void Awake()
    {
        explosion = GetComponentInChildren<EnnemyExplosion>();
        ennemy = GetComponentInChildren<ExplosiveEnnemy>();
    }

    private void OnEnable()
    {
        explosion.gameObject.SetActive(false);
        ennemy.gameObject.SetActive(true);
        ennemy.transform.position = transform.position;
    }

    public void DestinationReached()
    {
        explosion.gameObject.SetActive(true);
        explosion.transform.position = ennemy.transform.position;
    }

    public void ExplosionFinished()
    {
        gameObject.SetActive(false);
    }
}
