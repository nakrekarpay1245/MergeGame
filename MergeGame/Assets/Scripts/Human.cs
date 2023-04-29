using UnityEngine;
using UnityEngine.UI;

public class Human : MonoBehaviour
{
    [Header("HUMAN Body")]
    [SerializeField]
    private GameObject humanBody;

    [Header("Hair")]
    [SerializeField]
    private Image hairImage;

    [Header("Face")]
    [SerializeField]
    private Image faceImage;

    [Header("Eyebrow")]
    [SerializeField]
    private Image eyebrowLeftImage;
    [SerializeField]
    private Image eyebrowRightImage;

    [Header("Eye")]
    [SerializeField]
    private Image eyeLeftImage;
    [SerializeField]
    private Image eyeRightImage;

    [Header("Noise")]
    [SerializeField]
    private Image noiseImage;

    [Header("Shirt")]
    [SerializeField]
    private Image shirtImage;
    [Header("Shirt Arm")]
    [SerializeField]
    private Image leftShirtArmImage;
    [SerializeField]
    private Image rightShirtArmImage;

    [Header("Mouth")]
    [SerializeField]
    private Image mouthImage;

    [Header("Mouth")]
    [SerializeField]
    private Sprite normalMouthSprite;
    [SerializeField]
    private Sprite happyMouthSprite;

    /// <summary>
    /// Sets the mouth image sprite to a randomly selected sprite from the happy mouth sprite list
    /// </summary>
    public void HappyMouth()
    {
        mouthImage.sprite = happyMouthSprite;
    }

    /// <summary>
    /// Sets the mouth image sprite to the normal mouth sprite
    /// </summary>
    public void NormalMouth()
    {
        mouthImage.sprite = normalMouthSprite;
    }

    public void RegenerateHuman()
    {
        hairImage.sprite = HumanManager.singleton.GetRandomHair();

        faceImage.sprite = HumanManager.singleton.GetRandomFace();

        eyebrowLeftImage.sprite = HumanManager.singleton.GetRandomEyebrow();
        eyebrowRightImage.sprite = eyebrowLeftImage.sprite;

        eyeLeftImage.sprite = HumanManager.singleton.GetRandomEye();
        eyeRightImage.sprite = eyeLeftImage.sprite;

        noiseImage.sprite = HumanManager.singleton.GetRandomNoise();

        shirtImage.sprite = HumanManager.singleton.GetRandomShirt();
        leftShirtArmImage.sprite = HumanManager.singleton.GetRandomShirtArm();
        rightShirtArmImage.sprite = leftShirtArmImage.sprite;
    }
}
