using UnityEngine;
using DG.Tweening;

public class MainManager : MonoBehaviour
{
    [Header("Settings Menu")]
    [SerializeField]
    private GameObject settingsMenu;

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }

    public void OpenSettingsButton()
    {
        settingsMenu.transform.DOScale(1, TimeManager.singleton.GetUIDelay());
    }
    public void CloseSettingsButton()
    {
        settingsMenu.transform.DOScale(0, TimeManager.singleton.GetUIDelay());
    }
}
