using System.Collections.Generic;
using UnityEngine;

public class HumanManager : MonoSingleton<HumanManager>
{
    [Header("Face")]
    [SerializeField]
    private List<Sprite> _faceSpriteList;

    private int _faceIndex;

    /// <summary>
    /// Randomly selects a sprite
    /// </summary>
    /// <returns></returns>
    public Sprite GetRandomFace()
    {
        _faceIndex = Random.Range(0, _faceSpriteList.Count);
        return _faceSpriteList[_faceIndex];
    }
}