using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsPlacer : MonoBehaviour
{
    [SerializeField] private Sprite[] images;
    GameObject achievementTemplate;
    List<Achievement> achievements;
    List<GameObject> achievementUIList = new List<GameObject>();

    private void Awake()
    {
        achievementTemplate = transform.GetChild(0).gameObject;
    }

    private void OnEnable()
    {
        AchivementManager.instance.ReloadAchievementAndSetInObjects();
        achievementTemplate.SetActive(true);
        achievements = AchivementManager.instance.achievements;

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
            achievementUIList.Add(achievement);
        }

        achievementTemplate.SetActive(false);
    }

    private void OnDisable()
    {
        foreach(GameObject listUI in achievementUIList)
        {
            Destroy(listUI);
        }
        achievementUIList.Clear();
    }

}
