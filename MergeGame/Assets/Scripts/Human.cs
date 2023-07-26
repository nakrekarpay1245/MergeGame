using UnityEngine;
using UnityEngine.UI;

public class Human : MonoBehaviour
{
    [Header("Face")]
    [SerializeField]
    private Image _faceImage;

    public void RegenerateHuman()
    {
        _faceImage.sprite = HumanManager.singleton.GetRandomFace();
    }
}