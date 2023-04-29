using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class HumanManager : MonoSingleton<HumanManager>
{
    [Header("Hair")]
    [SerializeField]
    private List<Sprite> hairSpriteList;

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
    private List<Sprite> shirtSpriteList;
    [Header("Shirt Arm")]
    [SerializeField]
    private List<Sprite> shirtArmSpriteList;

    [Header("Client Situation")]
    [Header("Mouth")]
    [SerializeField]
    private List<Sprite> happyMouthSpriteList;

    private int shirtIndex;
    private int noiseIndex;
    private int eyeIndex;

    public Sprite GetRandomShirt()
    {
        // Randomly selects a shirt sprite and sets it on the shirt image component
        shirtIndex = Random.Range(0, shirtSpriteList.Count);
        return shirtSpriteList[shirtIndex];
    }

    public Sprite GetRandomShirtArm()
    {
        // Randomly selects a shirtArm sprite and sets it on the shirtArt image component
        return shirtArmSpriteList[shirtIndex];
    }

    public Sprite GetRandomNoise()
    {
        // Randomly selects a nose sprite and sets it on the nose image component
        noiseIndex = Random.Range(0, noiseSpriteList.Count);
        return noiseSpriteList[noiseIndex];
    }

    public Sprite GetRandomEye()
    {
        // Randomly selects an eye sprite and sets it on both eye image components
        int eyeIndex = Random.Range(0, eyeSpriteList.Count);
        return eyeSpriteList[eyeIndex];
    }

    public Sprite GetRandomEyebrow()
    {
        // Randomly selects an eyebrow sprite and sets it on both eyebrow image components
        int eyebrowIndex = Random.Range(0, eyebrowSpriteList.Count);
        return eyebrowSpriteList[eyebrowIndex];
    }

    public Sprite GetRandomFace()
    {
        // Randomly selects a face sprite and sets it on the face image component
        int faceIndex = Random.Range(0, faceSpriteList.Count);
        return faceSpriteList[faceIndex];
    }

    public Sprite GetRandomHair()
    {
        // Randomly selects a hair sprite and sets it on the hair image component
        int hairIndex = Random.Range(0, hairSpriteList.Count);
        return hairSpriteList[hairIndex];
    }
}
