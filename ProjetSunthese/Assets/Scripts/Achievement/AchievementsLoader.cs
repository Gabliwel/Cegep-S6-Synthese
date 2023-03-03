using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsLoader : MonoBehaviour
{
    private void OnEnable()
    {
        AchivementManager.instance.ReloadAchievementAndSetInObjects();
    }
}
