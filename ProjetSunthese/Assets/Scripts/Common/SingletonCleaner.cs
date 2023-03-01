using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonCleaner : MonoBehaviour
{
    private void OnEnable()
    {
        if (GameManager.instance != null)
        {
            Destroy(Player.instance.gameObject);
            Destroy(SoundMaker.instance.gameObject);
            Destroy(SoundManager.instance.gameObject);
            Destroy(GameManager.instance.gameObject);
            Destroy(DamageNumbersManager.instance.gameObject);
            Destroy(ProjectilesManager.instance.gameObject);
            Destroy(Scaling.instance.gameObject);
            Destroy(ParticleManager.instance.gameObject);
            Destroy(MusicMaker.instance.gameObject);
        }
    }
}
