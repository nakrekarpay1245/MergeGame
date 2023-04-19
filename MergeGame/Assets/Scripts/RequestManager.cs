using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RequestManager : MonoSingleton<RequestManager>
{
    private const float uiDelay = 0.35f;

    [Header("Request Buble")]
    [SerializeField]
    private GameObject requestBuble;

    [Header("Entities")]
    [SerializeField]
    private List<Entity> entityList;

    [Header("Display")]
    [SerializeField]
    private SpriteRenderer requestImage;

    private Entity requestEntity;

    private void Start()
    {
        GetRequest();
    }

    /// <summary>
    /// Gets a random request by selecting a random entity from the entity 
    /// list and setting its sprite on the request image component
    /// </summary>
    public void GetRequest()
    {
        // Animates the request bubble by scaling it down and then back up again
        requestBuble.transform.DOScale(0, uiDelay).OnComplete(() =>
            requestBuble.transform.DOScale(1, uiDelay));

        // Randomly selects an entity from the entity list and sets it as the request entity
        int requestIndex = Random.Range(0, entityList.Count);
        requestEntity = entityList[requestIndex];

        // Sets the sprite of the request image component to the sprite of the request entity
        requestImage.sprite = requestEntity.GetSprite();
    }

    /// <summary>
    /// Returns the request entity
    /// </summary>
    /// <returns></returns>
    public Entity GetRequestEntity()
    {
        return requestEntity;
    }

    /// <summary>
    /// Generates a new human face using the HumanManager and gets a new request.
    /// </summary>
    public void SetRequest()
    {
        HumanManager.singleton.GenerateHumanFace();
        GetRequest();
    }
}
