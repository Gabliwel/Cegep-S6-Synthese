using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://github.com/alexzorzella/Achievements/blob/main/Assets/AchievementManager.cs
public class AchievementManager : MonoBehaviour
{
    public static AchievementManager instance;
    public List<Achievement> achievements;
    private AchievementsList achievementListComponent;

    private JSONSave save;
    public int integer;
    public float floating_point;

    private void Start()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        achievementListComponent = Instantiate(achievementListComponent);
        InitializeAchievements();
    }

    private void Update()
    {
        CheckAchievementCompletion();
    }

    private void InitializeAchievements()
    {
        save = GetComponent<JSONSave>();
        achievementListComponent = save.LoadData();
        achievements = achievementListComponent.achievementList;
        if (achievements != null)
            return;

        achievements = new List<Achievement>();
        achievements.Add(new Achievement("Step By Step", "Set your integer to 110 or above.", (object o) => integer >= 100));
        achievements.Add(new Achievement("Not So Precise", "Set your floating point to 230.5 or above.", (object o) => floating_point >= 230.5));
    }

    public bool AchievementUnlocked(string achievementName)
    {
        bool result = false;

        if (achievements == null)
            return false;

        Achievement[] achievementsArray = achievements.ToArray();
        Achievement a = Array.Find(achievementsArray, ach => achievementName == ach.title);

        if (a == null)
            return false;

        result = a.achieved;

        return result;
    }

    private void CheckAchievementCompletion()
    {
        if (achievements == null)
            return;

        foreach (var achievement in achievements)
        {
            achievement.UpdateCompletion();
        }
    }

    public AchievementsList GetAchievementData()
    {
        return achievementListComponent;
    }

    void KilledEnnemies()
    {

    }

    void KilledMichael()
    {

    }
}