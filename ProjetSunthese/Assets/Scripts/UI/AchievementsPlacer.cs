using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsPlacer : MonoBehaviour
{
    private void Start()
    {
        GameObject achievementTemplate = transform.GetChild(0).gameObject;
        GameObject achievement;
        for(int i = 0; i < 10; i++)
        {
            achievement = Instantiate(achievementTemplate, transform);
        }

        Destroy(achievementTemplate);
    }
}
