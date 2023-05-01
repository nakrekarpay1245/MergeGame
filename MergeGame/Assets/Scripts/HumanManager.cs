using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class HumanManager : MonoSingleton<HumanManager>
{
    [Header("Face")]
    [SerializeField]
    private List<Sprite> faceSpriteList;

    private int faceIndex;

    public Sprite GetRandomFace()
    {
        // Randomly selects a hair sprite and sets it on the hair image component
        faceIndex = Random.Range(0, faceSpriteList.Count);
        return faceSpriteList[faceIndex];
    }
}
