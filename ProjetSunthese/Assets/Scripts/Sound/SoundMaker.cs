using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMaker : MonoBehaviour
{
    [SerializeField] private static int arrayLenght = 15;
    [SerializeField] private GameObject individualSoundMaker;

    private GameObject[] soundMakerArray = new GameObject[arrayLenght];
    private SoundManager soundManager;

    void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        for (int i = 0; i < arrayLenght; i++)
        {
            soundMakerArray[i] = Instantiate(individualSoundMaker);
            soundMakerArray[i].SetActive(false);
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
        RequestSound(position, soundManager.PlayerWalk);
    }

    public void PlayerTakeDamageSound(Vector2 position)
    {
        RequestSound(position, soundManager.PlayerTakeDamage);
    }

    public void GontrandShockWaveSound(Vector2 position)
    {
        RequestSound(position, soundManager.GontrandShockWave);
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

    public void BobAuraSound(Vector2 position)
    {
        RequestSound(position, soundManager.BobAura);
    }

    public void BobLaserSound(Vector2 position)
    {
        RequestSound(position, soundManager.BobLaser);
    }

    public void BobInvincibilitySound(Vector2 position)
    {
        RequestSound(position, soundManager.BobInvincibility);
    }

    public void BobDieSound(Vector2 position)
    {
        RequestSound(position, soundManager.BobDie);
    }

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
        RequestSound(position, soundManager.BofrerShieldHitSound);
    }

    public void HealSound(Vector2 position)
    {
        RequestSound(position, soundManager.Heal);
    }

    public void EquipBootsSound(Vector2 position)
    {
        RequestSound(position, soundManager.EquipBoots);
    }

    public void EquipGlovesSound(Vector2 position)
    {
        RequestSound(position, soundManager.EquipGloves);
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

    private void RequestSound(Vector2 position, AudioClip audioClip)
    {
        foreach (GameObject individual in soundMakerArray)
        {
            if (!individual.activeSelf)
            {
                individual.SetActive(true);
                individual.GetComponent<IndividualSoundMaker>().PlayAtPoint(audioClip, position);

                return;
            }
        }
    }
}
