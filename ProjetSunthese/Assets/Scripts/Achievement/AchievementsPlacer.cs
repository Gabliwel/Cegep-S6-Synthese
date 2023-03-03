using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsPlacer : MonoBehaviour
{
    [SerializeField] private Sprite[] images;
    GameObject achievementTemplate;
    List<Achievement> achievements;

    private void OnEnable()
    {
        achievementTemplate = transform.GetChild(0).gameObject;
        achievements = AchivementManager.instance.achievements;
        Debug.Log(achievements.Count);
        GameObject achievement; 
        for(int i = 0; i < achievements.Count; i++)
        {
            achievement = Instantiate(achievementTemplate, transform);

            achievement.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = achievements[i].Title;
            achievement.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = achievements[i].Description;
            achievement.transform.GetChild(2).GetComponent<Image>().sprite = images[i];
            if (achievements[i].IsCompleted)
            {
                achievement.GetComponent<Image>().color = new Color(0, 191, 47, 255);
            }
        }

        Destroy(achievementTemplate);
    }

}
