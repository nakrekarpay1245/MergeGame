using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TitleManager : MonoSingleton<TitleManager>
{
    [Header("EXPERIENCE")]
    [SerializeField]
    private float experience;

    [SerializeField]
    private float experiencePerCombine;

    [SerializeField]
    private List<int> levelExperienceList;

    [SerializeField]
    private List<string> titleList;

    [SerializeField]
    private TextMeshProUGUI titleText;

    [SerializeField]
    private int level;

    [SerializeField]
    private TextMeshProUGUI levelText;

    [SerializeField]
    private Slider experienceBar;

    private void Start()
    {
        levelExperienceList[0] = 2;
        for (int i = 1; i < levelExperienceList.Count; i++)
        {
            levelExperienceList[i] = levelExperienceList[i - 1] * 2;
        }
        experienceBar.value = 0;
        DisplayExperience();
        DisplayTitle();
    }

    public void IncreaseExperience()
    {
        experience += experiencePerCombine;
        DisplayExperience();
    }

    private void DisplayExperience()
    {
        if (experienceBar.value >= 1)
        {
            experience = 0;
            level++;
        }

        experienceBar.value = experience / levelExperienceList[level];

        DisplayTitle();
        DisplayLevel();
    }

    private void DisplayLevel()
    {
        levelText.text = (level + 1).ToString();
    }

    private void DisplayTitle()
    {
        titleText.text = titleList[level];
    }
}
