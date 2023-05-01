using UnityEngine;
using UnityEngine.UI;

public class Human : MonoBehaviour
{
    [Header("Face")]
    [SerializeField]
    private Image faceImage;

    public void RegenerateHuman()
    {
        faceImage.sprite = HumanManager.singleton.GetRandomFace();
    }
}
