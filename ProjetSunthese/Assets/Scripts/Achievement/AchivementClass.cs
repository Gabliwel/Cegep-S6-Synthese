using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Billy.Weapons;

public class AchivementClass
{
    public bool lotsChestOpened;
    public bool rageQuit;
    public bool beatBob;
    public bool beatGontrand;
    public bool beatMichael;
    public bool beatJeanGuy;
    public bool beatTheGame;
    public bool nbEnemyKilled;
    public bool weaponMaster;
    public bool skillIssue;


    public int nbChestOpened;
    public int nbKilledTotal;
    public List<WeaponsType> wonWith;
    public int nbDeath;
    public bool rageQuitFirst;

    public AchivementClass(bool lotsChestOpened, int nbChestOpened, bool rageQuit, bool rageQuitFirst, bool beatBob, bool beatGontrand, bool beatMichael, 
        bool beatJeanGuy, bool beatTheGame, bool nbEnemyKilled, int nbKilledTotal, bool weaponMaster, List<WeaponsType> wonWith, bool skillIssue, int nbDeath)
    {
        this.lotsChestOpened = lotsChestOpened;
        this.nbChestOpened = nbChestOpened;
        this.rageQuit = rageQuit;
        this.rageQuitFirst = rageQuitFirst;
        this.beatBob = beatBob;
        this.beatGontrand = beatGontrand;
        this.beatMichael = beatMichael;
        this.beatJeanGuy = beatJeanGuy;
        this.beatTheGame = beatTheGame;
        this.nbEnemyKilled = nbEnemyKilled;
        this.nbKilledTotal = nbKilledTotal;
        this.weaponMaster = weaponMaster;
        this.wonWith = wonWith;
        this.skillIssue = skillIssue;
        this.nbDeath = nbDeath;
    }

    public AchivementClass()
    {
        this.lotsChestOpened = false;
        this.nbChestOpened = 0;
        this.rageQuit = false;
        this.rageQuitFirst = true;
        this.beatBob = false;
        this.beatGontrand = false;
        this.beatMichael = false;
        this.beatJeanGuy = false;
        this.beatTheGame = false;
        this.nbEnemyKilled = false;
        this.nbKilledTotal = 0;
        this.weaponMaster = false;
        this.wonWith = new List<WeaponsType>();
        this.skillIssue = false;
        this.nbDeath = 0;
    }
}
