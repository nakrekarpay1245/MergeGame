using UnityEngine;
using DG.Tweening;

public class MainManager : MonoBehaviour
{
    [Header("Settings Menu")]
    [SerializeField]
    private GameObject _settingsMenu;

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }

    public void OpenSettingsButton()
    {
        _settingsMenu.transform.DOScale(1, TimeManager.singleton.GetUIDelay());
    }
    public void CloseSettingsButton()
    {
        _settingsMenu.transform.DOScale(0, TimeManager.singleton.GetUIDelay());
    }
}
