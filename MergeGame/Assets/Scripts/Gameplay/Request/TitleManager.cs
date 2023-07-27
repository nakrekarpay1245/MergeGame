using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TitleManager : MonoSingleton<TitleManager>
{
    [Header("EXPERIENCE")]
    [SerializeField]
    private float _experience;

    [SerializeField]
    private float _experiencePerCombine;

    [SerializeField]
    private List<int> _levelExperienceList;

    [SerializeField]
    private List<string> _titleList;

    [SerializeField]
    private TextMeshProUGUI _titleText;

    [SerializeField]
    private int _level;

    [SerializeField]
    private TextMeshProUGUI _levelText;

    [SerializeField]
    private Slider _experienceBar;

    private void Start()
    {
        _levelExperienceList[0] = 2;
        for (int i = 1; i < _levelExperienceList.Count; i++)
        {
            _levelExperienceList[i] = _levelExperienceList[i - 1] * 2;
        }
        _experienceBar.value = 0;
        DisplayExperience();
        DisplayTitle();
    }

    public void IncreaseExperience()
    {
        _experience += _experiencePerCombine;
        DisplayExperience();
    }

    private void DisplayExperience()
    {
        if (_experienceBar.value >= 1)
        {
            _experience = 0;
            _level++;
        }

        _experienceBar.value = _experience / _levelExperienceList[_level];

        DisplayTitle();
        DisplayLevel();
    }

    private void DisplayLevel()
    {
        _levelText.text = (_level + 1).ToString();
    }

    private void DisplayTitle()
    {
        _titleText.text = _titleList[_level];
    }
}