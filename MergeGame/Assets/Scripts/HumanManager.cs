using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HumanManager : MonoSingleton<HumanManager>
{
    private const float uiDelay = 0.35f;

    [Header("HUMAN Body")]
    [SerializeField]
    private GameObject humanBody;

    [Header("Hair")]
    [SerializeField]
    private SpriteRenderer hairImage;

    [Header("Hair")]
    [SerializeField]
    private List<Sprite> hairSpriteList;

    [Header("Face")]
    [SerializeField]
    private SpriteRenderer faceImage;
    [SerializeField]
    private SpriteRenderer eyebrowLeftImage;
    [SerializeField]
    private SpriteRenderer eyebrowRightImage;
    [SerializeField]
    private SpriteRenderer eyeLeftImage;
    [SerializeField]
    private SpriteRenderer eyeRightImage;
    [SerializeField]
    private SpriteRenderer mouthImage;
    [SerializeField]
    private SpriteRenderer noiseImage;

    [Header("Face")]
    [SerializeField]
    private List<Sprite> faceSpriteList;
    [SerializeField]
    private List<Sprite> eyebrowSpriteList;
    [SerializeField]
    private List<Sprite> eyeSpriteList;
    [SerializeField]
    private List<Sprite> noiseSpriteList;

    [Header("Shirt")]
    [SerializeField]
    private SpriteRenderer shirtImage;

    [Header("Shirt")]
    [SerializeField]
    private List<Sprite> shirtSpriteList;

    [Header("Client Situation")]
    [Header("Mouth")]
    [SerializeField]
    private List<Sprite> happyMouthSpriteList;
    [SerializeField]
    private Sprite normalMouthSprite;

    private void Start()
    {
        GenerateHumanFace();
    }

    /// <summary>
    /// Generates a random human face by setting the sprites of various image components
    /// </summary>
    public void GenerateHumanFace()
    {
        //Debug.Log("GenerateHumanFace");

        // Animates the human body by scaling it down and then back up again
        humanBody.transform.DOScale(0, uiDelay).OnComplete(() =>
                   humanBody.transform.DOScale(1, uiDelay));

        // Randomly selects a hair sprite and sets it on the hair image component
        int hairIndex = Random.Range(0, hairSpriteList.Count);
        hairImage.sprite = hairSpriteList[hairIndex];

        // Randomly selects a face sprite and sets it on the face image component
        int faceIndex = Random.Range(0, faceSpriteList.Count);
        faceImage.sprite = faceSpriteList[faceIndex];

        // Randomly selects an eyebrow sprite and sets it on both eyebrow image components
        int eyebrowIndex = Random.Range(0, eyebrowSpriteList.Count);
        eyebrowLeftImage.sprite = eyebrowSpriteList[eyebrowIndex];
        eyebrowRightImage.sprite = eyebrowSpriteList[eyebrowIndex];

        // Randomly selects an eye sprite and sets it on both eye image components
        int eyeIndex = Random.Range(0, eyeSpriteList.Count);
        eyeLeftImage.sprite = eyeSpriteList[eyeIndex];
        eyeRightImage.sprite = eyeSpriteList[eyeIndex];

        // Randomly selects a nose sprite and sets it on the nose image component
        int noiseIndex = Random.Range(0, noiseSpriteList.Count);
        noiseImage.sprite = noiseSpriteList[noiseIndex];

        // Randomly selects a shirt sprite and sets it on the shirt image component
        int shirtIndex = Random.Range(0, shirtSpriteList.Count);
        shirtImage.sprite = shirtSpriteList[shirtIndex];

        NormalMouth();
    }

    /// <summary>
    /// Sets the mouth image sprite to a randomly selected sprite from the happy mouth sprite list
    /// </summary>
    public void HappyMouth()
    {
        int mouthIndex = Random.Range(0, happyMouthSpriteList.Count);
        mouthImage.sprite = happyMouthSpriteList[mouthIndex];
    }

    /// <summary>
    /// Sets the mouth image sprite to the normal mouth sprite
    /// </summary>
    public void NormalMouth()
    {
        mouthImage.sprite = normalMouthSprite;
    }
}
