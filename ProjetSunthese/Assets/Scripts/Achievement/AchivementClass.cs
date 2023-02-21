using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    public int nbChestOpened;
    public int nbKilledTotal;

    public AchivementClass(bool lotsChestOpened, int nbChestOpened, bool rageQuit, bool beatBob, bool beatGontrand, bool beatMichael, bool beatJeanGuy, bool beatTheGame)
    {
        this.lotsChestOpened = lotsChestOpened;
        this.nbChestOpened = nbChestOpened;
        this.rageQuit = rageQuit;
        this.beatBob = beatBob;
        this.beatGontrand = beatGontrand;
        this.beatMichael = beatMichael;
        this.beatJeanGuy = beatJeanGuy;
        this.beatTheGame = beatTheGame;
    }

    public AchivementClass()
    {
        this.lotsChestOpened = false;
        this.nbChestOpened = 0;
        this.rageQuit = false;
        this.beatBob = false;
        this.beatGontrand = false;
        this.beatMichael = false;
        this.beatJeanGuy = false;
        this.beatTheGame = false;
    }
}
