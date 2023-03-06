using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMaker : MonoBehaviour
{
    public static SoundMaker instance = null;

    [SerializeField] private static int arrayLenght = 15;
    [SerializeField] private GameObject individualSoundMaker;
    [SerializeField] private float testVolume;

    private GameObject[] soundMakerArray = new GameObject[arrayLenght];
    private SoundManager soundManager;
    private GameObject walkingMaker;

    private float DEFAULT_VOLUME = 0.2f;
    private float WALK_VOLUME = 0.05f;
    private float FIRE_ATTACK_VOLUME = 0.075f;

    void Start()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        for (int i = 0; i < arrayLenght; i++)
        {
            soundMakerArray[i] = Instantiate(individualSoundMaker);
            soundMakerArray[i].SetActive(false);
            soundMakerArray[i].transform.parent = gameObject.transform;
        }
    }

    public void PanelPopUpSound(Vector2 position)
    {
        RequestSound(position, soundManager.PanelPopUp);
    }

    public void BuyItemSound(Vector2 position)
    {
        RequestSound(position, soundManager.BuyItem);
    }

    public void DenyBuyItemSound(Vector2 position)
    {
        RequestSound(position, soundManager.DenyBuyItem);
    }

    public void PlayerRollSound(Vector2 position)
    {
        RequestSound(position, soundManager.PlayerRoll);
    }

    public void PlayerBowAttackSound(Vector2 position)
    {
        RequestSound(position, soundManager.PlayerBowAttack);
    }

    public void PlayerAxeAttackSound(Vector2 position)
    {
        RequestSound(position, soundManager.PlayerAxeAttack);
    }

    public void PlayerWandAttackSound(Vector2 position)
    {
        RequestSound(position, soundManager.PlayerWandAttack);
    }

    public void PlayerStaffAttackSound(Vector2 position)
    {
        RequestSound(position, soundManager.PlayerStaffAttack);
    }

    public void PlayerDaggerAttackSound(Vector2 position)
    {
        RequestSound(position, soundManager.PlayerDaggerAttack);
    }

    public void PlayerSwordAttackSound(Vector2 position)
    {
        RequestSound(position, soundManager.PlayerSwordAttack);
    }

    public void PlayerDieSound(Vector2 position)
    {
        RequestSound(position, soundManager.PlayerDie);
    }

    public void PlayerWalkSound(Vector2 position)
    {
        RequestInfiniteSound(position, soundManager.PlayerWalk, WALK_VOLUME);
    }

    public void StopPlayerWalkSound()
    {
        if (walkingMaker != null)
        {
            walkingMaker.SetActive(false);
        }
    }

    public void PlayerTakeDamageSound(Vector2 position)
    {
        RequestSound(position, soundManager.PlayerTakeDamage, DEFAULT_VOLUME);
    }

    public void GontrandShockWaveSound(Vector2 position)
    {
        RequestSound(position, soundManager.GontrandShockWave, FIRE_ATTACK_VOLUME);
    }

    public void GontrandAuraSound(Vector2 position)
    {
        RequestSound(position, soundManager.GontrandAura);
    }

    public void GontrandDieSound(Vector2 position)
    {
        RequestSound(position, soundManager.GontrandDie);
    }

    public void MichealTPSound(Vector2 position)
    {
        RequestSound(position, soundManager.MichealTP);
    }

    public void MichealThrowSound(Vector2 position)
    {
        RequestSound(position, soundManager.MichealThrow);
    }

    public void MichealDieSound(Vector2 position)
    {
        RequestSound(position, soundManager.MichealDie);
    }

    public void JgRockThrowSound(Vector2 position)
    {
        RequestSound(position, soundManager.JgRockThrow);
    }

    public void JgSpawnStalagmitesSound(Vector2 position)
    {
        RequestSound(position, soundManager.JgSpawnStalagmites);
    }

    public void JgSpawnMobsSound(Vector2 position)
    {
        RequestSound(position, soundManager.JgSpawnMobs);
    }

    public void JgDieSound(Vector2 position)
    {
        RequestSound(position, soundManager.JgDie);
    }

    /************* BOB ***************/
    public void BobAuraSound(Vector2 position)
    {
        RequestSound(position, soundManager.BobAura, 0.7f);
    }

    public void BobLaserSound(Vector2 position)
    {
        RequestSound(position, soundManager.BobLaser, 0.2f);
    }

    public void BobInvincibilitySound(Vector2 position)
    {
        RequestSound(position, soundManager.BobInvincibility, 0.4f);
    }

    public void BobDieSound(Vector2 position)
    {
        RequestSound(position, soundManager.BobDie);
    }

    public void BobFireSound(Vector2 position)
    {
        RequestSound(position, soundManager.BobFires, 0.7f);
    }

    /************ BOFRER ********************/
    public void BofrerBFLSound(Vector2 position)
    {
        RequestSound(position, soundManager.BofrerBFL);
    }

    public void BofrerRocketsSound(Vector2 position)
    {
        RequestSound(position, soundManager.BofrerRockets);
    }

    public void BofrerShieldHitSound(Vector2 position)
    {
        RequestSound(position, soundManager.BofrerShieldHitSound, DEFAULT_VOLUME);
    }

    public void BofrerHomingBall(Vector2 position)
    {
        RequestSound(position, soundManager.BofrerHomingBall);
    }

    public void BofrerStolenAttackSound(Vector2 position)
    {
        RequestSound(position, soundManager.BofrerStolenAttackSound);
    }


    public void HealSound(Vector2 position)
    {
        RequestSound(position, soundManager.Heal);
    }

    public void UseItemSound(Vector2 position)
    {
        RequestSound(position, soundManager.UseItem);
    }

    public void LevelUpSound(Vector2 position)
    {
        RequestSound(position, soundManager.LevelUp);
    }

    public void VictorySound(Vector2 position)
    {
        RequestSound(position, soundManager.Victory);
    }

    public void GameOverSound(Vector2 position)
    {
        RequestSound(position, soundManager.GameOver);
    }

    public void EnemyDeathSound(Vector2 position)
    {
        RequestSound(position, soundManager.EnemyDeath, DEFAULT_VOLUME);
    }

    /******************** Pause ***********************/
    public void PauseSound(Vector2 position)
    {
        RequestSound(position, soundManager.Pause, 0.4f);
    }

    public void UnpauseSound(Vector2 position)
    {
        RequestSound(position, soundManager.Unpause, 0.4f);
    }

    /******************** Request ***********************/
    public void RequestSound(Vector2 position, AudioClip audioClip)
    {
        foreach (GameObject individual in soundMakerArray)
        {
            if (!individual.activeSelf && individual != walkingMaker)
            {
                individual.SetActive(true);
                individual.GetComponent<IndividualSoundMaker>().PlayAtPoint(audioClip, position);

                return;
            }
        }
    }

    private void RequestSound(Vector2 position, AudioClip audioClip, float volume)
    {
        foreach (GameObject individual in soundMakerArray)
        {
            if (!individual.activeSelf && individual != walkingMaker)
            {
                individual.SetActive(true);
                individual.GetComponent<IndividualSoundMaker>().PlayAtPoint(audioClip, position, volume);

                return;
            }
        }
    }

    private void RequestInfiniteSound(Vector2 position, AudioClip audioClip, float volume)
    {
        if (walkingMaker == null)
        {
            foreach (GameObject individual in soundMakerArray)
            {
                if (!individual.activeSelf)
                {
                    walkingMaker = individual;
                    individual.SetActive(true);
                    individual.GetComponent<IndividualSoundMaker>().InfinitePlayAtPoint(audioClip, position, volume);

                    return;
                }
            }
        }
        else
        {
            walkingMaker.SetActive(true);
            walkingMaker.GetComponent<IndividualSoundMaker>().InfinitePlayAtPoint(audioClip, position, volume);
        }
    }
}
