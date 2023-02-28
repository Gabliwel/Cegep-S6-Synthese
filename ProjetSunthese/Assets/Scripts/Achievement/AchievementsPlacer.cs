using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsPlacer : MonoBehaviour
{
    private void Start()
    {
        GameObject achievementTemplate = transform.GetChild(0).gameObject;
        GameObject achievement;
        List<Achievement> achievements = AchivementManager.instance.achievements;
        for(int i = 0; i < achievements.Count; i++)
        {
            achievement = Instantiate(achievementTemplate, transform);

            achievement.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = achievements[i].Title;
            achievement.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = achievements[i].Description;
            if (achievements[i].IsCompleted)
            {
                achievement.GetComponent<Image>().color = new Color(0, 191, 47, 255);
            }
        }

        Destroy(achievementTemplate);
    }
}
